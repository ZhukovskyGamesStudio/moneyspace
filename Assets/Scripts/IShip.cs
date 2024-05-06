using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class IShip : MonoBehaviour {
    public abstract void RotateForward(Vector3 rotVector);
    public abstract float GetSpeedPercent();
    public abstract int GetHp();

    public Action OnDestroyed;

    public abstract void Accelerate();

    public abstract void Slowdown();

    public abstract void FirePrime(Vector3 target);

    public abstract void FireSecond(Vector3 target);

    public abstract void TakeDamage(int amount);

    public abstract void Respawn();
}