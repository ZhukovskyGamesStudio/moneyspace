using UnityEngine;

public class KillsTray : MonoBehaviour {
    [SerializeField]
    private KillTrayLine _killTrayLinePrefab;

    [SerializeField]
    private float _trayHideTime;

    public void AddToTray(AbstractPilot killer, AbstractPilot victim) {
        KillTrayLine line = Instantiate(_killTrayLinePrefab, transform);
        line.SetData(killer, victim);
        Destroy(line.gameObject, _trayHideTime);
    }
}