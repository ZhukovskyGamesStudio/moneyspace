using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public static float FightRadius; 

    public RespawnManager RespawnManager = new RespawnManager();
    
    public PilotsManager PilotsManager;

    public PlayersManager PlayersManager = new PlayersManager();

        
    private void Awake() {
        Instance = this;
    }
}