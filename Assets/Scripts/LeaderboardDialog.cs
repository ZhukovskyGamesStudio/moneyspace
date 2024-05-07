using System.Collections.Generic;
using UnityEngine;

public class LeaderboardDialog : MonoBehaviour {
    [SerializeField]
    private List<LeaderboardLineView> _blueLines, _redLines;

    private const int MAX_PLAYERS_IN_TEAM = 10;

    public void SetData(List<PlayerData> blueTeam, List<PlayerData> redTeam) {
        for (int i = 0; i < MAX_PLAYERS_IN_TEAM; i++) {
            if (blueTeam.Count > i) {
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