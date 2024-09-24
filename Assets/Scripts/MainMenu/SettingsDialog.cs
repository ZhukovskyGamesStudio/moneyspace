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
        _masterSlider.SetValueWithoutNotify(MoneyspaceSaveLoadManager.Profile.MasterVolume);
        _effectsSlider.SetValueWithoutNotify(MoneyspaceSaveLoadManager.Profile.EffectVolume);
        _musicSlider.SetValueWithoutNotify(MoneyspaceSaveLoadManager.Profile.MusicVolume);
    }

    public void Close() {
        _animationHandler.ChangeWithAnimation(false);
    }

    public void OnMasterVolumeChange(float percent) {
        MoneyspaceSaveLoadManager.Profile.MasterVolume = percent;
        MoneyspaceSaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }

    public void OnSoundChange(float percent) {
        MoneyspaceSaveLoadManager.Profile.EffectVolume = percent;
        MoneyspaceSaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }

    public void OnMusicChange(float percent) {
        MoneyspaceSaveLoadManager.Profile.MusicVolume = percent;
        MoneyspaceSaveLoadManager.Save();
        OnVolumesChange?.Invoke();
    }
}