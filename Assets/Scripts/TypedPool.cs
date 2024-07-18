using System.Collections.Generic;
using UnityEngine;

public class TypedPool<T> : MonoBehaviour where T : MonoBehaviour {
    [SerializeField]
    protected T _prefab;

    [SerializeField]
    private int _prewarmAmount = 500;

    private Queue<T> _poolQueue;
    private Transform _transform;

    protected virtual void Awake() {
        _transform = transform;
    }

    private void Start() {
        Prewarm();
    }

    private void Prewarm() {
        _poolQueue = new Queue<T>(_prewarmAmount);
        for (int i = 0; i < _prewarmAmount; i++) {
            Release(AddLaser());
        }
    }

    private T AddLaser() {
        return Instantiate(_prefab, Vector3.zero, Quaternion.identity, _transform);
    }

    public T Get() {
        return _poolQueue.TryDequeue(out T l) ? l : AddLaser();
    }

    public void Release(T bullet) {
        bullet.gameObject.SetActive(false);
        _poolQueue.Enqueue(bullet);
    }
}