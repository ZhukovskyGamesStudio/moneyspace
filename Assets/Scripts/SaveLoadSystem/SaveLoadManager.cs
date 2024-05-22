using System;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {
    public static SaveLoadManager Instance;
    public static SaveProfile Profile;

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        int index = PlayerPrefs.GetInt("profileIndex", 0);
        ChooseProfile(index);
    }

    public void ChooseProfile(int index) {
        Profile = SaveProfile.Load(index);
        PlayerPrefs.SetInt("profileIndex", index);
    }

    public static void Save() {
        SaveProfile.Save(Profile,PlayerPrefs.GetInt("profileIndex", 0) );
    }
}