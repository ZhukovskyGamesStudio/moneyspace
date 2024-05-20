using UnityEngine;

public class PlayerPilot : AbstractPilot {
    [SerializeField]
    private bool _isMouseTarget = false;

    [SerializeField]
    private float _minDistanceToRotate = 50;

    [SerializeField]
    private float _minRotationMuliplier = 0.1f;

    public override void Init() {
        GetShip();
    }

    protected override void GetShip() {
        base.GetShip();
        _ship.name = "PlayerShip";
        _ship.OnDestroyed += OnShipDestroyed;
        CameraFollow.Instance.SetTarget(_ship.GetCameraFollowTarget());
    }

    public override void Activate() {
        base.Activate();
        RespawnShip();
    }

    public override void DeActivate() {
        base.DeActivate();
        GameUI.Instance._arView.SetActive(false);
    }

    private void OnShipDestroyed(PlayerData _, PlayerData __) {
        GameManager.Instance.RespawnManager.MinusPoint(_playerData.Team);
        GameUI.Instance._arView.SetActive(false);
        GameUI.Instance.LeaderboardDialog.OpenRespawnState(PlayerRespawn);
    }

    private void PlayerRespawn() {
        RespawnShip();
        GameUI.Instance._arView.SetActive(true);
    }

    private void Update() {
        GameUI.Instance._playerHpView.SetData(_ship.GetHpPercent(), _ship.GetShieldPercent());
        GameUI.Instance._arView.SetData(_ship.GetSpeedPercent(), _ship.GetOverheatPercent());
        if (Input.GetKey(KeyCode.W)) {
            _ship.Accelerate();
        }

        if (Input.GetKey(KeyCode.S)) {
            _ship.Slowdown();
        }

        if (Input.GetMouseButton(0)) {
            FirePrime();
        }

        if (Input.GetMouseButton(1)) {
            FireSecond();
        }

        Vector2 shift = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        if (shift.magnitude < _minDistanceToRotate) {
            shift *= _minRotationMuliplier;
        } else {
            shift -= shift.normalized * _minDistanceToRotate;
        }

        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);

        _ship.RotateBy(rotVector + TrySideRotate());
    }

    private void FirePrime() {
        Vector3 screenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Vector3 point = ray.GetPoint(10000);
        _ship.FirePrime(point);
    }

    private void FireSecond() {
        Vector3 screenPos = _isMouseTarget ? Input.mousePosition : new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Vector3 point = ray.GetPoint(10000);
        _ship.FireSecond(point);
    }

    private Vector3 TrySideRotate() {
        Vector3 sideRot = Vector3.zero;
        if (Input.GetKey(KeyCode.E)) {
            sideRot += Vector3.back;
        }

        if (Input.GetKey(KeyCode.Q)) {
            sideRot += Vector3.forward;
        }

        return sideRot;
    }
}