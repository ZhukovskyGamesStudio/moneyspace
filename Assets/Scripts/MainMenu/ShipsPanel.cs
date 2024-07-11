using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipsPanel : MonoBehaviour {
    [SerializeField]
    private MenuShipsView _menuShipsView;
    public MenuShipsView MenuShipsView => _menuShipsView;

    [SerializeField]
    private Button _leftButton, _rightButton;

    private int _curShipIndex;

    [SerializeField]
    private GameObject _coinImage,_unboughtNameBack;

    [SerializeField]
    private TextMeshProUGUI _costText, _unboughtNameText;

    [SerializeField]
    private ShipsStatsSmallPanel _shipsStatsSmallPanel;

    public ShipsStatsSmallPanel ShipsStatsSmallPanel => _shipsStatsSmallPanel;

    private void Start() {
        Init();
    }

    private void Init() {
        _menuShipsView.Init();
        _curShipIndex = SaveLoadManager.Profile.SelectedShip;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.SetPos(_curShipIndex);
        UpdateView(_curShipIndex);
        ChangeSmallStatsViewActive(true);
    }

    public void ChangeShip(bool isRight) {
        _curShipIndex += isRight ? 1 : -1;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.Move(isRight);
        UpdateView(_curShipIndex);

        MainMenuUI.Instance.ShipUpgradeDialog.Close();

        SaveLoadManager.Profile.SelectedShip = _curShipIndex;
        SaveLoadManager.Save();
        ChangeSmallStatsViewActive(true);
    }

    public void UpdateView(int shipIndex) {
        ShipConfig curShipConfig = ShipsFactory.Ships[shipIndex];
        var upgradeData = SaveLoadManager.Profile.ShipUpgradeDatas.FirstOrDefault(sud => sud.Type == curShipConfig.ShipType);
        if (upgradeData != null) {
            MainMenuUI.Instance.SetButtonPlay();
            MainMenuUI.Instance.ShipUpgradeDialog.SetData(curShipConfig, upgradeData);
            UpdateCostView(curShipConfig, true);
        } else {
            UpdateCostView(curShipConfig, false);
            MainMenuUI.Instance.SetButtonBuy(OnBuySelectedShip);
        }
        
        if (upgradeData == null) {
            upgradeData = curShipConfig.DefaultShipUpgrades; 
        }
        MainMenuUI.Instance.PlayerHpView.SetMaxTexts(curShipConfig.MaxHp, upgradeData.Shield * ShipsFactory.ShipStatsGeneralConfig.ShieldPerPoint);
    }

    public void ChangeSmallStatsViewActive(bool isActive) {
        if (isActive) {
            ShipConfig curShipConfig = ShipsFactory.Ships[_curShipIndex];
            var upgradeData = SaveLoadManager.Profile.ShipUpgradeDatas.FirstOrDefault(sud => sud.Type == curShipConfig.ShipType);
            if (upgradeData == null) {
                upgradeData = curShipConfig.DefaultShipUpgrades;
            }
            
            ShipsStatsSmallPanel.UpdateView(curShipConfig,upgradeData);
            ShipsStatsSmallPanel.ShowViaAnim(true);
        } else {
            ShipsStatsSmallPanel.ShowViaAnim(false);
        }
    }

    private void UpdateCostView(ShipConfig config, bool isBought) {
        if (isBought) {
            _coinImage.SetActive(false);
            _unboughtNameBack.gameObject.SetActive(false);
            _costText.text = config.ShipName;
        } else {
            _coinImage.SetActive(true);
            _unboughtNameBack.gameObject.SetActive(true);
            _unboughtNameText.text = config.ShipName;
            _costText.text = CoinsView.GetDottedView(config.ShipCost);
        }
    }
    

    private void UpdateButtonsState(int curIndex) {
        _leftButton.interactable = curIndex != 0;
        _rightButton.interactable = curIndex != ShipsFactory.Ships.Count - 1;
    }

    private void OnBuySelectedShip() {
        ShipConfig curShipConfig = ShipsFactory.Ships[_curShipIndex];
        int cost = curShipConfig.ShipCost;
        if (SaveLoadManager.Profile.CoinsAmount < cost) {
            MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
            return;
        } else {
            SaveLoadManager.Profile.CoinsAmount -= cost;
            MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
        }
        
        
        ShipUpgradeData newData = curShipConfig.DefaultShipUpgrades.Copy;
        SaveLoadManager.Profile.ShipUpgradeDatas.Add(newData);
        SaveLoadManager.Save();
        MainMenuUI.Instance.SetButtonPlay();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
        MainMenuUI.Instance.ShipUpgradeDialog.SetData(curShipConfig, newData);
        UpdateCostView(curShipConfig, true);
    }
}