using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipConfig", fileName = "ShipConfig", order = 0)]
public class ShipConfig : ScriptableObject {
    public IShip Prefab;
    public Sprite Icon;
    public ShipType ShipType;

    [Header("ShopParameters")]
    public string ShipName;
    [TextArea]
    public string Description;

    public float UpperShiftOnShowcase = 0;

    [Header("FightParameters")]
    public int MaxHp;
    [Range(1,12)]
    public int SpeedMax;
    public int ShieldMax;
    public int AttackMax;

    public int ShipCost = 1000000;
    public int UpgradeCost = 100000;

    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();
}