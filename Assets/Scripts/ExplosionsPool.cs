public class ExplosionsPool : TypedPool<Explosion> {
    public static ExplosionsPool Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
    }
}