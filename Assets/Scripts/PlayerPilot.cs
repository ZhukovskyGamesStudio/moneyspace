using System;
using UnityEngine;

public class PlayerPilot : MonoBehaviour {
    [SerializeField]
    private IShip _ship;

    [SerializeField]
    private bool _isMouseTarget = false;

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            _ship.Accelerate();
        }

        if (Input.GetKey(KeyCode.S)) {
            _ship.Slowdown();
        }

        if (Input.GetMouseButtonDown(0)) {
            FirePrime();
        }

        if (Input.GetMouseButtonDown(1)) {
            FireSecond();
        }

        Vector2 shift = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);
        _ship.RotateForward(rotVector + TrySideRotate());
    }

    private void FirePrime() {
        Vector3 screenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
       
        Vector3 point = ray.GetPoint(10000);
        _ship.FirePrime(point);
    }
    
    private void FireSecond() {
        Vector3 screenPos = _isMouseTarget ? Input.mousePosition : new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Vector3 point = ray.GetPoint(10000);
        _ship.FireSecond(point);
    }

    private Vector3 TrySideRotate() {
        Vector3 sideRot = Vector3.zero;
        if (Input.GetKey(KeyCode.E)) {
            sideRot += Vector3.back;
        }

        if (Input.GetKey(KeyCode.Q)) {
            sideRot += Vector3.forward;
        }

        return sideRot;
    }
}