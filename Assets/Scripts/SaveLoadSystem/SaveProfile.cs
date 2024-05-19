using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveProfile {
    public int CoinsAmount = 0;

    public List<ShipUpgradeData> ShipUpgradeData = new List<ShipUpgradeData>();

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
            CoinsAmount = 0,
            ShipUpgradeData = new List<ShipUpgradeData>()
        };
    }
}