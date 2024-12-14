using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour {
    private Transform _camera;

    private void Start() {
        _camera = Camera.main.transform;
    }

    private void Update() {
        transform.rotation = Quaternion.LookRotation(_camera.forward * -1, _camera.up);
    }
}