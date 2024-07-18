using System;
using UnityEngine;

public class LaserCanon : MonoBehaviour {
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _horizontalShift = 5;

    private Transform _transform;
    private void Awake() {
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
        Gizmos.DrawWireSphere(_transform.position,0.5f);
        Gizmos.DrawLine(_transform.position ,_transform.position + _transform.forward*5);
    }
}