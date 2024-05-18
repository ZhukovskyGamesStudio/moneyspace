using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipsFactory : MonoBehaviour {
    [SerializeField]
    private List<IShip> _shipsList;

    public static ShipsFactory Instance;

    private void Awake() {
        Instance = this;
    }

    public static IShip GetShip(ShipType type) {
        IShip shipPrefab = Instance._shipsList.First(s => s.ShipType == type);
        return Instantiate(shipPrefab);
    }
}

[Serializable]
public enum ShipType {
    None,
    First,
    Second,
    Third,
    Fouth
}