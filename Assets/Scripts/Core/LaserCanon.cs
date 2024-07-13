using System;
using UnityEngine;

public class LaserCanon : MonoBehaviour {
    [SerializeField]
    private LaserBullet _laserBulletPrefab;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private float _horizontalShift = 5;

    public void Shoot(Vector3 target,float lifetime, AbstractPilot owner) {
        Vector3 point = target + transform.right * _horizontalShift;
        Vector3 direction = point - transform.position;

        transform.forward = direction;

        LaserBullet b = Instantiate(_laserBulletPrefab, _shootPoint.position, Quaternion.identity);
        b.Init(direction, ShipsFactory.ShipStatsGeneralConfig.LaserSpeed, gameObject.layer,lifetime, owner);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,0.5f);
        Gizmos.DrawLine(transform.position ,transform.position + transform.forward*5);
    }
}