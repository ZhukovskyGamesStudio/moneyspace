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
        _curShipIndex = SaveLoadManager.Profile.SelectedShip;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.SetPos(_curShipIndex);
    }

    public void ChangeShip(bool isRight) {
        _curShipIndex += isRight ? 1 : -1;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.Move(isRight);
        var curShipConfig = ShipsFactory.Ships[_curShipIndex];
        if (SaveLoadManager.Profile.ShipUpgradeDatas.Any(sud => sud.Type == curShipConfig.ShipType)) {
            MainMenuUI.Instance.SetButtonPlay();
        } else {
            MainMenuUI.Instance.SetButtonBuy(OnBuySelectedShip);
        }

        SaveLoadManager.Profile.SelectedShip = _curShipIndex;
    }

    private void UpdateButtonsState(int curIndex) {
        _leftButton.interactable = curIndex != 0;
        _rightButton.interactable = curIndex != ShipsFactory.Ships.Count - 1;
    }

    private void OnBuySelectedShip() {
        var curShipConfig = ShipsFactory.Ships[_curShipIndex];
        SaveLoadManager.Profile.ShipUpgradeDatas.Add(curShipConfig.DefaultShipUpgrades);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetButtonPlay();
    }
}