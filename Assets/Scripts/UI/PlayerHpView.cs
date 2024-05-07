using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHpView : MonoBehaviour {
    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    private Slider _shieldSlider;

    private void Awake() {
        SetData(Random.Range(0,1f),Random.Range(0,1f));
    }

    public void SetData(float hpPercent, float shieldPercent) {
        _hpSlider.value = hpPercent;
        _shieldSlider.value = shieldPercent;
    }
}