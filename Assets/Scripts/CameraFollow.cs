using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow Instance;
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _stickTarget;

    private Transform _transform;
    private Vector3 _shakeShift;

    [SerializeField]
    private float _followLerp, _shakeDecceleration;

    private void Awake() {
        Instance = this;
        _transform = transform;
    }

    private void LateUpdate() {
        Vector3 targetPos = _target.position;
        //targetPos.y = Mathf.Lerp(targetPos.y, _stickTarget.position.y, 0.5f);
        Vector3 position = _transform.position;
        //targetPos.z = position.z;
        position = Vector3.Lerp(position, targetPos + _shakeShift, _followLerp);
        _transform.position = position;
        if (_shakeShift.magnitude > 0) {
            _shakeShift *= _shakeDecceleration;
        }
    }

    public void RecoilShake(Vector3 shootDir, float force) {
        _shakeShift += -shootDir * force;
    }
}