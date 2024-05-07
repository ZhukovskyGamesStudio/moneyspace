using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeathmatchProgressView : MonoBehaviour {
    [SerializeField]
    private Slider _blueSlider;

    [SerializeField]
    private Slider _redSlider;

    [SerializeField]
    private TextMeshProUGUI _blueText, _redText;

    private void Awake() {
        int redCount = Random.Range(0, 101);
        int blueCount = Random.Range(0, 101);
        SetData(blueCount / 100f, blueCount, redCount / 100f, redCount);
    }

    public void SetData(float bluePercent, int blueCount, float redPercent, int redCount) {
        _blueSlider.value = redPercent;
        _redSlider.value = bluePercent;
        _blueText.text = blueCount.ToString();
        _redText.text = redCount.ToString();
    }
}