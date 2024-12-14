using System;
using UnityEngine;

public class LaserCanon : MonoBehaviour {
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _horizontalShift = 5;

    private Transform _transform;
    private void Start() {
        _transform = transform;
    }

    public void Shoot(Vector3 target,float lifetime, AbstractPilot owner) {
        Vector3 point = target + _transform.right * _horizontalShift;
        Vector3 direction = point - _transform.position;

        _transform.forward = direction;

        LaserBullet b = LasersPool.Instance.Get();
        b.Init(_shootPoint.position, direction, ShipsFactory.ShipStatsGeneralConfig.LaserSpeed, gameObject.layer, lifetime, owner);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,0.5f);
        Gizmos.DrawLine(transform.position ,transform.position + transform.forward*5);
    }
}