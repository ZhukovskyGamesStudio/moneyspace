using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotPilot : AbstractPilot {
    [SerializeField]
    private float _respawnTime = 10f;

    private Transform _target;

    private BotState _state;
    private float _distToTarget;
    private float _shipSpeed;

    private float _maxShootDistance = 100;
    private float _shootDistanceRandomness = 25;

    private Coroutine _shootCoroutine;

    //attack random parameters
    private float _shootDistance;
    private float _desirableSpeed;
    private float _attackTime;

    private EnemyMarker _marker;

    public override void Init() {
        GetShip();
        CreateMarker();
        RespawnShip();
    }

    public override void Activate() {
        FindTarget();
    }

    protected override void GetShip() {
        base.GetShip();
        _ship.OnDestroyed += StartRespawning;
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

        _marker.SetTarget(_ship.transform);
        _ship._visibleChecker.OnVisibleAction += () => { ChangeMarkerVisibility(true); };
        _ship._visibleChecker.OnInvisibleAction += () => { ChangeMarkerVisibility(false); };
    }

    private void ChangeMarkerVisibility(bool isActive) {
        if (_marker)
            _marker.gameObject.SetActive(isActive);
    }

    private void FindTarget() {
        _target = GameObject.FindWithTag(_playerData.Team == Team.Blue ? "Red" : "Blue").transform;
        if (_target == null) {
            Debug.Log("no target found!");
        }

        RndAttackParameters();
        _state = BotState.Hunt;
        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    private void Update() {
        if (_state == BotState.Respawn) {
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
        if (_distToTarget < _shootDistance) {
            _state = BotState.Shoot;
            _shootCoroutine = StartCoroutine(ShootCoroutine());
            return;
        }

        Vector3 dir = _target.position - _ship.transform.position;
        if (Vector3.Angle(_ship.transform.forward, dir) > 60 && _shipSpeed > 0.3) {
            _ship.Slowdown();
        } else if (_shipSpeed < 1) {
            _ship.Accelerate();
        }

        RotateShip(dir);
    }

    private void Shoot() {
        if (_distToTarget < 20) {
            _state = BotState.Evade;
            StopCoroutine(_shootCoroutine);
            return;
        }

        _ship.FirePrime(_target.position);

        if (_shipSpeed < _desirableSpeed) {
            _ship.Accelerate();
        } else {
            _ship.Slowdown();
        }

        Vector3 dir = _target.position - _ship.transform.position;
        RotateShip(dir);
    }

    private IEnumerator ShootCoroutine() {
        yield return new WaitForSeconds(_attackTime);
        _state = BotState.Evade;
    }

    private void Evade() {
        if (_distToTarget > _shootDistance) {
            _state = BotState.Hunt;
            RndAttackParameters();
            return;
        }

        if (_shipSpeed < 1) {
            _ship.Accelerate();
        }

        Vector3 dir = _ship.transform.position - _target.position;
        RotateShip(dir);
    }

    private void RotateShip(Vector3 to) {

        if (Vector3.Distance(transform.position, Vector3.zero) > GameManager.FightRadius) {
            to = Vector3.Lerp(to, Vector3.zero, 0.5f);
        }
        
        Vector3 rotVector = Quaternion.FromToRotation(_ship.transform.forward, to).eulerAngles;

        // Vector3 from = _ship.transform.forward;
        // Vector3 rotVector = new Vector3(Vector3.SignedAngle(from, to, -_ship.transform.up), Vector3.SignedAngle(from, to, _ship.transform.right));
        _ship.RotateForward(rotVector);
    }

    private void RndAttackParameters() {
        _state = BotState.Hunt;
        _shootDistance = _maxShootDistance + Random.Range(-1, 1f) * _shootDistanceRandomness;
        _desirableSpeed = Random.Range(0.2f, 0.8f);
        _attackTime = Random.Range(3, 10f);
    }

    private float CalculateDist() => Vector3.Magnitude(_target.position - _ship.transform.position);

    private void StartRespawning(PlayerData _, PlayerData __) {
        GameManager.Instance.RespawnManager.MinusPoint(_playerData.Team);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        _state = BotState.Respawn;
        yield return new WaitForSeconds(_respawnTime);
        RespawnShip();
    }

    enum BotState {
        Hunt,
        Shoot,
        Evade,
        Respawn
    }
}