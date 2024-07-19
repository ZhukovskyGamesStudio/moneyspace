using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SaveProfile {
    public float MasterVolume = 1f, EffectVolume = 0.5f, MusicVolume = 0.5f;
    public int CoinsAmount = 0;
    public int SelectedShip = 0;
    public int SelectedPlayerIcon = 0;
    public string Nickname = "unknownPlayer";
    public List<int> BoughtIcons = new List<int>();
    public List<ShipUpgradeData> ShipUpgradeDatas = new List<ShipUpgradeData>();
    public bool IsFtueDialogSeen = false;
    public int GamesPlayedAmount = 0;
    public int GamesWonAmount = 0;
    public int KillsAmount = 0;

    public static SaveProfile Load(int profileIndex) {
        string key = "saveProfile_" + profileIndex;
        if (!PlayerPrefs.HasKey(key)) {
            SaveProfile empty = SaveProfile.Empty();
            Save(empty, profileIndex);
        }

        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<SaveProfile>(json);
    }

    public static void Save(SaveProfile profile, int profileIndex) {
        string key = "saveProfile_" + profileIndex;
        string json = JsonUtility.ToJson(profile);
        PlayerPrefs.SetString(key, json);
    }

    public static SaveProfile Empty() {
        return new SaveProfile() {
            CoinsAmount = MainConfigTable.Instance.MainGameConfig.StartingCoinsAmount,
            SelectedShip = 0,
            SelectedPlayerIcon = 0,
            Nickname = "User" + Random.Range(1000,9999),
            ShipUpgradeDatas = new List<ShipUpgradeData>() {
                ShipsFactory.Ships.First(s=>s.ShipType == ShipType.First).DefaultShipUpgrades
            },
            BoughtIcons = new List<int>() {
                0,
            }
        };
    }
}