using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private MainMenuUI _mainMenuUI;

    private void Start() {
        _mainMenuUI.Init();
        _mainMenuUI.SetData(SaveLoadManager.Profile);
    }

    public void PlayButton() {
        SceneManager.LoadScene("GameScene");
    }

    public void UpgradesButton() {
        _mainMenuUI.ToggleShipUpgradeDialog();
    }

    public void SettingsButton() {
        Debug.Log("SettingsButton");
    }

    public void WatchAdButton() {
        SaveLoadManager.Profile.CoinsAmount += MainConfigTable.Instance.MainGameConfig.RewardForWatchAd;
        SaveLoadManager.Save();
        _mainMenuUI.SetData(SaveLoadManager.Profile);
        _mainMenuUI.CloseDialogs();
        Debug.Log("WatchAdButton");
    }

    public void OpenPlayerData() {
        _mainMenuUI.TogglePlayerSelectDialog();
    }
}