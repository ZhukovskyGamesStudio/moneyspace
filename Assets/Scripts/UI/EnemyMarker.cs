using System;
using UnityEngine;

public class EnemyMarker : MonoBehaviour {

    [SerializeField]
    private float _minScale, _maxScale, _minUpShift, _maxUpShift, _minDistance, _maxDistance;
    private Transform _trackingObject;
    private float _currentUpShift;

    public void SetTarget(Transform trackingObject) {
        _trackingObject = trackingObject;
    }

    private void LateUpdate() {
        UpdateView();
        Vector3 pos = Camera.main.WorldToScreenPoint(_trackingObject.position);
        pos.z = 0;
        pos.y += _currentUpShift;
        transform.position = pos;
    }

    private void UpdateView() {
        float distance = (Camera.main.transform.position - _trackingObject.position).magnitude;
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