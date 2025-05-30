using System.Collections;
using UnityEngine;

public class PrepareGameManager : MonoBehaviour {
    [SerializeField]
    private PilotsManager _pilotsManager;

    [SerializeField]
    private MinimapCameraFollow _minimapCameraFollow;

    public void Start() {
        StartCoroutine(StartingCoroutine());
        //LoadingPanel.Instance.OnSceneLoaded += OnInstanceOnLoadingPanelHided;
    }

    private void OnInstanceOnLoadingPanelHided(string a) {
       //LoadingPanel.Instance.OnSceneLoaded -= OnInstanceOnLoadingPanelHided;
       // StartCoroutine(StartingCoroutine());
    }

    private IEnumerator StartingCoroutine() {
        yield return StartCoroutine(_minimapCameraFollow.PrepareMinimap());

        GameManager.Instance.PilotsManager = _pilotsManager;
        if (!MoneyspaceSaveLoadManager.Profile.IsFtueDialogSeen) {
            ShowFtue();
        } else {
            InitGame();
        }
    }

    private void ShowFtue() {
        GameUI.Instance.FtueDialog.Show(EndFtue);
    }

    private void EndFtue() {
        MoneyspaceSaveLoadManager.Profile.IsFtueDialogSeen = true;
        MoneyspaceSaveLoadManager.Save();
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