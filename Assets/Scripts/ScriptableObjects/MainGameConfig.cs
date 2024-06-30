using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainGameConfig", fileName = "MainGameConfig", order = 0)]
public class MainGameConfig : ScriptableObject {
    [Header("Deathmatch")]
    public int StartingPointsInEachTeam = 2;
    public int PlayersInGameAmount = 20;
    public float BotRespawnTime = 10;
    public float FightRadius = 5000;

    [Header("Costs & Rewards")]
    public int StartingCoinsAmount = 123456;
    public int RewardForWatchAd = 100000;
    public int MultiplierForWatchAdInGame = 2;
    public int RewardForKill = 10000;
    public int RewardForWin = 100000;
    public List<int> IconCost = new List<int>();
}