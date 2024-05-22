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
        _speedUpgrade.SetData(_upgradeData.Speed, _config.SpeedMax);
        _shieldUpgrade.SetData(_upgradeData.Shield, _config.ShieldMax);
        _attackUpgrade.SetData(_upgradeData.Attack, _config.AttackMax);
    }

    public void UpgradeSpeed() {
        _upgradeData.Speed++;
        SaveLoadManager.Save();

        UpdateView();
    }

    public void UpgradeShield() {
        _upgradeData.Shield++;
        SaveLoadManager.Save();

        UpdateView();
    }

    public void UpgradeAttack() {
        _upgradeData.Attack++;
        SaveLoadManager.Save();

        UpdateView();
    }
}