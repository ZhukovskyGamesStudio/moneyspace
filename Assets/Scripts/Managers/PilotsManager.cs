using System.Collections.Generic;
using UnityEngine;

public class PilotsManager : MonoBehaviour {
    [SerializeField]
    private PlayerPilot _playerPilotPrefab;

    [SerializeField]
    private BotPilot _botPilotPrefab;

    [SerializeField]
    private Transform _pilotsHolder;

    private List<AbstractPilot> _pilots = new List<AbstractPilot>();

    public void GeneratePilots(List<PlayerData> blue, List<PlayerData> red) {
        foreach (var playerData in blue) {
            if (playerData.isBot) {
                BotPilot botPilot = Instantiate(_botPilotPrefab, _pilotsHolder);
                botPilot.SetPlayerData(playerData);
                _pilots.Add(botPilot);
            } else {
                PlayerPilot playerPilot = Instantiate(_playerPilotPrefab, _pilotsHolder);
                playerPilot.SetPlayerData(playerData);
                _pilots.Add(playerPilot);
            }
        }

        foreach (var playerData in red) {
            if (playerData.isBot) {
                BotPilot botPilot = Instantiate(_botPilotPrefab, _pilotsHolder);
                botPilot.SetPlayerData(playerData);
                _pilots.Add(botPilot);
            } else {
                PlayerPilot playerPilot = Instantiate(_playerPilotPrefab, _pilotsHolder);
                playerPilot.SetPlayerData(playerData);
                _pilots.Add(playerPilot);
            }
        }
    }

    public void ActivatePilots() {
        foreach (AbstractPilot variable in _pilots) {
            variable.Init();
        }

        foreach (AbstractPilot variable in _pilots) {
            variable.Activate();
        }
    }

    public void DeactivatePilots() {
        foreach (AbstractPilot variable in _pilots) {
            variable.DeActivate();
        }
    }
}