public class LasersPool : TypedPool<LaserBullet> {
    public static LasersPool Instance;

    protected override void Start() {
        base.Start();
        Instance = this;
    }
}