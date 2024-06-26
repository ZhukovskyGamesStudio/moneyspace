using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipDetectZone : MonoBehaviour {
    public static ShipDetectZone Instance;
    private void Awake() {
        Instance = this;
    }

    private List<Ship> _shipsInsideDetection = new List<Ship>();
    private Ship _playerShip;
    
    public void SetPlayerShip(Ship playerShip) {
        _playerShip = playerShip;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody == null) {
            return;
        }
        Ship collShip = other.attachedRigidbody.GetComponent<Ship>();
        if (collShip == null || collShip == _playerShip) {
            return;
        }
        

        if (_shipsInsideDetection.Contains(collShip)) {
            return;
        } else {
            _shipsInsideDetection.Add(collShip);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.attachedRigidbody == null) {
            return;
        }
        Ship collShip = other.attachedRigidbody.GetComponent<Ship>();
        if (collShip == null) {
            return;
        }

        StartCoroutine(RemoveFromList(collShip));
    }

    private IEnumerator RemoveFromList(Ship ship) {
        yield return new WaitForSeconds(2);
        if (_shipsInsideDetection.Contains(ship)) {
            _shipsInsideDetection.Remove(ship);
        }
    }

    public bool HasShipsToTarget() {
        if (_shipsInsideDetection.Count == 0) {
            return false;
        }

        TryClearShipsFromDestroyed();
        return _shipsInsideDetection.Count != 0;
    }

    public Ship GetClosestShip() {
        return _shipsInsideDetection.OrderBy(s => (s.transform.position - transform.position).sqrMagnitude).First();
    }

    private void TryClearShipsFromDestroyed() {
        _shipsInsideDetection = _shipsInsideDetection.FindAll(s => s != null);
    }
}