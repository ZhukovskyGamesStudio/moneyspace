using UnityEngine;

public class ArMarkersManager : MonoBehaviour {
    [SerializeField]
    private EnemyMarker _enemyMarker;

    [SerializeField]
    private Transform _markersHolder;

    public EnemyMarker CreateMarker() {
        return Instantiate(_enemyMarker, _markersHolder);
    }
}