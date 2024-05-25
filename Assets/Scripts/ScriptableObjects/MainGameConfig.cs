using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainGameConfig", fileName = "MainGameConfig", order = 0)]
public class MainGameConfig : ScriptableObject {
    [Header("Deathmatch")]
    public int StartingPointsInEachTeam = 2;
    public int PlayersInGameAmount = 20;
    public float BotRespawnTime = 10;

    [Header("Costs & Rewards")]
    public int RewardForWatchAd = 100000;
    public int MultiplierForWatchAdInGame = 2;
    public int RewardForKill = 10000;
    public int RewardForWin = 100000;
}