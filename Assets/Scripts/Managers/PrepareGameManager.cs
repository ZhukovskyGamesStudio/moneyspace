using UnityEngine;

public class PrepareGameManager : MonoBehaviour {
    [SerializeField]
    private PilotsManager _pilotsManager;

    [SerializeField]
    private float _fightRadius = 500;

    public void Start() {
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        GameManager.FightRadius = _fightRadius;
        GameManager.Instance.PilotsManager = _pilotsManager;
        GameManager.Instance.PlayersManager.LoadPlayer();
        GameManager.Instance.PlayersManager.GenerateBots(cnfg.PlayersInGameAmount);
        _pilotsManager.GeneratePilots(GameManager.Instance.PlayersManager.BlueTeam, GameManager.Instance.PlayersManager.RedTeam);
        GameUI.Instance.LeaderboardDialog.Init(GameManager.Instance.PlayersManager);

        _pilotsManager.ActivatePilots();
        GameManager.Instance.RespawnManager.SetStartingScore(cnfg.StartingPointsInEachTeam);
    }
}