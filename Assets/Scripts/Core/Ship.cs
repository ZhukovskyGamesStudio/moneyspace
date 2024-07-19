using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : IShip {
    [SerializeField]
    private Transform _model;

    [SerializeField]
    private ShipThrust _shipThrust;

    [SerializeField]
    private Explosion _explosion;

    [SerializeField]
    private List<LaserCanon> _primeCanons = new List<LaserCanon>();

    [SerializeField]
    private List<LaserCanon> _secondCanons = new List<LaserCanon>();

    [SerializeField]
    private Transform _cameraFollowTarget;

    [SerializeField]
    private Transform _modelContainer;

    [SerializeField]
    private Shield _shield3dView;

    [Header("Sounds")]
    [SerializeField]
    private TypedAudioSource _laserSoundSource;

    [SerializeField]
    private TypedAudioSource _secondLaserSoundSource, _shieldAudioSource, _takeDamageSource, _engineAudioSource;
    
    private bool _recoil = false;
    private bool _isOverheated = false;
    private bool _isBouncing = false;
    private bool _isRespawning = false;
    private bool _isBoostReloading = false;

    private int _hp;
    private float _shipSpeed = 0;
    private float _rotationSpeed = 0;
    private float _shield;
    private float _overheat;
    private float _boost;
    private float _timeCounter;
    private float _MaxTimeForCounter = 15;

    private Quaternion _randomBounceDirection;
    private Dictionary<PlayerData, int> _damageDealers = new Dictionary<PlayerData, int>();
    public float ShipSpeed => _shipSpeed;

    private void Start() {
        if (_owner.PlayerData.isBot) {
            _engineAudioSource.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() {
        if (_isRespawning) {
            return;
        }

        RotateBy(RotateByV);
        RotateTo(RotateToQ);

        if (_isBouncing) {
            RotateTo(_randomBounceDirection);
            RotateTo(_randomBounceDirection);
            Accelerate();
            FlyBackward();
        } else {
            FlyForward();
        }
        _engineAudioSource.SetVolumePercent(_shipSpeed/GetMaxSpeed);

        DecreaseOverheat();
        IncreaseBoost();
        RepairShield();

        _shipThrust.SetThrustLight(_shipSpeed / GetMaxSpeed);

        float maxRadius = MainConfigTable.Instance.MainGameConfig.FightRadius;
        if (Math.Abs(transform.position.x) > maxRadius || Math.Abs(transform.position.y) > maxRadius ||
            Math.Abs(transform.position.z) > maxRadius) {
            Debug.Log($"Вы вылетели за пределы боевой зоны у вас осталось {_MaxTimeForCounter - _timeCounter} секунд что бы вернуться");
            _timeCounter += Time.fixedDeltaTime;

            if (_timeCounter > _MaxTimeForCounter) {
                TakeDamage(100000, _owner);
            }
        } else {
            _timeCounter = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null) {
            return;
        }

        if (_isBouncing || _isRespawning) {
            return;
        }

        //столкновение с лазерами обрабатываются на стороне лазера
        if (rb.GetComponent<LaserBullet>() != null) {
            return;
        }

        TakeDamage(ShipsFactory.ShipStatsGeneralConfig.DamageFromCollision, _owner);
        BounceFromCollision(collision.contacts[0].normal);
    }

    private void BounceFromCollision(Vector3 normal) {
        if (_isBouncing || !gameObject.activeSelf) {
            return;
        }

        _isBouncing = true;
        StartCoroutine(BounceCoroutine(normal));
    }

    public Transform GetTargetLockAnchor => _modelContainer.transform;

    public bool IsRespawning => _isRespawning;

    private IEnumerator BounceCoroutine(Vector3 normal) {
        _randomBounceDirection = Quaternion.Euler(normal);
        yield return new WaitForSeconds(1f);
        _isBouncing = false;
    }

    public void StartRespawnAnimation() {
        StartCoroutine(PlayerRespawnAnimationCoroutine());
    }

    private IEnumerator PlayerRespawnAnimationCoroutine() {
        _isRespawning = true;
        Cursor.lockState = CursorLockMode.Locked;
        float animTime = 0, maxAnimTime = 2;
        while (animTime < maxAnimTime) {
            Vector3 shift = new Vector3(0, -5, -10);
            MoveModel(Vector3.zero, (1 - animTime / maxAnimTime) * shift);
            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        Cursor.lockState = CursorLockMode.Confined;
        _isRespawning = false;
    }

    private void RepairShield() {
        if (_shield >= MaxShied) {
            return;
        }

        _shield += _shipConfig.ShiedRepairSpeed * Time.fixedDeltaTime;
        if (_shield > MaxShied) {
            _shield = MaxShied;
        }
    }

    private void DecreaseOverheat() {
        if (_overheat <= 0) {
            return;
        }

        _overheat -= _shipConfig.DecreaseOverheatSpeed * Time.fixedDeltaTime;
        if (_overheat <= 0) {
            _overheat = 0;
            _isOverheated = false;
        }
    }

    private void IncreaseBoost() {
        _boost += _shipConfig.BoostIncreasePerSecond * Time.fixedDeltaTime;
        if (_boost > 1) {
            _boost = 1;
        }
    }

    public override void RotateBy(Vector3 rotVector) {
        if (rotVector == Vector3.zero) {
            return;
        }

        MoveModel(rotVector, Vector3.zero);

        Vector3 rotDistance = new Vector3(rotVector.x * _shipConfig.VertRotationMultiplier, rotVector.y * _shipConfig.VertRotationMultiplier,
            rotVector.z * _shipConfig.HorRotationMultiplier) * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotDistance);
    }

    public override void RotateTo(Quaternion quaternion) {
        if (quaternion == Quaternion.identity) {
            return;
        }

        TechicalShipParameters tech = ShipsFactory.ShipStatsGeneralConfig.TechicalParams;
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, tech.BotRotationSlerp);
    }

    public override float GetSpeedPercent() {
        return Math.Clamp(_shipSpeed / GetMaxSpeed, 0, 1f);
    }

    public float SideRotationSpeed => _shipConfig.PlayerSideRotationSpeed;

    public override float GetOverheatPercent() {
        return _overheat;
    }

    public override float GetHpPercent() {
        return _hp / (_shipConfig.MaxHp + 0f);
    }

    public override float GetShieldPercent() {
        return _shield / (MaxShied + 0f);
    }

    public override int GetLaserDamage() {
        return ShipsFactory.ShipStatsGeneralConfig.LaserBaseDamage +
               _shipUpgradeData.Attack * ShipsFactory.ShipStatsGeneralConfig.LaserDamagePerPoint;
    }

    private void MoveModel(Vector3 rotVector, Vector3 shift) {
        float lerpStep = _shipConfig.ModelLerp;
        Vector3 modelRotVector =
            new Vector3(rotVector.x * _shipConfig.VertRotationMultiplier, 0, -rotVector.y * _shipConfig.VertRotationMultiplier) *
            _shipConfig.ModelRotation;

        _model.localRotation = Quaternion.Slerp(_model.localRotation, Quaternion.Euler(modelRotVector), lerpStep);

        Vector3 modelShift = new Vector3(rotVector.y, -rotVector.x, 0);
        Vector3 summedShift = modelShift * _shipConfig.ModelMovement + shift;
        _model.localPosition = Vector3.Slerp(_model.localPosition, summedShift, lerpStep);
    }

    private void FlyForward() {
        transform.position += transform.forward * (_shipSpeed * Time.fixedDeltaTime);
    }

    private void FlyBackward() {
        transform.position -= transform.forward * (_shipSpeed * Time.fixedDeltaTime);
    }

    private float GetMaxSpeed => ShipsFactory.ShipStatsGeneralConfig.BaseSpeedMax + _shipUpgradeData.Speed * ShipsFactory.ShipStatsGeneralConfig.SpeedMaxPerPoint;

    public bool IsOverheated => _isOverheated;

    public override void Accelerate() {
        _shipSpeed += _shipConfig.AccelerationSpeed;
        _shipSpeed = Mathf.Clamp(_shipSpeed, 0, GetMaxSpeed);
    }

    public void SetBoost(bool isBoosting) {
        if (_boost > 0 && isBoosting && !_isBoostReloading) {
            _shipSpeed *= _shipConfig.BoostSpeedMultiplier;
            _boost -= (_shipConfig.BoostDecreasePerSecond - _shipConfig.BoostIncreasePerSecond) * Time.deltaTime;
            if (_boost < 0) {
                _boost = 0;
                StartCoroutine(BoostReloadCoroutine());
            }
        } 
    }

    private IEnumerator BoostReloadCoroutine() {
        float delay = 1;
        _isBoostReloading = true;
        yield return new WaitForSeconds(delay);
        _isBoostReloading = false;
    }

    public float GetBoostPercent => _boost;

    public override void Slowdown() {
        _shipSpeed -= _shipConfig.DecelerationSpeed;
        _shipSpeed = Mathf.Clamp(_shipSpeed, 0, GetMaxSpeed);
    }

    public override void SlowdownKeyDown() {
        _shipSpeed -= _shipConfig.AccelerationSpeed;
        _shipSpeed = Mathf.Clamp(_shipSpeed, 0, GetMaxSpeed);
    }

    public override void FirePrime(Vector3 target) {
        if (_recoil || _isOverheated || !gameObject.activeSelf) {
            return;
        }

        _overheat += _shipConfig.OverheatFromShoot;
        if (_overheat >= 1) {
            _overheat = 1;
            _isOverheated = true;
        }

        _laserSoundSource.Play();

        StartCoroutine(RecoilCoroutine(_shipConfig.ShootRecoil));
        foreach (var VARIABLE in _primeCanons) {
            VARIABLE.Shoot(target, ShipsFactory.ShipStatsGeneralConfig.PrimeLaserLifetime, _owner);
        }
    }

    private IEnumerator RecoilCoroutine(float delay) {
        _recoil = true;
        yield return new WaitForSeconds(delay);
        _recoil = false;
    }

    public override void FireSecond(Vector3 target) {
        if (_recoil || _isOverheated || !gameObject.activeSelf) {
            return;
        }

        _overheat += _shipConfig.OverheatFromSecond;
        if (_overheat >= 1) {
            _overheat = 1;
            _isOverheated = true;
        }

        _secondLaserSoundSource.Play();

        StartCoroutine(RecoilCoroutine(_shipConfig.SecondRecoil));
        foreach (var VARIABLE in _secondCanons) {
            VARIABLE.Shoot(target, ShipsFactory.ShipStatsGeneralConfig.SecondLaserLifetime, _owner);
        }
    }

    public override void TakeDamage(int amount, AbstractPilot fromPilot) {
        if (_isRespawning) {
            return;
        }

        PlayerData from = fromPilot != null ? fromPilot.PlayerData : null;
        float damageThroughShield = amount - _shield;
        _shield -= amount;
        if (_shield >= 0) {
            _shield3dView.ShowShield();
            _shieldAudioSource.Play();
            return;
        } else {
            _shield = 0;
        }

        if (damageThroughShield <= 0) {
            return;
        }

        _takeDamageSource.Play();
        _hp -= Mathf.RoundToInt(damageThroughShield);
        OnTakeDamage?.Invoke(fromPilot);
        if (!_owner.PlayerData.isBot) {
            Debug.Log($"Player took {Mathf.RoundToInt(damageThroughShield)} damage!");
        }

        if (from != null) {
            if (_damageDealers.ContainsKey(from)) {
                _damageDealers[from] += amount;
            } else {
                _damageDealers.Add(from, amount);
            }
        }

        if (_hp < 0 && gameObject.activeSelf) {
            _hp = 0;
            _owner.PlayerData.Deaths++;
            if (from != null) {
                from.Kills++;
            }

            foreach (var kvp in _damageDealers) {
                if (kvp.Key != from) {
                    kvp.Key.Assists++;
                }
            }

            OnDestroyed?.Invoke(_owner, fromPilot);
            Explode();
        }
    }

    public override void Respawn() {
        _shipSpeed = GetMaxSpeed / 2;
        _hp = _shipConfig.MaxHp;
        _overheat = 0;
        _isOverheated = false;
        _recoil = false;
        _isBouncing = false;
        _shield3dView.HideShieldInstant();
        _boost = 1;
        _isBoostReloading = false;
    }

    public override Transform GetCameraFollowTarget() {
        return _cameraFollowTarget;
    }

    private void Explode() {
        Instantiate(_explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}