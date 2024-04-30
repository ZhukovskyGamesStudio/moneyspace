using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class Ship : MonoBehaviour {
    [SerializeField]
    private float _shipMaxSpeed = 3;

    [SerializeField]
    private float _accelerationSpeed = 4f;

    [SerializeField]
    private float _horRotation = 1, _vertRotation = 1;
    
    [SerializeField]
    private float _minVertAngle = 10, _minHorAngle = 10;

    [SerializeField]
    private float _modelRotation = 30f;
    
    [SerializeField]
    private float _modelMovement = 30f;

    [SerializeField]
    private Transform _model;

    private float _shipSpeed = 0;

    private float _rotationSpeed = 0;

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
        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);

        if (Mathf.Abs(rotVector.x) < _minVertAngle) {
            rotVector.x = 0;
        }

        if (Mathf.Abs(rotVector.y) < _minHorAngle) {
            rotVector.y = 0;
        }

        rotVector += TrySideRotate();
        Vector3 rotDistance = rotVector * _vertRotation * Time.fixedDeltaTime;
        transform.rotation *= Quaternion.Euler(rotDistance);
        Vector3 modelRotVector = new Vector3(-shift.y * _vertRotation, 0, -shift.x * _vertRotation) * _modelRotation;
        _model.localRotation = Quaternion.Euler(modelRotVector);
        _model.localPosition = shift * _modelMovement;
    }

    private Vector3 TrySideRotate() {
        Vector3 sideRot = Vector3.zero;
        if (Input.GetKey(KeyCode.E)) {
            sideRot += Vector3.back;
        }

        if (Input.GetKey(KeyCode.Q)) {
            sideRot += Vector3.forward;
        }

        return sideRot * _horRotation;
    }

    private void FlyForward() {
        transform.position += transform.forward * _shipSpeed * Time.fixedDeltaTime;
    }
}