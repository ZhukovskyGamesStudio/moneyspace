using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipConfig", fileName = "ShipConfig", order = 0)]
public class ShipConfig : ScriptableObject {
    public IShip Prefab;
    public GameObject ModelPrefab;
    public Sprite Icon;
    public ShipType ShipType;

    [Header("Shop Parameters")]
    public string ShipName;

    [TextArea]
    public string Description;

    public int ShipCost = 1000000;

    public float UpperShiftOnShowcase = 0;

    [Header("Fight Parameters")]
    public int MaxHp;

    public int ShiedRepairSpeed = 10;
    public float OverheatFromShoot = 0.1f, OverheatFromSecond = 0.025f;

    public float DecreaseOverheatSpeed = 0.1f;

    public float AccelerationSpeed = 4f, DecelerationSpeed = 4f;

    [Header("Movement Parameters")]
    public float PlayerSideRotationSpeed = 35;

    public float VerticalMaxRotationSpeed = 10, HorizontalMaxRotationSpeed = 10;
    public float HorRotationMultiplier = 1, VertRotationMultiplier = 1;

    [Header("Model Movement")]
    public float ModelRotation = 30f;

    public float ModelMovement = 30f;

    [Header("Upgrades")]
    public ShipUpgradeData DefaultShipUpgrades = new ShipUpgradeData();

    [Range(1, 12)]
    public int SpeedMax;

    [Range(1, 12)]
    public int ShieldMax;

    [Range(1, 12)]
    public int AttackMax;

    public List<int> AttackUpgradesCost = new List<int>();
    public int GetAttackCost(int cur) => cur - 1 < AttackUpgradesCost.Count ? AttackUpgradesCost[cur - 1] : 0;
    public List<int> ShieldUpgradesCost = new List<int>();
    public int GetShieldCost(int cur) => cur - 1 < ShieldUpgradesCost.Count ? ShieldUpgradesCost[cur - 1] : 0;
    public List<int> SpeedUpgradesCost = new List<int>();
    public int GetSpeedCost(int cur) => cur - 1 < SpeedUpgradesCost.Count ? SpeedUpgradesCost[cur - 1] : 0;
}