using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotsFactory : BaseFactory {
    [SerializeField]
    private TextAsset _botNamesFile;

    private List<string> _botNames;
    public static BotsFactory Instance;

    private void ParseFile() {
        string cleaned = _botNamesFile.text.Replace("\r", "");
        _botNames = cleaned.Split('\n').ToList();
    }

    public override void InitInstance() {
        base.InitInstance();
        Instance = this;
        ParseFile();
    }

    public List<string> GetRandomBotsNames(int amount) {
        return _botNames.OrderBy(_ => Random.Range(0, 1f)).Take(amount).ToList();
    }
}