public class KillTrayLinePool : TypedPool<KillTrayLine> {
    public static KillTrayLinePool Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
    }
}