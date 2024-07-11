using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Shield : MonoBehaviour {
    [SerializeField]
    private Renderer _shield;

    private float _fullColorAlpha;
    private Coroutine _showCoroutine;
    private static readonly int Threshold = Shader.PropertyToID("_Cutoff");

    private void Start() {
        SetAlpha(0);
    }

    public void ShowShield() {
        if (_showCoroutine != null) {
            StopCoroutine(_showCoroutine);
        }

        _showCoroutine = StartCoroutine(ShowShieldCoroutine());
    }

    public void HideShieldInstant() {
        SetAlpha(0);
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
        percent = Math.Clamp(percent, 0.01f, 0.99f);
        _shield.material.SetFloat(Threshold, 1 - percent);
    }
}