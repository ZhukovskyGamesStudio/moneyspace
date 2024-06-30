using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpView : MonoBehaviour {
    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    private Slider _shieldSlider;

    [SerializeField]
    private TextMeshProUGUI _hpText, _shieldText;

    public void SetData(float hpPercent, float shieldPercent) {
        _hpSlider.value = hpPercent;
        _shieldSlider.value = shieldPercent;
    }

    public void SetMaxTexts(int hp, int shield) {
        if (_hpText) {
            _hpText.text = hp.ToString();
        }

        if (_shieldText) {
            _shieldText.text = shield.ToString();
        }
    }
}