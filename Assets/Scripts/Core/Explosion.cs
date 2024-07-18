using UnityEngine;

public class Explosion : MonoBehaviour {
    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    public void Init(Collision collision) {
        ContactPoint contact = collision.GetContact(0);
        _transform.position = contact.point;
        _transform.rotation = Quaternion.identity;
        _transform.up = contact.normal;
        gameObject.SetActive(true);

        Invoke(nameof(Release), 1);
    }

    private void Release() {
        ExplosionsPool.Instance.Release(this);
    }
}