using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _laserText, _rocketText;

    private int _maxLaser = -1, _maxRocket = -1;

    private void Awake() {
        int MAX_LASER = 26;
        int MAX_ROCKET = 5;
        SetMaxData(MAX_LASER, MAX_ROCKET);

        int laser = Random.Range(0, MAX_LASER + 1);
        int rocket = Random.Range(0, MAX_ROCKET + 1);
        SetData(laser, rocket);
    }

    public void SetMaxData(int maxLaser, int maxRocket) {
        _maxLaser = maxLaser;
        _maxRocket = maxRocket;
    }

    public void SetData(int laser, int rocket) {
        _laserText.text = $"{laser}/{_maxLaser}";
        _rocketText.text = $"{rocket}/{_maxRocket}";
    }
}