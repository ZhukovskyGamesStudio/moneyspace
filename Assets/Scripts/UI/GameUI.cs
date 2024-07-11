using System;
using UnityEngine;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private LeaderboardDialog _leaderboardDialog;

    [SerializeField]
    private RewardPanel _endGameDialog;

    [SerializeField]
    public ARView _arView;

    [SerializeField]
    private UIMessages _uiMessages;

    public UIMessages UiMessages => _uiMessages;

    [SerializeField]
    public PlayerHpView _playerHpView;

    [SerializeField]
    public AmmoView _ammoView;

    [SerializeField]
    public DeathmatchProgressView _deathmatchProgressView;

    [SerializeField]
    private ArMarkersManager _arMarkersManager;

    [SerializeField]
    private KillsTray _killsTray;

    [SerializeField]
    private SmartCursorView _smartCursorView;

    public SmartCursorView SmartCursorView => _smartCursorView;
    public KillsTray KillsTray => _killsTray;

    public ArMarkersManager ArMarkersManager => _arMarkersManager;
    public LeaderboardDialog LeaderboardDialog => _leaderboardDialog;

    public RewardPanel EndGameDialog => _endGameDialog;

    public static GameUI Instance;

    public GameObject warpOnSpeed;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            _leaderboardDialog.OpenLbState();
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            _leaderboardDialog.CloseLbState();
        }
    }
}