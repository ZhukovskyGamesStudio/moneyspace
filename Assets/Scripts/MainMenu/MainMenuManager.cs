using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public void PlayButton() {
        SceneManager.LoadScene("GameScene");
    }

    public void UpgradesButton() {
        Debug.Log("UpgradesButton");
    }

    public void SettingsButton() {
        Debug.Log("SettingsButton");
    }

    public void WatchAdButton() {
        Debug.Log("WatchAdButton");
    }

    public void OpenPlayerData() {
        Debug.Log("OpenPlayerData");
    }
}
