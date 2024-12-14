using UnityEngine;

public class KillsTray : MonoBehaviour {
    [SerializeField]
    private float _trayHideTime;

    public void AddToTray(AbstractPilot killer, AbstractPilot victim) {
        KillTrayLine line = KillTrayLinePool.Instance.Get();
        line.transform.SetParent(transform);
        line.SetData(killer, victim, _trayHideTime);
        line.gameObject.SetActive(true);
    }
}