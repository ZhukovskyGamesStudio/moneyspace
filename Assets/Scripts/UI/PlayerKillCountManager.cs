using System;
using UnityEngine;
using YG;

public class PlayerKillCountManager : MonoBehaviour {
    public static PlayerKillCountManager Instance;
    private int _killCount;

    private void Awake() {
        Instance = this;
    }

    public void AddOne() {
        _killCount++;
        MoneyspaceSaveLoadManager.Profile.KillsAmount++;
        MoneyspaceSaveLoadManager.Save();
        YandexGame.NewLeaderboardScores( "killsTotal", MoneyspaceSaveLoadManager.Profile.KillsAmount);
        
        GameUI.Instance.UiMessages.TimedMessage.PlaySound(true);
        switch (_killCount) {
            case 1:
                GameUI.Instance.UiMessages.TimedMessage.ShowText("Первое убийство!", 3, Color.gray);
                break;
            case 2:
                GameUI.Instance.UiMessages.TimedMessage.ShowText("Двойное убийство!!", 3, Color.white);
                break;
            case 3:
                GameUI.Instance.UiMessages.TimedMessage.ShowText("Тройное убийство!!!", 4, Color.yellow);
                break;
            case 4:
                GameUI.Instance.UiMessages.TimedMessage.ShowText("Ультра убийство!!!!", 4, Color.red);
                break;
            default:
                GameUI.Instance.UiMessages.TimedMessage.ShowText("Серия убийств: " + _killCount + "!!!!!", 5, Color.magenta);
                break;
        }
    }

    public void Drop(string killerName = "") {
        _killCount = 0;
        string msg = killerName != "" ? $"Вас взорвал {killerName}!" : "Вы взорвались!";
        GameUI.Instance.UiMessages.TimedMessage.PlaySound(false);
        GameUI.Instance.UiMessages.TimedMessage.ShowText(msg, 5, Color.gray);
    }
}