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
    public float UpperShiftOnShowcase;

    [Header("Fight Parameters")]
    public int MaxHp;
    public int ShiedRepairSpeed = 10;
    public float OverheatFromShoot = 0.1f, ShootRecoil = 0.3f;
    public float OverheatFromSecond = 0.025f, SecondRecoil = 0.03f;
    public float DecreaseOverheatSpeed = 0.1f;
    public float AccelerationSpeed = 4f, DecelerationSpeed = 4f;

    [Header("Movement Parameters")]
    public float PlayerSideRotationSpeed = 35;
    public float HorRotationMultiplier = 1, VertRotationMultiplier = 1;
    public float BoostSpeedMultiplier = 1.5f;
    public float BoostDecreasePerSecond = 0.33f;
    public float BoostIncreasePerSecond = 0.1f;

    [Header("Model Movement")]
    public float ModelRotation = 30f;
    public float ModelMovement = 30f;
    public float ModelLerp = 0.15f;

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
    
    
    public ShipUpgradeData GetRandomizedUpgrades() {
        ShipUpgradeData shipUpgradeData = DefaultShipUpgrades.Copy;
        shipUpgradeData.Speed = GetRandomizedStat(shipUpgradeData.Speed, SpeedMax);
        shipUpgradeData.Shield = GetRandomizedStat(shipUpgradeData.Shield, ShieldMax); 
        shipUpgradeData.Attack = GetRandomizedStat(shipUpgradeData.Attack, AttackMax); 
       
        return shipUpgradeData;
    }

    private int GetRandomizedStat(int basic, int max) {
        float chance = ShipsFactory.ShipStatsGeneralConfig.BotChanceToUpgradeStat;
        int upgraded = basic;
        //Если это первый бой игрока - то все корабли ботов непрокачанные
        if (SaveLoadManager.Profile.GamesPlayedAmount == 0) {
            return basic;
        }
        while (upgraded < max-1 && Random.Range(0, 1f) < chance) {
            upgraded++;
        }
        return upgraded;
    }
}