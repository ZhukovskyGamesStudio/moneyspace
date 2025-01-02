using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMarker : MonoBehaviour {

    [SerializeField]
    private float _minScale, _maxScale, _minUpShift, _maxUpShift, _minDistance, _maxDistance;

    [SerializeField]
    private Image _image;
    private Transform _trackingObject;
    private float _currentUpShift;
    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;
    }

    public void SetTarget(Transform trackingObject) {
        _trackingObject = trackingObject;
    }

    public void SetActiveness(bool isActive) {
        if (_image.enabled != isActive) {
            _image.enabled = isActive;
        }
    }

    private void LateUpdate() {
        if (!_image.enabled) {
            return;
        }
        UpdateView();
        Vector3 pos = _mainCamera.WorldToScreenPoint(_trackingObject.position);
        pos.z = 0;
        pos.y += _currentUpShift;
        transform.position = pos;
    }

    private void UpdateView() {
        float distance = (_mainCamera.transform.position - _trackingObject.position).magnitude;
        if (distance < _minDistance) {
            distance = _minDistance;
        }

        float percent = distance / _maxDistance;
        if (percent > 1) {
            percent = 1;
        }

        percent = 1 - percent;

        transform.localScale = Vector3.one * Mathf.Lerp(_minScale, _maxScale, percent);
        _currentUpShift = Mathf.Lerp(_minUpShift , _maxUpShift, percent);
    }
}