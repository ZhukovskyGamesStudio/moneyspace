using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipsView : MonoBehaviour {
    [SerializeField]
    private float _spacing = 15;

    [SerializeField]
    private float _moveTime = 1;

    [SerializeField]
    private Transform _rotationPrefab, _shipsHolder;

    private List<IShip> _ships = new List<IShip>();
    private Coroutine _moveCoroutine;
    private Vector3 _startingPos;
    private int _curInt;


    public void Init() {
        _startingPos = _shipsHolder.position;
        CreateShips();
    }

    public void CreateShips() {
        for (int index = 0; index < ShipsFactory.Ships.Count; index++) {
            ShipConfig cnfg = ShipsFactory.Ships[index];
            IShip ship = Instantiate(cnfg.Prefab, _shipsHolder);
            ship.enabled = false;
            ship.transform.rotation = _rotationPrefab.rotation;
            ship.transform.position += Vector3.right * _spacing * index;
            _ships.Add(ship);
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

        _curInt += isRight ? 1 : -1;
        _moveCoroutine = StartCoroutine(ShipsMoveCoroutine());
    }

    private IEnumerator ShipsMoveCoroutine() {
        Vector3 startingPos = _shipsHolder.position;
        Vector3 target = _startingPos - Vector3.right * _curInt * _spacing;
        float curTime = 0;
        while (curTime < _moveTime) {
            curTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            _shipsHolder.position = Vector3.Lerp(startingPos, target, curTime / _moveTime);
        }
    }
}