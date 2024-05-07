using DefaultNamespace;
using UnityEngine;

public class PrepareGameManager : MonoBehaviour {

    [SerializeField]
    private PilotsManager _pilotsManager;

    private PlayersManager _playersManager = new PlayersManager();

    [SerializeField]
    private int _playersAmount = 9;
    
    [SerializeField]
    private int _startingScore = 100;

    public void Start() {
        _playersManager.LoadPlayer();
        _playersManager.GenerateBots(_playersAmount);
        _pilotsManager.GeneratePilots(_playersManager.BlueTeam, _playersManager.RedTeam);
        GameUI.Instance.LeaderboardDialog.SetData(_playersManager.BlueTeam, _playersManager.RedTeam);
        
        
        _pilotsManager.ActivatePilots();
        GameManager.Instance.RespawnManager.SetStartingScore(_startingScore);
    }
}