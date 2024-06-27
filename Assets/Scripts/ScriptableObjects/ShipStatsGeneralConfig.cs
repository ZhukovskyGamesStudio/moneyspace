using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipStatsGeneralConfig", fileName = "ShipStatsGeneralConfig", order = 0)]
public class ShipStatsGeneralConfig : ScriptableObject {
    public int ShieldPerPoint;
    

    public int LaserDamage;
    
    public float LaserSpeed = 100;
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

    public float BotRandomShootDelta = 10;
    public float BotRandomShootChance = 0.5f;
    public float BotRandomEvadePointDelta = 150;
    public float BotRandomChasePointDelta = 10;
}
[Serializable]
public class TechicalShipParameters {
    public float PlayerSideRotationSpeed = 35;
    public float _verticalMaxRotationSpeed = 10, _horizontalMaxRotationSpeed = 10;
    public float _horRotation = 1, _vertRotation = 1;
    public float BotRotationSlerp;

}