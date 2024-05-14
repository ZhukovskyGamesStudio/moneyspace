using Unity.Mathematics;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private float _speed;

    [SerializeField]
    private int _damageAmount = 10;

    [SerializeField]
    private Explosion _explosion;

    public void Init(Vector3 dir, float speed, int layer) {
        transform.forward = dir;
        _speed = speed;
        gameObject.layer = layer;
        transform.GetChild(0).gameObject.layer = layer;
        Destroy(gameObject, 3);
    }

    private void FixedUpdate() {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        Rigidbody rb = collision.GetContact(0).otherCollider.attachedRigidbody;
        if (rb != null) {
            Ship ship = rb.GetComponent<Ship>();
            if (ship != null) {
                ship.TakeDamage(_damageAmount);
            }
        }

        Explode(collision);
        Destroy(gameObject);
    }

    private void Explode(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        Explosion explosion = Instantiate(_explosion, transform.position, quaternion.identity, collision.GetContact(0).otherCollider.transform);
        explosion.transform.up = normal;
    }
}