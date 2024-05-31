using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ARView : MonoBehaviour {
    [SerializeField]
    private Slider _speedSlider;

    [SerializeField]
    private Slider _overheatSlider;

    [SerializeField]
    private ArShootAssist _arShootAssist;
    
    public ArShootAssist ArShootAssist => _arShootAssist;
    
    private void Awake() {
        SetData(Random.Range(0,1f),Random.Range(0,1f));
    }

    public void SetActive(bool isActive) {
        gameObject.SetActive(isActive);
    }
    
    public void SetData(float speedPercent, float overheatPercent) {
        _speedSlider.value = speedPercent;
        _overheatSlider.value = overheatPercent;
    }
}