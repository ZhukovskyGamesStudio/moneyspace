using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipStatsGeneralConfig", fileName = "ShipStatsGeneralConfig", order = 0)]
public class ShipStatsGeneralConfig : ScriptableObject {
    public int ShieldPerPoint;
    public int BaseSpeedMax = 15;
    public int SpeedMaxPerPoint;
    public int LaserBaseDamage = 10;
    public int LaserDamagePerPoint;
    
    public float LaserSpeed = 100;
    public float PrimeLaserLifetime = 3;
    public float SecondLaserLifetime = 1;



    public int DamageFromCollision = 100;
    public TechicalShipParameters TechicalParams = new TechicalShipParameters();
    
    [Header("BotParameters")]
    [Range(0,180)]
    public float BotShootAngle;

    public float BotMinStartShootDistance;
    public float BotMaxStartShootDistance;

    public float BotMinAttackTime;
    public float BotMaxAttackTime;


    public float StartEvadeDistance;
    
    [Range(0,1)]
    public float BotHuntDesirableSpeed;
    [Range(0,1)]
    public float BotShootDesirableSpeed;
    [Range(0,1)]
    public float BotEvadeDesirableSpeed;

    public float BotChanceToUpgradeStat = 0.85f;
    public float ChanceToGetPlayerAsTarget = 0.05f;
    public float BotRandomShootDelta = 10;
    public float BotRandomShootChance = 0.5f;
    public float BotRandomShootDeltaAgainsPlayer = 10;
    public float BotRandomShootChanceAgainsPlayer = 0.5f;   
    
    public float BotChanceToShootWithSecondGun = 0.3f;
    public float BotRandomEvadePointDelta = 150;
    public float BotRandomChasePointDelta = 10;
}
[Serializable]
public class TechicalShipParameters {
    public float BotRotationSlerp;
    public float DelayDropLockAfterShipMovedAwayFromCenter = 0.7f;
}