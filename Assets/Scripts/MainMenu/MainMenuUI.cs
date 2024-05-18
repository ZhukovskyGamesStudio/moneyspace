using UnityEngine;

public class MainMenuUI : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeDialog _shipUpgradeDialog;

    public void ToggleShipUpgradeDialog() {
        _shipUpgradeDialog.Open();
    }
}