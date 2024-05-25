using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipStatsGeneralConfig", fileName = "ShipStatsGeneralConfig", order = 0)]
public class ShipStatsGeneralConfig : ScriptableObject {
    public int ShieldPerPoint;

    public int LaserDamage;

   
    
    
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
}