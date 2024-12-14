public class KillTrayLinePool : TypedPool<KillTrayLine> {
    public static KillTrayLinePool Instance;

    protected override void Start() {
        base.Start();
        Instance = this;
    }
}