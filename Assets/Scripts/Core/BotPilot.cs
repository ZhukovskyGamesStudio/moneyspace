using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class BotPilot : AbstractPilot {
    private Transform _target;
    public Ship _targetShip;

    private BotState _state;
    private float _distToTarget;
    private float _shipSpeed;
    private Coroutine _attackCoroutine;

    //attack random parameters
    private float _startShootDistance;
    private float _attackTime;

    private EnemyMarker _marker;
    private Coroutine _respawnCoroutine;
    private Vector3 _cachedRandomVector;
    private Vector3 _cachedRandomShootVector;
    private float _cachedRandomValue;
    private bool _isFiringSecond;

    public override void Init() {
        GetShip();
        CreateMarker();
        RespawnShip();
    }

    public override void Activate() {
        base.Activate();
        FindTarget();
    }

    public override void DeActivate() {
        base.DeActivate();
        if (_respawnCoroutine != null) {
            StopCoroutine(_respawnCoroutine);
        }
    }

    protected override ShipType GetShipType() {
        return (ShipType)Random.Range(0, ShipsFactory.Ships.Count);
    }

    protected override void GetShip() {
        base.GetShip();
        ShipConfig shipConfig = ShipsFactory.Ships.First(s => s.ShipType == _ship.ShipType);
        ShipUpgradeData upgrades = shipConfig.GetRandomizedUpgrades();
        _ship.InitFromConfig(shipConfig, upgrades);
        _ship.OnDestroyed += StartRespawning;
        _ship.OnTakeDamage += OnTakingDamage;
    }

    private void CreateMarker() {
        if (_marker != null) {
            return;
        }

        if (_playerData.Team == Team.Red) {
            _marker = GameUI.Instance.ArMarkersManager.CreateRedMarker();
        } else {
            _marker = GameUI.Instance.ArMarkersManager.CreateBlueMarker();
        }

        _marker.SetTarget(_ship.GetTargetLockAnchor);
    }

    private void ChangeMarkerVisibility(bool isActive) {
        if (_marker)
            _marker.gameObject.SetActive(isActive);
    }

    private void FindTarget() {
        bool isTargetPlayer = Random.Range(0, 1f) < ShipsFactory.ShipStatsGeneralConfig.ChanceToGetPlayerAsTarget &&
                              _playerData.Team != Team.Blue;
        if (isTargetPlayer && FindFirstObjectByType<PlayerPilot>().Ship.isActiveAndEnabled) {
            var player = FindFirstObjectByType<PlayerPilot>().Ship;
            if (player.isActiveAndEnabled) {
                _target = player.transform;
            }
        } else {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(_playerData.Team == Team.Blue ? "Red" : "Blue");
            if (targets.Length > 0) {
                targets = targets.OrderBy(t => (t.transform.position - _ship.transform.position).magnitude).ToArray();
                _target = targets[Random.Range(0, 2)].transform;
            }
        }
        if (_target == null) {
            Debug.Log("no target found!");
        } else {
            _targetShip = _target.GetComponent<Ship>();
        }
       

        RndAttackParameters();
        _state = BotState.Hunt;
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private void UpdateMarkerVisible() {
        ChangeMarkerVisibility(ArShootAssist.CheckTargetVisible(_ship));
    }

    private void Update() {
        UpdateMarkerVisible();

        if (_state == BotState.Respawn || _ship == null) {
            return;
        }

        if (!_ship.gameObject.activeSelf) {
            return;
        }

        if (_target != null) {
            _distToTarget = CalculateDist();
        } else {
            _distToTarget = 100000;
            FindTarget();
        }

        _shipSpeed = _ship.GetSpeedPercent();

        if (_state == BotState.Hunt) {
            Hunt();
        } else if (_state == BotState.Shoot) {
            Shoot();
        } else if (_state == BotState.Evade) {
            Evade();
        }
    }

    private void Hunt() {
        if (_distToTarget < _startShootDistance) {
            _state = BotState.Shoot;
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(AttackCoroutine());
            _cachedRandomVector = Random.insideUnitSphere;

            return;
        }

        Vector3 dir = (_target.position + _cachedRandomVector * ShipsFactory.ShipStatsGeneralConfig.BotRandomChasePointDelta -
                       _ship.transform.position).normalized;

        //todo add smooth lerp rotation
        if (Vector3.Angle(_ship.transform.forward, dir) > 60 + (1 - _cachedRandomValue) * 30 &&
            _shipSpeed > ShipsFactory.ShipStatsGeneralConfig.BotHuntDesirableSpeed + _cachedRandomValue / 5) {
            _ship.Slowdown();
        } else if (_shipSpeed < 1) {
            _ship.Accelerate();
        }

        RotateShip(dir);
    }

    private void Shoot() {
        if (_distToTarget < ShipsFactory.ShipStatsGeneralConfig.StartEvadeDistance) {
            _state = BotState.Evade;
            StopCoroutine(_attackCoroutine);
            _cachedRandomVector = Random.insideUnitSphere;
            return;
        }

        Vector3 predictedPointToShoot = ArShootAssist.CalculateLaserTrajectory(_targetShip, _ship);
        Vector3 shootDelta = CalculateShootDelta();
        Vector3 summedShootPoint = predictedPointToShoot + shootDelta;
        
        if (Vector3.Angle(_ship.transform.forward, summedShootPoint - _ship.transform.position) <
            ShipsFactory.ShipStatsGeneralConfig.BotShootAngle) {
            if (_isFiringSecond) {
                _ship.FireSecond(summedShootPoint);
            } else {
                _ship.FirePrime(summedShootPoint);
            }
        }

        if (_shipSpeed < ShipsFactory.ShipStatsGeneralConfig.BotShootDesirableSpeed) {
            _ship.Accelerate();
        } else {
            _ship.Slowdown();
        }

        Vector3 dir = _target.position - _ship.transform.position;
        RotateShip(dir);
    }

    private Vector3 CalculateShootDelta() {
        ShipStatsGeneralConfig cnfg = ShipsFactory.ShipStatsGeneralConfig;
        bool isPlayerTarget = false;
        if (_target.GetComponent<Ship>() != null) {
            isPlayerTarget = !_target.GetComponent<Ship>().GetOwner().PlayerData.isBot;
        }

        float chance = isPlayerTarget ? cnfg.BotRandomShootChanceAgainsPlayer : cnfg.BotRandomShootChance;
        bool isShootingWithDelta = Random.Range(0, 1f) < chance;
        if (!isShootingWithDelta) {
            return Vector3.zero;
        }

        return _cachedRandomShootVector * (isPlayerTarget ? cnfg.BotRandomShootDeltaAgainsPlayer : cnfg.BotRandomShootDelta);
    }

    private IEnumerator AttackCoroutine() {
        yield return new WaitForSeconds(_attackTime);
        _state = BotState.Evade;
        FindTarget();
    }

    private void Evade() {
        if (_distToTarget > _startShootDistance) {
            _state = BotState.Hunt;
            RndAttackParameters();
            return;
        }

        if (_shipSpeed <= ShipsFactory.ShipStatsGeneralConfig.BotEvadeDesirableSpeed) {
            _ship.Accelerate();
            _ship.SetBoost(_ship.GetShieldPercent() < 0.5f);
        }

        Vector3 dir = _ship.transform.position + _cachedRandomVector * ShipsFactory.ShipStatsGeneralConfig.BotRandomEvadePointDelta -
                      _target.position;
        RotateShip(dir);
    }

    private void RotateShip(Vector3 to) {
        to = to.normalized;
        float distFromCenter = Vector3.Distance(_ship.transform.position, Vector3.zero);
        if (distFromCenter > MainConfigTable.Instance.MainGameConfig.FightRadius) {
            float rotPercent = (distFromCenter - MainConfigTable.Instance.MainGameConfig.FightRadius) / 100;
            Vector3 dirToCenter = -_ship.transform.position.normalized;
            to = Vector3.Lerp(to, dirToCenter, rotPercent);
        }

        Quaternion finRotation = Quaternion.LookRotation(to);
        _ship.RotateToQ = finRotation;
    }

    private void RndAttackParameters() {
        _state = BotState.Hunt;
        var cnfg = ShipsFactory.ShipStatsGeneralConfig;
        _startShootDistance = Random.Range(cnfg.BotMinStartShootDistance, cnfg.BotMaxStartShootDistance);
        _attackTime = Random.Range(cnfg.BotMinAttackTime, cnfg.BotMaxAttackTime);
        _cachedRandomValue = Random.Range(0, 1f);
        _cachedRandomShootVector = Random.insideUnitSphere;
        _isFiringSecond = Random.Range(0, 1f) < cnfg.BotChanceToShootWithSecondGun;
    }

    private float CalculateDist() => Vector3.Magnitude(_target.position - _ship.transform.position - (_state == BotState.Evade
        ? _cachedRandomVector * ShipsFactory.ShipStatsGeneralConfig.BotRandomEvadePointDelta
        : Vector3.zero));

    private void StartRespawning(AbstractPilot _, AbstractPilot __) {
        GameManager.Instance.RespawnManager.MinusPoint(_playerData.Team);
        _respawnCoroutine = StartCoroutine(RespawnCoroutine());
    }

    private void OnTakingDamage(AbstractPilot from) {
        if (_state != BotState.Evade && _ship.GetShieldPercent() < 0.1f) {
            _target = from.Ship.transform;
            _state = BotState.Evade;
            _cachedRandomValue = Random.Range(0, 1f);
            StopCoroutine(_attackCoroutine);
        }
    }

    private IEnumerator RespawnCoroutine() {
        _state = BotState.Respawn;
        yield return new WaitForSeconds(MainConfigTable.Instance.MainGameConfig.BotRespawnTime);
        if (!_isActive) {
            yield break;
        }

        RespawnShip();
        FindTarget();
    }

    enum BotState {
        Hunt,
        Shoot,
        Evade,
        Respawn
    }

    private void OnDrawGizmosSelected() {
        if (_ship == null) {
            return;
        }

        Debug.Log(_state);
        if (_target != null) {
            Gizmos.color = PlayerData.Team == Team.Blue ? Color.blue : Color.red;
            Gizmos.DrawLine(_ship.transform.position, _target.position);
        }
    }
}