using UnityEngine;

public class Explosion : MonoBehaviour {
    private void Awake() {
        Destroy(gameObject, 1);
    }
}