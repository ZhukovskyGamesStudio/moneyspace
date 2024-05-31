using System;
using UnityEngine;

public class LaserCanon : MonoBehaviour {
    [SerializeField]
    private LaserBullet _laserBulletPrefab;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _horizontalShift = 5;

    public void Shoot(Vector3 target, AbstractPilot owner) {
        Vector3 point = target + transform.right * _horizontalShift;
        Vector3 direction = point - transform.position;

        transform.forward = direction;

        LaserBullet b = Instantiate(_laserBulletPrefab, _shootPoint.position, Quaternion.identity);
        b.Init(direction, ShipsFactory.ShipStatsGeneralConfig.LaserSpeed, gameObject.layer, owner);
    }
}