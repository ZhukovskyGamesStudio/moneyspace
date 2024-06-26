using System;
using UnityEngine;

public abstract class IShip : MonoBehaviour {
    [SerializeField]
    private ShipType _shipType;

    public ShipType ShipType => _shipType;

    protected ShipConfig _shipConfig;
    protected ShipUpgradeData _shipUpgradeData;

    protected int MaxShied => _maxShield;
    private int _maxShield;

    public void InitFromDefaultConfig(ShipConfig shipConfig) {
        InitFromConfig(shipConfig, shipConfig.DefaultShipUpgrades);
    }

    public void InitFromConfig(ShipConfig shipConfig, ShipUpgradeData shipUpgradeData) {
        _shipConfig = shipConfig;
        _shipUpgradeData = shipUpgradeData;
        _maxShield = _shipUpgradeData.Shield * ShipsFactory.ShipStatsGeneralConfig.ShieldPerPoint;
    }

    public void SetOwner(AbstractPilot data) {
        _owner = data;
    }

    public AbstractPilot GetOwner() {
        return _owner;
    }
    
    protected AbstractPilot _owner;
    public abstract void RotateBy(Vector3 rotVector);
    public abstract float GetSpeedPercent();
    public abstract float GetOverheatPercent();
    public abstract float GetHpPercent();
    public abstract float GetShieldPercent();

    public Action<AbstractPilot, AbstractPilot> OnDestroyed;

    [SerializeField]
    private VisibleChecker _visibleChecker;

    public VisibleChecker VisibleChecker => _visibleChecker;

    public abstract void Accelerate();

    public abstract void Slowdown();


    public abstract void SlowdownKeyDown();

    public abstract void FirePrime(Vector3 target);

    public abstract void FireSecond(Vector3 target);

    public abstract void TakeDamage(int amount, AbstractPilot fromPilot);

    public abstract void Respawn();

    public abstract Transform GetCameraFollowTarget();
}