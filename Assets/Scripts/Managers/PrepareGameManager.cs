using UnityEngine;

public class PrepareGameManager : MonoBehaviour {
    [SerializeField]
    private PilotsManager _pilotsManager;

    public void Start() {
        GameManager.Instance.PilotsManager = _pilotsManager;
        if (!SaveLoadManager.Profile.IsFtueDialogSeen) {
            ShowFtue();
        } else {
            InitGame();
        }
    }

    private void ShowFtue() {
        GameUI.Instance.FtueDialog.Show(EndFtue);
    }

    private void EndFtue() {
        SaveLoadManager.Profile.IsFtueDialogSeen = true;
        SaveLoadManager.Save();
        InitGame();
    }

    private void InitGame() {
        GameManager.Instance.PlayersManager.LoadPlayer();
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        GameManager.Instance.PlayersManager.GenerateBots(cnfg.PlayersInTeamAmount);
        _pilotsManager.GeneratePilots(GameManager.Instance.PlayersManager.BlueTeam, GameManager.Instance.PlayersManager.RedTeam);
        GameUI.Instance.LeaderboardDialog.Init(GameManager.Instance.PlayersManager);

        _pilotsManager.ActivatePilots();
        GameManager.Instance.RespawnManager.SetStartingScore(cnfg.StartingPointsInEachTeam);
    }
}