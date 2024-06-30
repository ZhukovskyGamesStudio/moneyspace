using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ARView : MonoBehaviour {
    [SerializeField]
    private Slider _speedSlider;

    [SerializeField]
    private Slider _overheatSlider;

    [SerializeField]
    private Image _overheatFill;

    [SerializeField]
    private Color _normalOverheat, _overheatedOverheat;

    [SerializeField]
    private ArShootAssist _arShootAssist;

    public ArShootAssist ArShootAssist => _arShootAssist;

    private void Awake() {
        SetData(Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public void SetActive(bool isActive) {
        gameObject.SetActive(isActive);
    }

    public void SetData(float speedPercent, float overheatPercent) {
        _speedSlider.value = speedPercent;
        _overheatSlider.value = overheatPercent;
    }

    public void SetOverheatColor(bool isOverheated) {
        _overheatFill.color = isOverheated ? _overheatedOverheat : _normalOverheat;
    }
}