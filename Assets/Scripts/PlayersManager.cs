using System.Collections.Generic;
using UnityEngine;

public class PlayersManager {
    private List<PlayerData> _blueTeam = new List<PlayerData>();
    private List<PlayerData> _redTeam = new List<PlayerData>();

    private PlayerData _realPLayer;

    public List<PlayerData> BlueTeam => _blueTeam;
    public List<PlayerData> RedTeam => _redTeam;

    public void LoadPlayer() {
        Team playerTeam = Random.Range(0, 2) == 0 ? Team.Blue : Team.Red;
        _realPLayer = new PlayerData();
        _realPLayer.Nickname = "RealPlayer";
        _realPLayer.Team = playerTeam;
        if (playerTeam == Team.Blue) {
            _blueTeam.Add(_realPLayer);
        } else {
            _redTeam.Add(_realPLayer);
        }
    }

    public void GenerateBots(int amount) {
        int blueBots = amount / 2;
        int redBots = amount / 2;
        if (_realPLayer.Team == Team.Blue) {
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