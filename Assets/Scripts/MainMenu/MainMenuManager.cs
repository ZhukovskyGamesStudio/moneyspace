using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private MainMenuUI _mainMenuUI;

    private void Start() {
        _mainMenuUI.Init();
        _mainMenuUI.SetData(SaveLoadManager.Profile);
    }

    public void PlayButton() {
        LoadingPanel.ShowAndLoadScene("GameScene");
    }

    public void UpgradesButton() {
        _mainMenuUI.ShipUpgradeDialog.Toggle();
        _mainMenuUI.SettingsDialog.Close();
        _mainMenuUI.PlayerSelectDialog.Close();
    }

    public void SettingsButton() {
        _mainMenuUI.SettingsDialog.Toggle();
        _mainMenuUI.ShipUpgradeDialog.Close();
        _mainMenuUI.PlayerSelectDialog.Close();
    }

    public void WatchAdButton() {
#if UNITY_EDITOR
        GiveCoinsAfterRewAd();
#else
        YgHandler handler = new YgHandler();
        handler.ShowRewarded(GiveCoinsAfterRewAd);
#endif

        _mainMenuUI.SetData(SaveLoadManager.Profile);
        _mainMenuUI.CloseDialogs();

        Debug.Log("WatchAdButton");
    }

    private void GiveCoinsAfterRewAd() {
        SaveLoadManager.Profile.CoinsAmount += MainConfigTable.Instance.MainGameConfig.RewardForWatchAd;
        SaveLoadManager.Save();
    }

    public void OpenPlayerData() {
        _mainMenuUI.PlayerSelectDialog.Toggle();
        _mainMenuUI.ShipUpgradeDialog.Close();
        _mainMenuUI.SettingsDialog.Close();
    }
}