using System;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class Ship : MonoBehaviour {
    [SerializeField]
    private float _shipMaxSpeed = 3;

    [SerializeField]
    private float _rotationLerp = 0.1f;

    [SerializeField]
    private float _minAngle = 10f;

    [SerializeField]
    private float _accelerationSpeed = 4f;

    private float _shipSpeed = 0;

    [SerializeField]
    private float _rotationAcceleration = 40f;

    private float _rotationSpeed = 0;

    [SerializeField]
    private Transform _model;

    [SerializeField]
    private bool _isModelRotating;
    [SerializeField]
    private float _modelRotateMultiplier = 1/3f;
    
    [SerializeField]
    private float _justFloat = 30f;
    
    [SerializeField]
    private float _horRotation = 1, _vertRotation = 1;
    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        _shipSpeed = _shipMaxSpeed / 2;
        //Cursor.visible = false;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            _shipSpeed += _accelerationSpeed;
        }

        if (Input.GetKey(KeyCode.S)) {
            _shipSpeed -= _accelerationSpeed;
        }

        _shipSpeed = Mathf.Clamp(_shipSpeed, -_shipMaxSpeed / 2, _shipMaxSpeed);
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
        return ray.GetPoint(10000) - transform.position;
    }

    private void RotateForward(Vector3 dir) {
        Vector2 shift = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        /*
        Quaternion targetRot = Quaternion.LookRotation(dir, transform.up);
        float rotSpeed = _rotationAcceleration * Mathf.Abs(_shipSpeed) / _shipMaxSpeed;
        

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _rotationLerp * rotSpeed);
        
        float upAngle = Vector3.SignedAngle(_model.forward, dir, transform.right);
        float leftAngle = Vector3.SignedAngle(_model.forward, dir, transform.up);

        if (_isModelRotating) {
            Quaternion targetModelRot = Quaternion.Euler(upAngle,0,-leftAngle/2);
            targetModelRot *= Quaternion.AngleAxis(-_justFloat * _modelRotateMultiplier, dir);
            //targetModelRot *= Quaternion.AngleAxis(-upAngle/2, _model.forward );
            //_model.rotation = Quaternion.Lerp(_model.rotation, targetModelRot.normalized, _rotationLerp * rotSpeed * 2);
            
            _model.localRotation = Quaternion.Lerp( _model.localRotation, targetModelRot, _rotationLerp * rotSpeed*2);
        }*/
        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);
        rotVector += TrySideRotate();
        Vector3 rotDistance = rotVector * _vertRotation * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotDistance);
        Vector3 modelRotVector = new Vector3(-shift.y * _vertRotation, 0, -shift.x * _vertRotation )* _justFloat;
        _model.localRotation = Quaternion.Euler(modelRotVector);
    }

    private Vector3 TrySideRotate() {
        Vector3 sideRot = Vector3.zero;
        if (Input.GetKey(KeyCode.D)) {
            sideRot += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A)) {
            sideRot += Vector3.back;
        }

        return sideRot * _horRotation;
    }

    private void FlyForward() {
        transform.position += transform.forward * _shipMaxSpeed * Time.fixedDeltaTime;
    }
}