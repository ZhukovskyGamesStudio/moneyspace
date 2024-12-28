using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbstractPilot : MonoBehaviour {
    protected Ship _ship;
    protected PlayerData _playerData;
    protected bool _isActive = false;
    public PlayerData PlayerData => _playerData;
    public Ship Ship => _ship as Ship;

    public ShipType ShipType => _ship.ShipType;
    public virtual void Init() { }

    public virtual void Activate() {
        _isActive = true;
        MinimapIconsContainer.Instance.AddIcon(this);
    }

    public virtual void DeActivate() {
        _isActive = false;
        StopAllCoroutines();
        _ship.gameObject.SetActive(false);
        enabled = false;
    }

    public void SetPlayerData(PlayerData data) {
        _playerData = data;
    }

    protected virtual ShipType GetShipType() {
        throw new NotImplementedException();
    }

    protected virtual void GetShip() {
        _ship = ShipsFactory.GetShip(GetShipType()) as Ship;
        _ship.SetOwner(this);
        _ship.gameObject.SetActive(false);
        _ship.transform.SetParent(transform);
        _ship.name = _playerData.Nickname + "ship";
        _ship.tag = _playerData.Team.ToString();
        _ship.OnDestroyed += LogDestroyedToTray;
        
        SetChildsLayer();
    }

    private void SetChildsLayer() {
        int layer = LayerMask.NameToLayer(_playerData.Team == Team.Blue ? "BlueTeam" : "RedTeam");
        _ship.gameObject.layer = layer;
        Transform[] children = _ship.GetComponentsInChildren<Transform>();
        foreach (Transform child in children) {
            if (child.gameObject.layer == LayerMask.NameToLayer("Minimap")) {
                continue;
            }
            child.gameObject.layer = layer;
        }
    }

    private void LogDestroyedToTray(AbstractPilot victim, AbstractPilot killer) {
        GameUI.Instance.KillsTray.AddToTray(killer, victim);
        if (killer != null && !killer.PlayerData.isBot) {
            PlayerKillCountManager.Instance.AddOne();
        }

        if (victim != null && !victim.PlayerData.isBot) {
            string killerName = killer != null && killer.PlayerData.isBot ? killer.PlayerData.Nickname : "";
            PlayerKillCountManager.Instance.Drop(killerName);
        }
    }

    protected virtual void RespawnShip() {
        Transform spawnPoint = SpawnPoints.GetRandomSpawnPoint(_playerData.Team);
        transform.SetParent(spawnPoint);
        _ship.transform.position = spawnPoint.position + Random.insideUnitSphere * 50;
        _ship.gameObject.SetActive(true);
        _ship.Respawn();
    }
}