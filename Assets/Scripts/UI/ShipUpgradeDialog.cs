using UnityEngine;

public class ShipUpgradeDialog : MonoBehaviour {
    [SerializeField]
    private ShipUpgradeBar _speedUpgrade, _shieldUpgrade, _attackUpgrade;

    public void Open() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        _speedUpgrade.SetData();
        _shieldUpgrade.SetData();
        _attackUpgrade.SetData();
    }
}