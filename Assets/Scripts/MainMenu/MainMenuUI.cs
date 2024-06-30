using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeDialog _shipUpgradeDialog;

    [SerializeField]
    private PlayerSelectDialog _playerSelectDialog;

    public ShipUpgradeDialog ShipUpgradeDialog => _shipUpgradeDialog;

    [SerializeField]
    private PlayerMainMenuView _playerMainMenuView;

    [SerializeField]
    private ShipsPanel _shipsPanel;

    public ShipsPanel ShipsPanel => _shipsPanel;

    [SerializeField]
    private PlayerHpView _playerHpView;

    public PlayerHpView PlayerHpView => _playerHpView;

    [SerializeField]
    private CoinsView _coinsView;

    public CoinsView CoinsView => _coinsView;

    [SerializeField]
    private CoinsView _watchAdButtonCoinsView;

    [SerializeField]
    private Button _playButton, _buyButton, _upgradesButton;

    private Action _onBuy;

    public static MainMenuUI Instance;

    private void Awake() {
        Instance = this;
    }

    public void ToggleShipUpgradeDialog() {
        _shipUpgradeDialog.Toggle();
    }

    public void TogglePlayerSelectDialog() {
        _playerSelectDialog.Toggle();
    }

    public void CloseDialogs() {
        _shipUpgradeDialog.Close();
        _playerSelectDialog.Close();
    }

    public void Init() {
        _watchAdButtonCoinsView.SetData(MainConfigTable.Instance.MainGameConfig.RewardForWatchAd);
    }

    public void SetData(SaveProfile profile) {
        _coinsView.SetData(profile.CoinsAmount);
        _playerMainMenuView.UpdateData(profile);
    }

    public void SetButtonPlay() {
        _playButton.gameObject.SetActive(true);
        _buyButton.gameObject.SetActive(false);
        _upgradesButton.interactable = true;
    }

    public void SetButtonBuy(Action onBuy) {
        _playButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(true);
        _upgradesButton.interactable = false;
        _onBuy = onBuy;
    }

    public void BuyShip() {
        _onBuy?.Invoke();
    }
}