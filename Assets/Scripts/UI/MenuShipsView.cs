using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipsView : MonoBehaviour {
    [SerializeField]
    private float _spacing = 15;

    [SerializeField]
    private float _upgradePosShift = 7;

    [SerializeField]
    private float _moveTime = 1, _moveToUpgradeTime = 0.5f;

    [SerializeField]
    private Vector3 _startingLookDirection, _upgradePosDirection;

    [Space(20)]
    [SerializeField]
    private Transform _rotationPrefab, _shipsHolder;

    private List<GameObject> _ships = new List<GameObject>();
    private List<Vector3> _poses = new List<Vector3>();
    private Coroutine _moveCoroutine, _upgradeShipCoroutine;
    private Vector3 _startingPos;
    private int _curInt;
    private Vector3 _startingUpgradePos;
    private bool _inUpgradePos;

    public void Init() {
        _startingPos = _shipsHolder.position;
        CreateShips();
    }

    private void CreateShips() {
        for (int index = 0; index < ShipsFactory.Ships.Count; index++) {
            ShipConfig cnfg = ShipsFactory.Ships[index];
            GameObject ship = Instantiate(cnfg.ModelPrefab, _shipsHolder);
            ship.gameObject.layer = LayerMask.NameToLayer("BlueTeam");
            Transform tr = ship.transform;
            tr.rotation = _rotationPrefab.rotation;
            tr.position += Vector3.right * _spacing * index + Vector3.up * cnfg.UpperShiftOnShowcase;
            _ships.Add(ship);
            _poses.Add(tr.localPosition);
        }
    }

    public void SetPos(int curSelectedShip) {
        _curInt = curSelectedShip;
        Vector3 target = _shipsHolder.position - Vector3.right * _curInt * _spacing;
        _shipsHolder.position = target;
    }

    public void Move(bool isRight) {
        if (_moveCoroutine != null) {
            StopCoroutine(_moveCoroutine);
        }
        
        if (_inUpgradePos) {
            ToggleUpgradePos(false);
        }
        _curInt += isRight ? 1 : -1;
        SlideAllShips();
    }

    private void SlideAllShips() {
        if (_moveCoroutine != null) {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(ShipsMoveCoroutine(_startingPos - Vector3.right * _curInt * _spacing));
    }

    private void MoveShipToUpgradePos() {
        if (_upgradeShipCoroutine != null) {
            StopCoroutine(_upgradeShipCoroutine);
        }

        _startingUpgradePos = _poses[_curInt];
        Vector3 targetPosShift = _startingUpgradePos + Vector3.left * _upgradePosShift;
        _upgradeShipCoroutine = StartCoroutine(UpgradeShipCoroutine(targetPosShift, _upgradePosDirection));
    }

    private void MoveShipBackFromUpgradePos() {
        if (_upgradeShipCoroutine != null) {
            StopCoroutine(_upgradeShipCoroutine);
        }

        _upgradeShipCoroutine = StartCoroutine(UpgradeShipCoroutine(_startingUpgradePos, _startingLookDirection));
    }

    public void ToggleUpgradePos(bool isOn) {
        if (isOn && ! _inUpgradePos) {
            _inUpgradePos = true;
            MoveShipToUpgradePos();
        } else if (_inUpgradePos) {
            _inUpgradePos = false;
            MoveShipBackFromUpgradePos();
        }
    }

    private IEnumerator ShipsMoveCoroutine(Vector3 target) {
        Vector3 startingPos = _shipsHolder.position;
        float curTime = 0;
        while (curTime < _moveTime) {
            curTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            _shipsHolder.position = Vector3.Lerp(startingPos, target, curTime / _moveTime);
        }
    }

    private IEnumerator UpgradeShipCoroutine(Vector3 target, Vector3 targetRotation) {
        Transform curShip = _ships[_curInt].transform;
        Vector3 startingPos = curShip.localPosition;
        Quaternion startingRotation = curShip.localRotation;
        Quaternion targetRot = Quaternion.Euler(targetRotation);
        float curTime = 0;
        while (curTime < _moveToUpgradeTime) {
            curTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            curShip.localPosition = Vector3.Slerp(startingPos, target, curTime / _moveToUpgradeTime);
            curShip.localRotation = Quaternion.Slerp(startingRotation, targetRot, curTime / _moveToUpgradeTime);
        }
    }
}