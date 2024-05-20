using System;
using UnityEngine;

public class AbstractPilot : MonoBehaviour {
    protected IShip _ship;
    protected PlayerData _playerData;

    public virtual void Init() { }

    public virtual void Activate() { }

    public virtual void DeActivate() {
        enabled = false;
        StopAllCoroutines();
        _ship.gameObject.SetActive(false);
    }

    public void SetPlayerData(PlayerData data) {
        _playerData = data;
    }

    protected virtual ShipType GetShipType() {
        throw new NotImplementedException();
    }

    protected virtual void GetShip() {
        _ship = ShipsFactory.GetShip(GetShipType());
        _ship.SetOwner(_playerData);
        _ship.gameObject.SetActive(false);
        _ship.transform.SetParent(transform);
        _ship.name = _playerData.Nickname + "ship";
        _ship.tag = _playerData.Team.ToString();

        var layer = LayerMask.NameToLayer(_playerData.Team == Team.Blue ? "BlueTeam" : "RedTeam");
        _ship.gameObject.layer = layer;
        var childs = _ship.GetComponentsInChildren<Transform>();
        foreach (var VARIABLE in childs) {
            VARIABLE.gameObject.layer = layer;
        }
    }

    protected void RespawnShip() {
        Transform spawnPoint = SpawnPoints.GetRandomSpawnPoint(_playerData.Team);
        transform.SetParent(spawnPoint);
        _ship.transform.position = spawnPoint.position;
        _ship.gameObject.SetActive(true);
        _ship.Respawn();
    }
}