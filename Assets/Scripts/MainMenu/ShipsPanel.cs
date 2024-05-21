using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipsPanel : MonoBehaviour {
    [SerializeField]
    private MenuShipsView _menuShipsView;

    [SerializeField]
    private Button _leftButton, _rightButton;

    private int _curShipIndex;

    private void Start() {
        Init();
    }

    public void Init() {
        _menuShipsView.Init();
        _curShipIndex = SaveLoadManager.Profile.SelectedShip;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.SetPos(_curShipIndex);
        UpdateView(_curShipIndex);
    }

    public void ChangeShip(bool isRight) {
        _curShipIndex += isRight ? 1 : -1;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.Move(isRight);
        UpdateView(_curShipIndex);

        MainMenuUI.Instance.ShipUpgradeDialog.Close();
        SaveLoadManager.Profile.SelectedShip = _curShipIndex;
        SaveLoadManager.Save();
    }

    public void UpdateView(int shipIndex) {
        var curShipConfig = ShipsFactory.Ships[shipIndex];
        var upgradeData = SaveLoadManager.Profile.ShipUpgradeDatas.FirstOrDefault(sud => sud.Type == curShipConfig.ShipType);
        if (upgradeData != null) {
            MainMenuUI.Instance.SetButtonPlay();
            MainMenuUI.Instance.ShipUpgradeDialog.SetData(curShipConfig, upgradeData);
        } else {
            MainMenuUI.Instance.SetButtonBuy(OnBuySelectedShip);
        }
    }

    private void UpdateButtonsState(int curIndex) {
        _leftButton.interactable = curIndex != 0;
        _rightButton.interactable = curIndex != ShipsFactory.Ships.Count - 1;
    }

    private void OnBuySelectedShip() {
        ShipConfig curShipConfig = ShipsFactory.Ships[_curShipIndex];
        ShipUpgradeData newData = curShipConfig.DefaultShipUpgrades.Copy;
        SaveLoadManager.Profile.ShipUpgradeDatas.Add(newData);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetButtonPlay();
        MainMenuUI.Instance.ShipUpgradeDialog.SetData(curShipConfig, newData);
    }
}