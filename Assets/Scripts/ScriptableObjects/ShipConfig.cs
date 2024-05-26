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

    [Header("FightParameters")]
    public int MaxHp;
    [Range(1,12)]
    public int SpeedMax;
    public int ShieldMax;
    public int AttackMax;

    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();
}