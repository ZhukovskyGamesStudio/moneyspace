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
    }

    private void UpdateUI() {
        GameUI.Instance._deathmatchProgressView.SetData((_blueTeamScore + 0f) / _startingScore, _blueTeamScore,
            (_redTeamScore + 0f) / _startingScore, _redTeamScore);
    }
}