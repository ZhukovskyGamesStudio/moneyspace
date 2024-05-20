using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipConfig", fileName = "ShipConfig", order = 0)]
public class ShipConfig : ScriptableObject {
    public IShip Prefab;
    public ShipType ShipType;

    public int ShipMaxHp;

    public int ShipSpeedMax;
    public int ShipShieldMax;
    public int ShipAttackMax;

    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();
}