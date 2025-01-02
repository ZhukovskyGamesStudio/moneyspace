using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPilot : AbstractPilot {
    [SerializeField]
    private bool _isMouseTarget = false;

    [SerializeField]
    private float _minDistanceToRotate = 50;

    [SerializeField]
    private float _minRotationMuliplier = 0.1f;

    private Ship _curTarget;
    private Camera _mainCam;
    
    

    public override void Init() {
        _mainCam = Camera.main;
        GetShip();
        RespawnShip();
    }

    protected override ShipType GetShipType() {
        return ShipsFactory.Ships[MoneyspaceSaveLoadManager.Profile.SelectedShip].ShipType;
    }

    protected override void GetShip() {
        base.GetShip();
        var shipConfig = ShipsFactory.Ships.First(s => s.ShipType == _ship.ShipType);
        var upgradeData = MoneyspaceSaveLoadManager.Profile.ShipUpgradeDatas.First(s => s.Type == _ship.ShipType);
        _ship.InitFromConfig(shipConfig, upgradeData);
        _ship.name = "PlayerShip";
        _ship.OnDestroyed += OnShipDestroyed;
        CameraFollow.Instance.SetTarget(_ship.GetCameraFollowTarget());
        MinimapCameraFollow.Instance.SetTarget(_shipTransform);
    }

    public override void Activate() {
        base.Activate();
    }

    public override void DeActivate() {
        base.DeActivate();
        GameUI.Instance._arView.SetActive(false);
        GameUI.Instance.UiMessages.gameObject.SetActive(false);
    }

    private void OnShipDestroyed(AbstractPilot _, AbstractPilot __) {
        GameManager.Instance.RespawnManager.MinusPoint(_playerData.Team);
        GameUI.Instance._arView.SetActive(false);
        StartCoroutine(OpenLbAfterDelay(2));
        ClearTarget();
        ShipDetectZone.Instance.ClearList();
        GameUI.Instance.warpOnSpeed.SetActive(false);
        UpdateTargetMessageView();
    }

    private IEnumerator OpenLbAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GameUI.Instance.UiMessages.gameObject.SetActive(false);
        GameUI.Instance.UiMessages.TimedMessage.ForceHide();
        GameUI.Instance.LeaderboardDialog.OpenRespawnState(PlayerRespawn);
    }

    private void PlayerRespawn() {
        RespawnShip();
        GameUI.Instance._arView.SetActive(true);
        GameUI.Instance.UiMessages.gameObject.SetActive(true);
    }

    protected override void RespawnShip() {
        base.RespawnShip();
        ShipDetectZone.Instance.SetPlayerShip(_ship);
        _ship.StartRespawnAnimation();
    }

    private void Update() {
        if (!_ship.gameObject.activeSelf) {
            return;
        }

        if (Input.GetKey(KeyCode.W)) {
            _ship.Accelerate();
            _ship.SetBoost(Input.GetKey(KeyCode.LeftShift) );
        } else {
            _ship.Slowdown();
        }

        if (Input.GetKey(KeyCode.S)) {
            _ship.SlowdownKeyDown();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M)) {
            LoadingPanel.ShowAndLoadScene("MenuScene");
        }
#endif

        if (Input.GetMouseButton(0)) {
            FirePrime();
        }

        if (Input.GetMouseButton(1)) {
            FireSecond();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            Ship target = TryAcquireTarget();
            if (target != null && target != _curTarget) {
                ClearTarget();
                LockOnTarget(target);
            } else if (_curTarget != null & target == null) {
                ClearTarget();
            }
        }
    }

    private void LateUpdate() {
        UpdateArSliders();
        UpdateTargetMessageView();
        UpdateSpeedAndRotation();
        UpdateOverheatMessage();
        UpdateBoostArView();
    }

    private void UpdateArSliders() {
        GameUI.Instance._playerHpView.SetData(_ship.GetHpPercent(), _ship.GetShieldPercent());
        GameUI.Instance._arView.SetData(_ship.GetSpeedPercent(), _ship.GetOverheatPercent());
    }

    private void UpdateBoostArView() {
        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);
        GameUI.Instance._arView.SetBoostState(isHoldingShift && _ship.GetBoostPercent > 0.05f, _ship.GetBoostPercent);
    }

    private void UpdateOverheatMessage() {
        GameUI.Instance.UiMessages.OverheatMessage.SetActive(_ship.IsOverheated);
        GameUI.Instance._arView.SetOverheatColor(_ship.IsOverheated);
    }

    private void UpdateSpeedAndRotation() {
        Vector2 shift = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        if (shift.magnitude < _minDistanceToRotate) {
            shift *= _minRotationMuliplier;
        } else {
            shift -= shift.normalized * _minDistanceToRotate;
        }

        Vector3 rotVector = new Vector3(-shift.y, shift.x, 0);
        _ship.RotateByV = rotVector + TrySideRotate();

        TurnSpeedParticles();
    }

    private void LockOnTarget(Ship target) {
        GameUI.Instance._arView.ArShootAssist.Activate(target, _ship as Ship);
        _curTarget = target;
        _curTarget.OnDestroyed += OnCurTargetDestroyed;
    }

    private void OnCurTargetDestroyed(AbstractPilot arg1, AbstractPilot arg2) {
        ShipDetectZone.Instance.RemoveFromListInstant(_curTarget);
        ClearTarget();
    }

    private void ClearTarget() {
        if (_curTarget == null) {
            return;
        }

        _curTarget.OnDestroyed -= OnCurTargetDestroyed;
        GameUI.Instance._arView.ArShootAssist.Deactivate();
        _curTarget = null;
    }

    private void UpdateTargetMessageView() {
        bool canHaveTarget = _ship.gameObject.activeSelf;
        if (!canHaveTarget) {
            GameUI.Instance.UiMessages.ChangeTargetMessage.SetActive(false);
            GameUI.Instance.UiMessages.FindTargetMessage.SetActive(false);
            return;
        }

        bool hasShipsToTarget = ShipDetectZone.Instance.HasShipsToTarget();
        if (hasShipsToTarget) {
            Ship nextTarget = ShipDetectZone.Instance.GetClosestShip();
            if (nextTarget != _curTarget) {
                GameUI.Instance.UiMessages.ChangeTargetMessage.SetActive(true);
                GameUI.Instance.UiMessages.FindTargetMessage.SetActive(false);
            }
        } else {
            GameUI.Instance.UiMessages.ChangeTargetMessage.SetActive(false);
            GameUI.Instance.UiMessages.FindTargetMessage.SetActive(_curTarget == null);
        }
    }

    private Ship TryAcquireTarget() {
        bool canHaveTarget = ShipDetectZone.Instance.HasShipsToTarget();
        if (!canHaveTarget) {
            return GameManager.Instance.PilotsManager.GetClosesOppositeTeamShip(_shipTransform.position, true, _curTarget);
        }

        return ShipDetectZone.Instance.GetClosestShip();
    }

    private void FirePrime() {
        Vector3 screenPos = Input.mousePosition;
        Ray ray = _mainCam.ScreenPointToRay(screenPos);

        Vector3 point = ray.GetPoint(10000);
        _ship.FirePrime(point);
    }

    private void FireSecond() {
        Vector3 screenPos = _isMouseTarget ? Input.mousePosition : new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = _mainCam.ScreenPointToRay(screenPos);
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

        return sideRot * _ship.SideRotationSpeed;
    }

    private void TurnSpeedParticles() {
        GameUI.Instance.warpOnSpeed.SetActive(_ship.GetSpeedPercent() >= 0.8);
    }
}