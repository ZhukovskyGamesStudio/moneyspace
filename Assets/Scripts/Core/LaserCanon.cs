using System;
using UnityEngine;

public class LaserCanon : MonoBehaviour {
    [SerializeField]
    private LaserBullet _laserBulletPrefab;

    [SerializeField]
    private float _bulletSpeed = 35;

    [SerializeField]
    private Transform _shootPoint;

   

    [SerializeField]
    private float _horizontalShift = 5;

    public void Shoot(Vector3 target) {
        LaserBullet b = Instantiate(_laserBulletPrefab, _shootPoint.position, Quaternion.identity);
        b.gameObject.layer = gameObject.layer;
        Vector3 point = target + transform.right * _horizontalShift;
        Vector3 direction = point - transform.position;
        transform.forward = direction;
        b.Init(direction, _bulletSpeed);
    }
}