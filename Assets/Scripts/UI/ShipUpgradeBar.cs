using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeBar : MonoBehaviour {
    [SerializeField]
    private List<UpgradePointView> _pointViews;

    [SerializeField]
    private Button _upgradeButton;

    [SerializeField]
    private TextMeshProUGUI _upgradeCostText, _mainUpgradeButtonText;

    [SerializeField]
    private Image _coinIcon;

    public void SetData(int have, int possible, int cost) {
        for (int index = 0; index < _pointViews.Count; index++) {
            UpgradePointView pointView = _pointViews[index];
            if (index < have) {
                pointView.SetState(0);
            } else if (index < possible) {
                pointView.SetState(1);
            } else {
                pointView.SetState(2);
            }
        }

        SetUpgradeButton(have, possible, cost);
    }

    private void SetUpgradeButton(int have, int possible, int cost) {
        if (have == possible) {
            ChangeUpgradeButtonState(false, "Максимально", "улучшено", false);
        } else {
            string costText = CoinsView.GetDottedView(cost);
            ChangeUpgradeButtonState(true, "Улучшить", costText, true);
        }
    }

    private void ChangeUpgradeButtonState(bool isInteractable, string upperText, string costText, bool isCoinIcon) {
        if (_upgradeButton) {
            _upgradeButton.interactable = isInteractable;
            _mainUpgradeButtonText.text = upperText;
            _upgradeCostText.text = costText;
            _coinIcon.gameObject.SetActive(isCoinIcon);
        }
    }
}