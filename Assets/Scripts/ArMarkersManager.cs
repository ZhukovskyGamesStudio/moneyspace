using UnityEngine;

public class ArMarkersManager : MonoBehaviour {
    [SerializeField]
    private EnemyMarker _enemyMarker, _teammateMarker;

    [SerializeField]
    private Transform _markersHolder;

    public EnemyMarker CreateRedMarker() {
        return Instantiate(_enemyMarker, _markersHolder);
    }
    
    public EnemyMarker CreateBlueMarker() {
        return Instantiate(_teammateMarker, _markersHolder);
    }
}