using System.Collections.Generic;
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

    public List<int> AttackUpgradesCost = new List<int>();
    public List<int> ShieldUpgradesCost = new List<int>();
    public List<int> SpeedUpgradesCost = new List<int>();
    public int UpgradeCost = 100000;

    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();
}