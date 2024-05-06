using System;
using UnityEngine;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private LeaderboardDialog _leaderboardDialog;

    [SerializeField]
    private ARView _arView;

    [SerializeField]
    private PlayerHpView _playerHpView;

    [SerializeField]
    private AmmoView _ammoView;

    [SerializeField]
    private DeathmatchProgressView _deathmatchProgressView;

    [SerializeField]
    private ArMarkersManager _arMarkersManager;

    public ArMarkersManager ArMarkersManager => _arMarkersManager;
    
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