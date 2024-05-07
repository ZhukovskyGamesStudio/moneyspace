using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoints : MonoBehaviour {

    public static SpawnPoints Instance;
    [SerializeField]
    private List<Transform> _spawnPointsBlue;

    public List<Transform> SpawnPointsBlue => _spawnPointsBlue;

    [SerializeField]
    public List<Transform> _spawnPointsRed;

    public List<Transform> SpawnPointsRed => _spawnPointsRed;

    private void Awake() {
        Instance = this;
    }

    public static Transform GetRandomSpawnPoint(Team team) {
        List<Transform> list = Instance._spawnPointsRed;
        if (team == Team.Blue) {
            list = Instance._spawnPointsBlue;
        }

        return list[Random.Range(0, list.Count)];
    }
}