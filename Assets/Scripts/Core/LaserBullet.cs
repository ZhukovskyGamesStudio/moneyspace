using Unity.Mathematics;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private float _speed;
    [SerializeField]
    private Explosion _explosion;

    private AbstractPilot _owner;

    public void Init(Vector3 dir, float speed, int layer, AbstractPilot owner) {
        transform.forward = dir;
        _speed = speed;
        gameObject.layer = layer;
        transform.GetChild(0).gameObject.layer = layer;
        Destroy(gameObject, 3);
        _owner = owner;
    }

    private void FixedUpdate() {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        Rigidbody rb = collision.GetContact(0).otherCollider.attachedRigidbody;
        if (rb != null) {
            Ship ship = rb.GetComponent<Ship>();
            if (ship != null) {
                ship.TakeDamage(_owner.Ship.GetLaserDamage(), _owner);
            }
        }

        Explode(collision);
        Destroy(gameObject);
    }

    private void Explode(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        Explosion explosion = Instantiate(_explosion, collision.GetContact(0).point, quaternion.identity);
        explosion.transform.up = normal;
    }
}