using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainGameConfig", fileName = "MainGameConfig", order = 0)]
public class MainGameConfig : ScriptableObject {
    public int StartingPointsInEachTeam = 2;
    public int PlayersInGameAmount = 20;

    public int RewardForWatchAd = 100000;
    public int MultiplierForWatchAdInGame = 2;
    public int RewardForKill = 10000;
    public int RewardForWin = 100000;
}