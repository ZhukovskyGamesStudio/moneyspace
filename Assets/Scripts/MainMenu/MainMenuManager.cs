using UnityEngine;
using YG;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private MainMenuUI _mainMenuUI;

    private static bool isYgGameReady = false;

    private void Start() {
        if (!isYgGameReady) {
            YandexGame.GameReadyAPI();
            isYgGameReady = true;
        }

        Cursor.lockState = CursorLockMode.Confined;
        _mainMenuUI.Init();
        _mainMenuUI.SetData(MoneyspaceSaveLoadManager.Profile);

        if (MoneyspaceSaveLoadManager.Profile.GamesWonAmount > 1) {
            YandexGame.ReviewShow(true);
        }
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
#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            GiveCoinsAfterRewAd();
        }
    }
#endif

    public void WatchAdButton() {
        YgHandler handler = new YgHandler();
        handler.ShowRewarded(GiveCoinsAfterRewAd);

        _mainMenuUI.SetData(MoneyspaceSaveLoadManager.Profile);
        _mainMenuUI.CloseDialogs();
    }

    private void GiveCoinsAfterRewAd() {
        MoneyspaceSaveLoadManager.Instance.EarnCoins(MainConfigTable.Instance.MainGameConfig.RewardForWatchAd, "menuAd");
        MoneyspaceSaveLoadManager.Save();
        MainMenuUI.Instance.SetData(MoneyspaceSaveLoadManager.Profile);
    }

    public void OpenPlayerData() {
        _mainMenuUI.PlayerSelectDialog.Toggle();
        _mainMenuUI.ShipUpgradeDialog.Close();
        _mainMenuUI.SettingsDialog.Close();
    }
}