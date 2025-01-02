using System;
using UnityEngine;

public abstract class IShip : MonoBehaviour {
    [SerializeField]
    private ShipType _shipType;
    
    public ShipType ShipType => _shipType;
    
    [HideInInspector]
    public Vector3 RotateByV = Vector3.zero;
    [HideInInspector]
    public Quaternion RotateToQ = Quaternion.identity;

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
    
    public abstract void RotateTo(Quaternion rotVector);
    public abstract float GetSpeedPercent();
    public abstract float GetOverheatPercent();
    public abstract float GetHpPercent();
    public abstract float GetShieldPercent();

    public abstract int GetLaserDamage();

    public Action<AbstractPilot, AbstractPilot> OnDestroyed;
    public Action<AbstractPilot> OnTakeDamage;

    public abstract void Accelerate();

    public abstract void Slowdown();


    public abstract void SlowdownKeyDown();

    public abstract void FirePrime(Vector3 target);

    public abstract void FireSecond(Vector3 target);

    public abstract void TakeDamage(int amount, AbstractPilot fromPilot);

    public abstract void Respawn();

    public abstract Transform GetCameraFollowTarget();
}