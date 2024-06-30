using TMPro;
using UnityEngine;

public class ShipUpgradeDialog : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeBar _speedUpgrade, _shieldUpgrade, _attackUpgrade;

    [SerializeField]
    private TextMeshProUGUI _nameText, _descriptionText;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;
    
    private ShipConfig _config;
    private ShipUpgradeData _upgradeData;

    public void Toggle() {
        if (_animationHandler.IsOn) {
            _animationHandler.ChangeWithAnimation(false);
            MainMenuUI.Instance.ShipsPanel.ChangeSmallStatsViewActive(true);
            MainMenuUI.Instance.ShipsPanel.MenuShipsView.ToggleUpgradePos(false);
            return;
        }

        MainMenuUI.Instance.ShipsPanel.ChangeSmallStatsViewActive(false);
        MainMenuUI.Instance.ShipsPanel.MenuShipsView.ToggleUpgradePos(true);
        _animationHandler.ChangeWithAnimation(true);
        UpdateView();
    }

    public void Close() {
          _animationHandler.ChangeWithAnimation(false);
    }

    public void SetData(ShipConfig config, ShipUpgradeData upgradeData) {
        _config = config;
        _upgradeData = upgradeData;
        _nameText.text = config.ShipName;
        _descriptionText.text = config.Description;
    }

    private void UpdateView() {
        _speedUpgrade.SetData(_upgradeData.Speed, _config.SpeedMax, _config.GetSpeedCost(_upgradeData.Speed));
        _shieldUpgrade.SetData(_upgradeData.Shield, _config.ShieldMax, _config.GetShieldCost(_upgradeData.Shield));
        _attackUpgrade.SetData(_upgradeData.Attack, _config.AttackMax, _config.GetAttackCost(_upgradeData.Attack));
    }

    public void UpgradeSpeed() {
        _upgradeData.Speed++;
        SaveLoadManager.Profile.CoinsAmount -= _config.GetSpeedCost(_upgradeData.Speed);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }

    public void UpgradeShield() {
        _upgradeData.Shield++;
        SaveLoadManager.Profile.CoinsAmount -= _config.GetShieldCost(_upgradeData.Shield);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }

    public void UpgradeAttack() {
        _upgradeData.Attack++;
        SaveLoadManager.Profile.CoinsAmount -= _config.GetAttackCost(_upgradeData.Attack);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }
}