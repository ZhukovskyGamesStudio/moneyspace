using System;
using Unity.Mathematics;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    private float _speed;

    [SerializeField]
    private Explosion _explosion;
    
    public void Init(Vector3 dir, float speed) {
        transform.forward = dir;
        _speed = speed;

        Destroy(gameObject, 3);
    }

    private void FixedUpdate() {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision) {


        Explode(collision);
        Destroy(gameObject);
    }

    private void Explode(Collision collision) {
        Vector3 normal = collision.GetContact(0).normal;
        Explosion explosion = Instantiate(_explosion, transform.position, quaternion.identity,collision.GetContact(0).otherCollider.transform );
        explosion.transform.up = normal;
    }
}