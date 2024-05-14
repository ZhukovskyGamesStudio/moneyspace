using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardDialog : MonoBehaviour {
    [SerializeField]
    private List<LeaderboardLineView> _blueLines, _redLines;

    private const int MAX_PLAYERS_IN_TEAM = 10;

    private PlayersManager _playersManager;

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
}