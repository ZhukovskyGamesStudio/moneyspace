using UnityEngine;

public class UIMessages : MonoBehaviour {
    [SerializeField]
    private GameObject _changeTargetMessage;

    public GameObject ChangeTargetMessage => _changeTargetMessage;

    [SerializeField]
    private GameObject _findTargetMessage;

    public GameObject FindTargetMessage => _findTargetMessage;
    
}