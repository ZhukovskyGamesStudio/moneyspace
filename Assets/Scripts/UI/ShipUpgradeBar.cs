using System.Collections.Generic;
using UnityEngine;

public class ShipUpgradeBar : MonoBehaviour {
    [SerializeField]
    private List<UpgradePointView> _pointViews;

    public void SetData() {
        foreach (UpgradePointView pointView in _pointViews) {
            pointView.SetState(1);
        }
    }
}