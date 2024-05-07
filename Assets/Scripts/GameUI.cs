using System;
using UnityEngine;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private LeaderboardDialog _leaderboardDialog;

    [SerializeField]
    public ARView _arView;

    [SerializeField]
    public PlayerHpView _playerHpView;

    [SerializeField]
    public AmmoView _ammoView;

    [SerializeField]
    public DeathmatchProgressView _deathmatchProgressView;

    [SerializeField]
    private ArMarkersManager _arMarkersManager;

    public ArMarkersManager ArMarkersManager => _arMarkersManager;
    public LeaderboardDialog LeaderboardDialog => _leaderboardDialog;
    
    public static GameUI Instance;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            _leaderboardDialog.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            _leaderboardDialog.gameObject.SetActive(false);
        }
    }
}