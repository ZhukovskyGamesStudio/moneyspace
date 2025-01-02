using Unity.Mathematics;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private AbstractPilot _owner;
    private Transform _transform;
    private Vector3 _calculatedForward;

    [SerializeField]
    private Rigidbody _rb;

    public void Init(Vector3 position, Vector3 dir, float speed, int layer, float lifeTime, AbstractPilot owner) {
        gameObject.SetActive(true);
        _transform = transform;
        _transform.position = position;
        _transform.forward = dir;
        _calculatedForward = _transform.forward * speed;
        gameObject.layer = layer;
        transform.GetChild(0).gameObject.layer = layer;

        Invoke(nameof(Release), lifeTime);
        _owner = owner;
    }

    private void FixedUpdate() {
        _transform.position += _calculatedForward * Time.fixedDeltaTime;
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

        Release();
        Explode(collision);
    }

    private void Release() {
        CancelInvoke();
        StopAllCoroutines();
        LasersPool.Instance.Release(this);
    }

    private int GetDamage() {
        if (_owner != null) {
            if (_owner.Ship != null) {
                return _owner.Ship.GetLaserDamage();
            }
        }

        return ShipsFactory.ShipStatsGeneralConfig.LaserBaseDamage;
    }

    private void Explode(Collision collision) {
        ExplosionsPool.Instance.Get().Init(collision);
    }
}