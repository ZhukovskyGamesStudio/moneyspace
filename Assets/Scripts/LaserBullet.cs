using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private float _speed;

    public void Init(Vector3 dir, float speed) {
        transform.forward = dir;
        _speed = speed;

        Destroy(gameObject, 3);
    }

    private void FixedUpdate() {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }
}