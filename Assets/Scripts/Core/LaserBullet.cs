using Unity.Mathematics;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private float _speed;
    [SerializeField]
    private Explosion _explosion;

    private AbstractPilot _owner;

    public void Init(Vector3 dir, float speed, int layer,float lifeTime, AbstractPilot owner) {
        transform.forward = dir;
        _speed = speed;
        gameObject.layer = layer;
        transform.GetChild(0).gameObject.layer = layer;
        Destroy(gameObject, lifeTime);
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
                int damage = GetDamage();
                ship.TakeDamage(damage, _owner);
            }
        }

        Explode(collision);
        Destroy(gameObject);
    }

    private int GetDamage() {
        if (_owner != null) {
            if (_owner.Ship != null) {
                return _owner.Ship.GetLaserDamage();
            }
        }

        return ShipsFactory.ShipStatsGeneralConfig.LaserDamagePerPoint;
    }

    private void Explode(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        Explosion explosion = Instantiate(_explosion, collision.GetContact(0).point, quaternion.identity);
        explosion.transform.up = normal;
    }
}