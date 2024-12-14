using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour {
    [SerializeField]
    private Vector3 _cameraShift;

    [SerializeField]
    private bool _alignWithMainCam;

    public static MinimapCameraFollow Instance;
    private Transform _transform;
    [SerializeField]
    private Transform _target;

    private void Awake() {
        Instance = this;
        _transform = transform;
    }

    public void SetTarget(Transform target) {
        _target = target;
    }

    private void LateUpdate() {
        TryChangePos();
    }

    private void TryChangePos() {
        if (_target == null) {
            return;
        }

        Vector3 targetPos = _target.position;
        _transform.position = targetPos + _cameraShift;
        transform.rotation = Quaternion.Euler(90, 0, _target.eulerAngles.z);
    }
}