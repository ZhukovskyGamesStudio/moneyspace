using System.Collections.Generic;

public class PlayersManager {
    private List<PlayerData> _blueTeam = new List<PlayerData>();
    private List<PlayerData> _redTeam = new List<PlayerData>();

    public static PlayerData RealPLayer;

    public List<PlayerData> BlueTeam => _blueTeam;
    public List<PlayerData> RedTeam => _redTeam;

    public void LoadPlayer() {
        Team playerTeam = Team.Blue; //Random.Range(0, 2) == 0 ? Team.Blue : Team.Red;
        RealPLayer = new PlayerData();
        RealPLayer.Nickname = MoneyspaceSaveLoadManager.Profile.Nickname;
        RealPLayer.Team = playerTeam;
        RealPLayer.AvatarIndex = MoneyspaceSaveLoadManager.Profile.SelectedPlayerIcon;
        if (playerTeam == Team.Blue) {
            _blueTeam.Add(RealPLayer);
        } else {
            _redTeam.Add(RealPLayer);
        }
    }

    public void GenerateBots(int playersInTeamAmount) {
        int blueBots = playersInTeamAmount;
        int redBots = playersInTeamAmount;
        if (RealPLayer.Team == Team.Blue) {
            blueBots--;
        } else {
            redBots--;
        }

        List<string> botnames = BotsFactory.Instance.GetRandomBotsNames(playersInTeamAmount*2+5);
        
        for (int i = 0; i < blueBots; i++) {
            
            var botData = PlayerData.RandomBot(botnames[i]);
            botData.Team = Team.Blue;
            _blueTeam.Add(botData);
        }

        for (int i = 0; i < redBots; i++) {
            var botData = PlayerData.RandomBot(botnames[blueBots+i]);
            botData.Team = Team.Red;
            _redTeam.Add(botData);
        }
    }
}