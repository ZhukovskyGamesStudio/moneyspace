using System;
using UnityEngine;

public class EnemyMarker : MonoBehaviour {
    private Transform _trackingObject;

    public void SetTarget(Transform trackingObject) {
        _trackingObject = trackingObject;
    }

    private void Update() {
        transform.position = Camera.main.WorldToScreenPoint(_trackingObject.position);
    }
}