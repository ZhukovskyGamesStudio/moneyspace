using UnityEngine;

public class PrepareGameManager : MonoBehaviour {
    [SerializeField]
    private RespawnManager _respawnManager;

    [SerializeField]
    private PilotsManager _pilotsManager;

    private PlayersManager _playersManager = new PlayersManager();

    [SerializeField]
    private int _playersAmount = 9;

    public void Start() {
        _playersManager.LoadPlayer();
        _playersManager.GenerateBots(_playersAmount);
        _pilotsManager.GeneratePilots(_playersManager.BlueTeam, _playersManager.RedTeam);
        GameUI.Instance.LeaderboardDialog.SetData(_playersManager.BlueTeam, _playersManager.RedTeam);
        
        
        _pilotsManager.ActivatePilots();
    }
}