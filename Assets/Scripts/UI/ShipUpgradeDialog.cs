using TMPro;
using UnityEngine;

public class ShipUpgradeDialog : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeBar _speedUpgrade, _shieldUpgrade, _attackUpgrade;

    [SerializeField]
    private TextMeshProUGUI _nameText, _descriptionText;

    private ShipConfig _config;
    private ShipUpgradeData _upgradeData;

    public void Toggle() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        UpdateView();
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public void SetData(ShipConfig config, ShipUpgradeData upgradeData) {
        _config = config;
        _upgradeData = upgradeData;
        _nameText.text = config.ShipName;
        _descriptionText.text = config.Description;
    }

    private void UpdateView() {
        _speedUpgrade.SetData(_upgradeData.Speed, _config.SpeedMax, _config.UpgradeCost);
        _shieldUpgrade.SetData(_upgradeData.Shield, _config.ShieldMax, _config.UpgradeCost);
        _attackUpgrade.SetData(_upgradeData.Attack, _config.AttackMax, _config.UpgradeCost);
    }

    public void UpgradeSpeed() {
        _upgradeData.Speed++;
        SaveLoadManager.Profile.CoinsAmount -= _config.UpgradeCost;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }

    public void UpgradeShield() {
        _upgradeData.Shield++;
        SaveLoadManager.Profile.CoinsAmount -= _config.UpgradeCost;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }

    public void UpgradeAttack() {
        _upgradeData.Attack++;
        SaveLoadManager.Profile.CoinsAmount -= _config.UpgradeCost;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        UpdateView();
    }
}