using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipsFactory : MonoBehaviour {
    [SerializeField]
    private List<ShipConfig> _shipsConfigsList;

    [SerializeField]
    private ShipStatsGeneralConfig _shipStatsGeneralConfig;

    public static ShipStatsGeneralConfig ShipStatsGeneralConfig => Instance._shipStatsGeneralConfig;
    
    public static List<ShipConfig> Ships => Instance._shipsConfigsList;
    
    public static ShipsFactory Instance;

    private void Awake() {
        Instance = this;
    }

    public static IShip GetShip(ShipType type) {
        IShip shipPrefab = Instance._shipsConfigsList.First(s => s.ShipType == type).Prefab;
        return Instantiate(shipPrefab);
    }
}

[Serializable]
public enum ShipType {
    None = -1,
    First,
    Second,
    Third,
    Fouth
}