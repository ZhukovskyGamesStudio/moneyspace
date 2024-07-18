public class LasersPool : TypedPool<LaserBullet> {
    public static LasersPool Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
    }
}