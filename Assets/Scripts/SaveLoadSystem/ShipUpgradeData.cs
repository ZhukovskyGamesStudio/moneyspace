using System;

[Serializable]
public class ShipUpgradeData {
    public ShipType Type;
    public int Speed, Shield, Attack;

    public ShipUpgradeData Copy => new ShipUpgradeData() {
        Speed = Speed,
        Attack = Attack,
        Shield = Shield,
        Type = Type
    };
}