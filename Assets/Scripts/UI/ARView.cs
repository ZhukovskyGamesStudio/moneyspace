using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ARView : MonoBehaviour {
    [SerializeField]
    private Slider _speedSlider;

    [SerializeField]
    private Slider _overheatSlider;

    [SerializeField]
    private Image _speedFill,_overheatFill;

    [SerializeField]
    private Color _normalOverheat, _overheatedOverheat;
    
    [SerializeField]
    private Color _normalSpeed, _boostedSpeed;

    [SerializeField]
    private ArShootAssist _arShootAssist;

    [SerializeField]
    private GameObject _warpOnSpeed; 

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
    
    public void SetBoostState(bool isBoosted, float boostPercent) {
        _speedFill.color = isBoosted ? _boostedSpeed : _normalSpeed;
        _warpOnSpeed.gameObject.SetActive(isBoosted);
        if (isBoosted) {
            _speedSlider.value = boostPercent;  
        }
    }
}