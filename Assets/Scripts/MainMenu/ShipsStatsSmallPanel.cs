using UnityEngine;

public class ShipsStatsSmallPanel : MonoBehaviour
{
    [SerializeField]
    private ShipUpgradeBar _speedBar, _shieldBar, _attackBar;

    public void UpdateView(ShipConfig config, ShipUpgradeData upgradeData) {
        UpdateBarsView(config,upgradeData);
        
    }
    
    private void UpdateBarsView(ShipConfig config, ShipUpgradeData upgradeData) {
        _speedBar.SetData(upgradeData.Speed, config.SpeedMax, config.GetSpeedCost(upgradeData.Speed));
        _shieldBar.SetData(upgradeData.Shield, config.ShieldMax, config.GetShieldCost(upgradeData.Shield));
        _attackBar.SetData(upgradeData.Attack, config.AttackMax, config.GetAttackCost(upgradeData.Attack));
    }
}
