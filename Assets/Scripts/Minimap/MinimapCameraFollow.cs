using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCameraFollow : MonoBehaviour {
    [SerializeField]
    private Vector3 _cameraShift;

    [SerializeField]
    private bool _alignWithMainCam;

    public static MinimapCameraFollow Instance;
    private Transform _transform;
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private RawImage _minimapImage;

    [SerializeField]
    private Camera _minimapCamera, _mainCamera;

    [SerializeField]
    private RenderTexture _renderTexture;

    [SerializeField]
    private Canvas _canvas;

    private void Awake() {
        Instance = this;
        _transform = transform;
      
    }

    public IEnumerator PrepareMinimap() {
        //_mainCamera.gameObject.SetActive(false);
        //_canvas.gameObject.SetActive(false);
        _minimapCamera.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        //_minimapImage.texture = texture;
        yield return new WaitForEndOfFrame();
        _minimapCamera.gameObject.SetActive(false);
        //_mainCamera.gameObject.SetActive(true);
        //_canvas.gameObject.SetActive(true);
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