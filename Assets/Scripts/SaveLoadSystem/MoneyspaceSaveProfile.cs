using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class MoneyspaceSaveProfile : SaveProfile {
    public float MasterVolume = 1f, EffectVolume = 0.5f, MusicVolume = 0.5f;
    public int CoinsAmount;
    public int SelectedShip;
    public int SelectedPlayerIcon;
    public string Nickname = "unknownPlayer";
    public List<int> BoughtIcons = new List<int>();
    public List<ShipUpgradeData> ShipUpgradeDatas = new List<ShipUpgradeData>();
    public bool IsFtueDialogSeen;
    public int GamesPlayedAmount;
    public int GamesWonAmount;
    public int KillsAmount;

    public MoneyspaceSaveProfile() {
        CoinsAmount = MainConfigTable.Instance.MainGameConfig.StartingCoinsAmount;
        SelectedShip = 0;
        SelectedPlayerIcon = 0;
        Nickname = "User" + Random.Range(1000, 9999);
        ShipUpgradeDatas = new List<ShipUpgradeData>() {
            ShipsFactory.Ships.First(s => s.ShipType == ShipType.First).DefaultShipUpgrades
        };
        BoughtIcons = new List<int>() {
            0,
        };
    }
}