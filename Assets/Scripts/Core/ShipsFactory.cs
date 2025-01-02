using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipsFactory : BaseFactory {
    [SerializeField]
    private List<ShipConfig> _shipsConfigsList;

    [SerializeField]
    private ShipStatsGeneralConfig _shipStatsGeneralConfig;

    public static ShipStatsGeneralConfig ShipStatsGeneralConfig => Instance._shipStatsGeneralConfig;

    public static List<ShipConfig> Ships => Instance._shipsConfigsList;

    public static ShipsFactory Instance;

    public override void InitInstance() {
        base.InitInstance();
        Instance = this;
    }

    public static Ship GetShip(ShipType type) {
        IShip shipPrefab = Instance._shipsConfigsList.First(s => s.ShipType == type).Prefab;
        return Instantiate(shipPrefab) as Ship;
    }
}

[Serializable]
public enum ShipType {
    None = -1,
    First,
    Second,
    Third,
    Fouth,
    Fifth,
    HeavyFirst,
    HeavySecond,
    HeavyThird
}