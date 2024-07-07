using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotsFactory : MonoBehaviour {
    [SerializeField]
    private TextAsset _botNamesFile;

    private List<string> _botNames;
    public static BotsFactory Instance;
    
    private void ParseFile() {
        _botNames = _botNamesFile.text.Split('\n').ToList();
    }

    private void Awake() {
        Instance = this;
        ParseFile();
    }

    public List<string> GetRandomBotsNames(int amount) {
        return _botNames.OrderBy(_ => Random.Range(0, 1f)).Take(amount).ToList();
    }
}
