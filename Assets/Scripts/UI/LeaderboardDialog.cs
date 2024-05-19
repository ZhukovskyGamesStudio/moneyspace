using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardDialog : MonoBehaviour {
    [SerializeField]
    private List<LeaderboardLineView> _blueLines, _redLines;

    private const int MAX_PLAYERS_IN_TEAM = 10;

    private PlayersManager _playersManager;

    [SerializeField]
    private GameObject _respawnContainer, _endContainer;

    private Action _onRespawnClicked;
    private bool _isFixedOpen;
    private void OnEnable() {
        UpdateData();
    }

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
        gameObject.SetActive(true);
        _respawnContainer.SetActive(false);
        _endContainer.SetActive(false);
    }

    public void CloseLbState() {
        if (_isFixedOpen) {
            return;
        }
        gameObject.SetActive(false);
    }


    
    public void OpenRespawnState(Action onRespawnButton) {
        _isFixedOpen = true;
        _onRespawnClicked = onRespawnButton;
        gameObject.SetActive(true);
        _respawnContainer.SetActive(true);
        _endContainer.SetActive(false);
    }

    public void SetEndGameState() {
        _isFixedOpen = true;
        _respawnContainer.SetActive(false);
        _endContainer.SetActive(true);
    }

    public void RespawnButton() {
        _onRespawnClicked?.Invoke();
        gameObject.SetActive(false);
        _isFixedOpen = false;
    }
}