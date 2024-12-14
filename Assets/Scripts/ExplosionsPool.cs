public class ExplosionsPool : TypedPool<Explosion> {
    public static ExplosionsPool Instance;

    protected override void Start() {
        base.Start();
        Instance = this;
    }
}