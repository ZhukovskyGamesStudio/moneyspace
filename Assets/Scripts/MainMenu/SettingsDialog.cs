using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsDialog : MonoBehaviour {
    [SerializeField]
    private Slider _masterSlider, _effectsSlider, _musicSlider;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    public static Action OnVolumesChange;

    public void Toggle() {
        if (_animationHandler.IsOn) {
            Close();
        } else {
            Open();
        }
    }

    private void Open() {
        _animationHandler.ChangeWithAnimation(true);
        _masterSlider.SetValueWithoutNotify(SaveLoadManager.Profile.MasterVolume);
        _effectsSlider.SetValueWithoutNotify(SaveLoadManager.Profile.EffectVolume);
        _musicSlider.SetValueWithoutNotify(SaveLoadManager.Profile.MusicVolume);
    }

    public void Close() {
        _animationHandler.ChangeWithAnimation(false);
    }

    public void OnMasterVolumeChange(float percent) {
        SaveLoadManager.Profile.MasterVolume = percent;
        SaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }

    public void OnSoundChange(float percent) {
        SaveLoadManager.Profile.EffectVolume = percent;
        SaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }

    public void OnMusicChange(float percent) {
        SaveLoadManager.Profile.MusicVolume = percent;
        SaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }
}