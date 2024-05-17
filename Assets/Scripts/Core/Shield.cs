using System;
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField]
    private Renderer _shield;

    private float _fullColorAlpha;
    private Coroutine _showCoroutine;

    private void Awake() {
        _fullColorAlpha = _shield.material.color.a;
        SetAlpha(0);
    }

    public void ShowShield() {
        if (_showCoroutine != null) {
            StopCoroutine(_showCoroutine);
        }

        _showCoroutine = StartCoroutine(ShowShieldCoroutine());
    }

    private IEnumerator ShowShieldCoroutine() {
        SetAlpha(1);
        float curTime = 0;
        float decreaseTime = 1;
        while (curTime < decreaseTime) {
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            SetAlpha(1 - curTime / decreaseTime);
        }
        SetAlpha(0);
    }

    private void SetAlpha(float percent) {
        percent = Math.Clamp(percent, 0, 1);
        Color tmp = _shield.material.color;
        tmp.a = _fullColorAlpha * percent;
        _shield.material.color = tmp;
    }
}