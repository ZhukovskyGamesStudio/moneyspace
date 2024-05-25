using System.Collections.Generic;
using UnityEngine;

public class PlayersManager {
    private List<PlayerData> _blueTeam = new List<PlayerData>();
    private List<PlayerData> _redTeam = new List<PlayerData>();

    public static PlayerData RealPLayer;

    public List<PlayerData> BlueTeam => _blueTeam;
    public List<PlayerData> RedTeam => _redTeam;

    public void LoadPlayer() {
        Team playerTeam = Team.Blue;//Random.Range(0, 2) == 0 ? Team.Blue : Team.Red;
        RealPLayer = new PlayerData();
        RealPLayer.Nickname = "RealPlayer";
        RealPLayer.Team = playerTeam;
        if (playerTeam == Team.Blue) {
            _blueTeam.Add(RealPLayer);
        } else {
            _redTeam.Add(RealPLayer);
        }
    }

    public void GenerateBots(int playersAmount) {
        int blueBots = playersAmount / 2;
        int redBots = playersAmount / 2;
        if (RealPLayer.Team == Team.Blue) {
            blueBots--;
        } else {
            redBots--;
        }

        for (int i = 0; i < blueBots; i++) {
            var botData = PlayerData.RandomBot();
            botData.Team = Team.Blue;
            _blueTeam.Add(botData);
        }

        for (int i = 0; i < redBots; i++) {
            var botData = PlayerData.RandomBot();
            botData.Team = Team.Red;
            _redTeam.Add(botData);
        }
    }
}