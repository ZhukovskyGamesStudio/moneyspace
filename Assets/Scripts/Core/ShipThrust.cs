using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour {
    [SerializeField]
    private float _baseLightIntensity = 6;

    [SerializeField]
    private Light _thrustLight;

    [SerializeField]
    private List<Renderer> _renderers = new List<Renderer>();
    [SerializeField]
    private List<Renderer> _unchangableRenderers = new List<Renderer>();

    private List<Material> _materials = new List<Material>();

    private bool _isDisabledBecauseBot;

    private void Start() {
        foreach (Renderer ren in _renderers) {
            _materials.Add(ren.material);
        }
    }

    public void DisableRenderersForBot() {
        _isDisabledBecauseBot = true;

        foreach (Renderer ren in _renderers) {
            ren.gameObject.SetActive(false);
        }
        foreach (Renderer ren in _unchangableRenderers) {
            ren.gameObject.SetActive(false);
        }
    }

    public void SetThrustLight(float shipSpeedPercent) {
        if (_isDisabledBecauseBot) {
            return;
        }

        if (shipSpeedPercent <= 0) {
            _thrustLight.intensity = 0;
        } else {
            _thrustLight.intensity = _baseLightIntensity * shipSpeedPercent;
        }

        foreach (Material mat in _materials) {
            mat.SetFloat("_Opacity_RGB", shipSpeedPercent);
        }
    }
}