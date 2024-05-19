using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeDialog _shipUpgradeDialog;

    [SerializeField]
    private CoinsView _coinsView;

    [SerializeField]
    private CoinsView _watchAdButtonCoinsView;

    public void ToggleShipUpgradeDialog() {
        _shipUpgradeDialog.Open();
    }

    public void Init() {
        _watchAdButtonCoinsView.SetData(MainConfigTable.Instance.MainGameConfig.RewardForWatchAd);
    }

    public void SetData(SaveProfile profile) {
        _coinsView.SetData(profile.CoinsAmount);
    }
}