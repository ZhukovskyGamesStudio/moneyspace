using UnityEngine;

public class MainConfigTable : BaseFactory {
    public static MainConfigTable Instance;

    [SerializeField]
    private MainGameConfig _mainGameConfig;

    public MainGameConfig MainGameConfig => _mainGameConfig;

    public override void InitInstance() {
        base.InitInstance();
        Instance = this;
    }
}