using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeBar : MonoBehaviour {
    [SerializeField]
    private List<UpgradePointView> _pointViews;

    [SerializeField]
    private Button _upgradeButton;
    public void SetData(int have, int possible) {
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

        _upgradeButton.interactable = have < possible;
    }
}