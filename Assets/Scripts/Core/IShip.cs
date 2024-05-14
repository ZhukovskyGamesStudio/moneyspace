using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class IShip : MonoBehaviour {
    public void SetOwner(PlayerData data) {
        _owner = data;
    }

    protected PlayerData _owner;
    public abstract void RotateForward(Vector3 rotVector);
    public abstract float GetSpeedPercent();
    public abstract float GetHpPercent();

    public Action<PlayerData, PlayerData> OnDestroyed;

    public VisibleChecker _visibleChecker;

    public abstract void Accelerate();

    public abstract void Slowdown();

    public abstract void FirePrime(Vector3 target);

    public abstract void FireSecond(Vector3 target);

    public abstract void TakeDamage(int amount, PlayerData from);

    public abstract void Respawn();

    public abstract Transform GetCameraFollowTarget();
}