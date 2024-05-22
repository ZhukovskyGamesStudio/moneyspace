using UnityEngine;

public class MainConfigTable : MonoBehaviour {

    public static MainConfigTable Instance;

    [SerializeField]
    private MainGameConfig _mainGameConfig;

    public MainGameConfig MainGameConfig => _mainGameConfig;
    private void Awake() {
        Instance = this;
    }
}