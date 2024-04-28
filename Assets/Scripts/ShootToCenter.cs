using UnityEngine;

public class ShootToCenter : MonoBehaviour {
    [SerializeField]
    private LaserBullet _laserBulletPrefab;

    [SerializeField]
    private float _bulletSpeed = 5;

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void Shoot() {
        LaserBullet b = Instantiate(_laserBulletPrefab);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Vector3 point = ray.GetPoint(100);

        b.Init(point - transform.position, _bulletSpeed);
    }
}