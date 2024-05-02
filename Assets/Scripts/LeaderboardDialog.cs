using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeaderboardDialog : MonoBehaviour {
    [SerializeField]
    private List<LeaderboardLineView> _blueLines, _redLines;

    private const int MAX_PLAYERS_IN_TEAM = 10;

    private void Awake() {
        int blueTeamAmount = Random.Range(0, MAX_PLAYERS_IN_TEAM + 1);
        int redTeamAmount = Random.Range(0, MAX_PLAYERS_IN_TEAM + 1);

        List<PlayerData> blue = GetRndTeam(blueTeamAmount);
        List<PlayerData> red = GetRndTeam(redTeamAmount);

        SetData(blue, red);
    }

    private List<PlayerData> GetRndTeam(int amount) {
        List<PlayerData> team = new List<PlayerData>();
        for (int i = 0; i < amount; i++) {
            team.Add(new PlayerData() {
                Nickname = "Player#" + Random.Range(0, 9999),
                Kills = Random.Range(0, 30),
                Deaths = Random.Range(0, 30),
                Assists = Random.Range(0, 30),
                AvatarIndex = 0
            });
        }

        return team;
    }

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