using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipConfig", fileName = "ShipConfig", order = 0)]
public class ShipConfig : ScriptableObject {
    public IShip Prefab;
    public Sprite Icon;
    public ShipType ShipType;

    public string ShipName;
    [TextArea]
    public string Description;

    public int MaxHp;
    
    public int SpeedMax;
    public int ShieldMax;
    public int AttackMax;

    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();
}