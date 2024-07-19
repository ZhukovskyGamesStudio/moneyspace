using YG;

public class RespawnManager {
    private int _startingScore;

    private int _blueTeamScore, _redTeamScore;

    public void SetStartingScore(int startingScore) {
        _startingScore = startingScore;
        _blueTeamScore = _startingScore;
        _redTeamScore = _startingScore;
        UpdateUI();
    }

    public void MinusPoint(Team team) {
        if (team == Team.Blue) {
            _blueTeamScore--;
        } else {
            _redTeamScore--;
        }
        UpdateUI();
        if (_blueTeamScore == 0 || _redTeamScore == 0) {
            EndGame();
        }
    }

    private void UpdateUI() {
        GameUI.Instance._deathmatchProgressView.SetData((_blueTeamScore + 0f) / _startingScore, _blueTeamScore,
            (_redTeamScore + 0f) / _startingScore, _redTeamScore);
    }

    private void EndGame() {
        GameManager.Instance.PilotsManager.DeactivatePilots();

        bool isPlayerWon = _redTeamScore == 0;
        SaveLoadManager.Profile.GamesPlayedAmount++;
        if (isPlayerWon) {
            SaveLoadManager.Profile.GamesWonAmount++;
        }

        SaveLoadManager.Save();
        YandexGame.NewLeaderboardScores( "gamesWon", SaveLoadManager.Profile.GamesWonAmount);

        GameUI.Instance.EndGameDialog.Show(PlayersManager.RealPLayer.Kills, PlayersManager.RealPLayer.Assists, isPlayerWon);
    }
}