using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : IShip {
    [SerializeField]
    private int _shipMaxHp = 100;

    [SerializeField]
    private int _maxShied = 100;

    [SerializeField]
    private int _shiedRepairSpeed = 10;

    [SerializeField]
    private float _overheatFromShoot = 0.1f;

    [SerializeField]
    private float decreaseOverheatSpeed = 0.1f;

    private float _overheat;
    private int _hp;
    private float _shield;

    [SerializeField]
    private float _shipMaxSpeed = 3;

    [SerializeField]
    private float _accelerationSpeed = 4f;

    [SerializeField]
    private float _horRotation = 1, _vertRotation = 1;

    [SerializeField]
    private float _verticalMaxRotationSpeed = 10, _horizontalMaxRotationSpeed = 10;

    [SerializeField]
    private float _modelRotation = 30f;

    [SerializeField]
    private float _modelMovement = 30f;

    [SerializeField]
    private Transform _model;

    [SerializeField] private ShipThrust _shipThrust;

    [SerializeField]
    private Explosion _explosion;

    [SerializeField]
    private List<LaserCanon> _primeCanons = new List<LaserCanon>();

    [SerializeField]
    private List<LaserCanon> _secondCanons = new List<LaserCanon>();

    [SerializeField]
    private Transform _cameraFollowTarget;

    private float _shipSpeed = 0;

    private float _rotationSpeed = 0;

    private bool _recoil = false;

    private Dictionary<PlayerData, int> _damageDealers = new Dictionary<PlayerData, int>();

    private void Start() {
        Respawn();
        //Cursor.visible = false;
    }

    private void FixedUpdate() {
        //Camera.main.transform.forward = Vector3.Lerp(Camera.main.transform.forward, dir, _rotationLerp);
        FlyForward();
        DecreaseOverheat();
        RepairShield();
        _shipThrust.SetThrustLight(_shipSpeed/_shipMaxSpeed);
    }

    private void RepairShield() {
        if (_shield >= _maxShied) {
            return;
        }

        _shield += _shiedRepairSpeed * Time.fixedDeltaTime;
        if (_shield > _maxShied) {
            _shield = _maxShied;
        }
    }

    private void DecreaseOverheat() {
        if (_overheat <= 0) {
            return;
        }

        _overheat -= decreaseOverheatSpeed * Time.fixedDeltaTime;
        if (_overheat <= 0) {
            _overheat = 0;
            _isOverheated = false;
        }
    }

    public override void RotateBy(Vector3 rotVector) {
        /*if (Mathf.Abs(rotVector.x) < _minVertAngle) {
            rotVector.x = 0;
        }

        if (Mathf.Abs(rotVector.y) < _minHorAngle) {
            rotVector.y = 0;
        }*/
        MoveModel(rotVector);
        rotVector.x = Mathf.Clamp(rotVector.x, -_verticalMaxRotationSpeed, _verticalMaxRotationSpeed);
        rotVector.y = Mathf.Clamp(rotVector.y, -_horizontalMaxRotationSpeed, _horizontalMaxRotationSpeed);
        Vector3 rotDistance = new Vector3(rotVector.x * _vertRotation, rotVector.y * _vertRotation, rotVector.z * _horRotation) *
                              Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotDistance);
    }

    public override float GetSpeedPercent() {
        return _shipSpeed / _shipMaxSpeed;
    }

    public override float GetOverheatPercent() {
        return _overheat;
    }

    public override float GetHpPercent() {
        return _hp / (_shipMaxHp + 0f);
    }

    public override float GetShieldPercent() {
        return _shield / (_maxShied + 0f);
    }

    private void MoveModel(Vector3 rotVector) {
        Vector3 modelRotVector = new Vector3(rotVector.x * _vertRotation, 0, -rotVector.y * _vertRotation) * _modelRotation;
        _model.localRotation = Quaternion.Euler(modelRotVector);
        Vector3 modelShift = new Vector3(rotVector.y, -rotVector.x, 0);
        modelShift.z = 0;
        _model.localPosition = modelShift * _modelMovement;

        /*
        
        Vector2 shift = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);

       

        rotVector += TrySideRotate();
        Vector3 rotDistance = rotVector * _vertRotation * Time.fixedDeltaTime;
        transform.rotation *= Quaternion.Euler(rotDistance);
        Vector3 modelRotVector = new Vector3(-shift.y * _vertRotation, 0, -shift.x * _vertRotation) * _modelRotation;
        _model.localRotation = Quaternion.Euler(modelRotVector);
        _model.localPosition = shift * _modelMovement;*/
    }

    private void FlyForward() {
        transform.position += transform.forward * _shipSpeed * Time.fixedDeltaTime;
    }

    public override void Accelerate() {
        _shipSpeed += _accelerationSpeed;
        _shipSpeed = Mathf.Clamp(_shipSpeed, 0, _shipMaxSpeed);
    }

    public override void Slowdown() {
        _shipSpeed -= _accelerationSpeed;
        _shipSpeed = Mathf.Clamp(_shipSpeed, 0, _shipMaxSpeed);
    }

    public override void FirePrime(Vector3 target) {
        if (_recoil || _isOverheated || !gameObject.activeSelf) {
            return;
        }

        _overheat += _overheatFromShoot;
        if (_overheat >= 1) {
            _overheat = 1;
            _isOverheated = true;
        }

        StartCoroutine(RecoilCoroutine());
        foreach (var VARIABLE in _primeCanons) {
            VARIABLE.Shoot(target, _owner);
        }
    }

    private bool _isOverheated = false;

    private IEnumerator RecoilCoroutine() {
        _recoil = true;
        yield return new WaitForSeconds(0.3f);
        _recoil = false;
    }

    public override void FireSecond(Vector3 target) {
        if (!gameObject.activeSelf) {
            return;
        }

        foreach (var VARIABLE in _secondCanons) {
            VARIABLE.Shoot(target, _owner);
        }
    }

    public override void TakeDamage(int amount, PlayerData from) {
        _shield -= amount;
        if (_shield >= 0) {
            return;
        }

        _hp -= Mathf.RoundToInt(-_shield);
       
        if (_damageDealers.ContainsKey(from)) {
            _damageDealers[from] += amount;
        } else {
            _damageDealers.Add(from, amount);
        }

        if (_hp < 0 && gameObject.activeSelf) {
            _hp = 0;
            _owner.Deaths++;
            from.Kills++;
            foreach (var kvp in _damageDealers) {
                if (kvp.Key != from) {
                    kvp.Key.Assists++;
                }
            }

            OnDestroyed?.Invoke(_owner, from);
            Explode();
        }
    }

    public override void Respawn() {
        _shipSpeed = _shipMaxSpeed / 2;
        _hp = _shipMaxHp;
        _overheat = 0;
    }

    public override Transform GetCameraFollowTarget() {
        return _cameraFollowTarget;
    }

    private void Explode() {
        Instantiate(_explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}