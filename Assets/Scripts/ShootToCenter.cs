using System;
using UnityEngine;

public class ShootToCenter : MonoBehaviour {
    [SerializeField]
    private LaserBullet _laserBulletPrefab;

    [SerializeField]
    private float _bulletSpeed = 35;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private bool _isMouseTarget = false;

    [SerializeField]
    private float _horizontalShift = 5;

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void Shoot() {
        LaserBullet b = Instantiate(_laserBulletPrefab, _shootPoint.position, Quaternion.identity);
        Vector3 screenPos = _isMouseTarget ? Input.mousePosition : new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        transform.forward = ray.direction;
        Vector3 point = ray.GetPoint(10000) + transform.right * _horizontalShift;

        b.Init(point - transform.position, _bulletSpeed);
    }
}