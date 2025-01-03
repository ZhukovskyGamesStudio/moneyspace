using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeaderboardDialog : MonoBehaviour {
    [SerializeField]
    private List<LeaderboardLineView> _blueLines, _redLines;

    private const int MAX_PLAYERS_IN_TEAM = 10;

    private PlayersManager _playersManager;

    [SerializeField]
    private GameObject _respawnContainer, _endContainer;

    [SerializeField]
    private Button _respawnButton;
    
    [SerializeField]
    private TextMeshProUGUI _respawnTimerText;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;
    
    private Action _onRespawnClicked;
    private bool _isFixedOpen;

    public void Init(PlayersManager playersManager) {
        _playersManager = playersManager;
        UpdateData();
    }

    public void UpdateData() {
        List<PlayerData> blueTeam = _playersManager.BlueTeam;
        List<PlayerData> redTeam = _playersManager.RedTeam;

        blueTeam = blueTeam.OrderByDescending(p => p.Kills).ThenByDescending(p => p.Assists).ThenBy(p => p.Deaths).ToList();
        redTeam = redTeam.OrderByDescending(p => p.Kills).ThenByDescending(p => p.Assists).ThenBy(p => p.Deaths).ToList();
        for (int i = 0; i < MAX_PLAYERS_IN_TEAM; i++) {
            if (_playersManager.BlueTeam.Count > i) {
                _blueLines[i].SetData(blueTeam[i]);
            } else {
                _blueLines[i].SetInactive();
            }
        }

        for (int i = 0; i < MAX_PLAYERS_IN_TEAM; i++) {
            if (blueTeam.Count > i) {
                _redLines[i].SetData(redTeam[i]);
            } else {
                _redLines[i].SetInactive();
            }
        }
    }

    public void OpenLbState() {
        if (_isFixedOpen) {
            return;
        }

        _animationHandler.ChangeWithAnimation(true);
        UpdateData();
        _respawnContainer.SetActive(false);
        _endContainer.SetActive(false);
    }

    public void CloseLbState() {
        if (_isFixedOpen) {
            return;
        }

        _animationHandler.ChangeWithAnimation(false);
    }

    public void OpenRespawnState(Action onRespawnButton) {
        _isFixedOpen = true;
        _respawnButton.interactable = true;
        _respawnTimerText.text = "Возродиться";
        _onRespawnClicked = onRespawnButton;
        _animationHandler.ChangeWithAnimation(true);
        UpdateData();
        _respawnContainer.SetActive(true);
        _endContainer.SetActive(false);
    }

    public void SetEndGameState() {
        StopAllCoroutines();
        _isFixedOpen = true;
        _animationHandler.ChangeWithAnimation(true);
        UpdateData();
        _respawnContainer.SetActive(false);
        _endContainer.SetActive(true);
    }

    public void RespawnButton() {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine() {
        _respawnButton.interactable = false;
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        float curRespawnTime = Random.Range(cnfg.MinPlayerRespawnTime, cnfg.MaxPlayerRespawnTime);
        while (curRespawnTime > 0) {
            curRespawnTime -= Time.deltaTime;
            _respawnTimerText.text = "Возрождение через " + Mathf.CeilToInt(curRespawnTime) + "..";
            yield return new WaitForEndOfFrame();
        }
        _onRespawnClicked?.Invoke();
        _animationHandler.ChangeWithAnimation(false);
        _isFixedOpen = false;
    }

    public void ToMenuButton() {
        LoadingPanel.ShowAndLoadScene("MenuScene");
    }
}