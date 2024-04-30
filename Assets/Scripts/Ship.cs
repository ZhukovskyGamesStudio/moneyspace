using System;
using UnityEngine;

public class Ship : MonoBehaviour {
    [SerializeField]
    private float _shipSpeed = 3;

    [SerializeField]
    private float _rotationLerp = 0.1f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }

    private void FixedUpdate() {
        Vector3 dir = GetDirection();
        RotateForward(dir);
        //Camera.main.transform.forward = Vector3.Lerp(Camera.main.transform.forward, dir, _rotationLerp);
        FlyForward();
    }

    private Vector3 GetDirection() {
        Vector3 screenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        return ray.GetPoint(10000);
    }

    private void RotateForward(Vector3 dir) {
        var targetRot = Quaternion.LookRotation(dir, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _rotationLerp);
    }

    private void FlyForward() {
        transform.position += transform.forward * _shipSpeed * Time.fixedDeltaTime;
    }
}