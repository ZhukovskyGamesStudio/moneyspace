using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

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
            Close();
        } else {
            Open();
        }
    }

    private void Open() {
        MainMenuUI.Instance.ShipsPanel.ChangeSmallStatsViewActive(false);
        MainMenuUI.Instance.ShipsPanel.MenuShipsView.ToggleUpgradePos(true);
        _animationHandler.ChangeWithAnimation(true);
        UpdateView();
    }

    public void Close() {
        _animationHandler.ChangeWithAnimation(false);
        MainMenuUI.Instance.ShipsPanel.ChangeSmallStatsViewActive(true);
        MainMenuUI.Instance.ShipsPanel.MenuShipsView.ToggleUpgradePos(false);
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
        int cost = _config.GetSpeedCost(_upgradeData.Speed);
        if (SaveLoadManager.Profile.CoinsAmount < cost) {
            MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
            return;
        }

        YandexMetrica.Send("buyUpgrade", new Dictionary<string, string>() {
            { "buyUpgrade", _config.ShipName + "_speed" }
        });

        SaveLoadManager.Profile.CoinsAmount -= _config.GetSpeedCost(_upgradeData.Speed);
        _upgradeData.Speed++;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
        UpdateView();
    }

    public void UpgradeShield() {
        int cost = _config.GetShieldCost(_upgradeData.Shield);
        if (SaveLoadManager.Profile.CoinsAmount < cost) {
            MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
            return;
        }

        YandexMetrica.Send("buyUpgrade", new Dictionary<string, string>() {
            { "buyUpgrade", _config.ShipName + "_shield" }
         
        });

        SaveLoadManager.Profile.CoinsAmount -= _config.GetShieldCost(_upgradeData.Shield);
        _upgradeData.Shield++;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
        UpdateView();
    }

    public void UpgradeAttack() {
        int cost = _config.GetAttackCost(_upgradeData.Attack);
        if (SaveLoadManager.Profile.CoinsAmount < cost) {
            MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
            return;
        }

        YandexMetrica.Send("buyUpgrade", new Dictionary<string, string>() {
            { "buyUpgrade", _config.ShipName + "_attack" }
        });

        SaveLoadManager.Profile.CoinsAmount -= _config.GetAttackCost(_upgradeData.Attack);
        _upgradeData.Attack++;
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
        UpdateView();
    }
}