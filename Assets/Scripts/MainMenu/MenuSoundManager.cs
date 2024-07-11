using UnityEngine;

public class MenuSoundManager : MonoBehaviour {
    [SerializeField]
    public TypedAudioSource _effectsSource, _musicSource;

    [SerializeField]
    public AudioClip _buttonHover;

    [SerializeField]
    public AudioClip _buttonClick;

    [SerializeField]
    public AudioClip _startButton;

    [SerializeField]
    public AudioClip _nextShipButton;

    private void Start() {
        SettingsDialog.OnVolumesChange += UpdateVolumes;
    }

    private void UpdateVolumes() {
        _effectsSource.UpdateVolume();
        _musicSource.UpdateVolume();
    }

    public void HoverSound() {
        _effectsSource.PlayOneShot(_buttonHover);
    }

    public void ClickSound() {
        _effectsSource.PlayOneShot(_buttonClick);
    }

    public void StartSound() {
        _effectsSource.PlayOneShot(_startButton);
    }

    public void NextShipSound() {
        _effectsSource.PlayOneShot(_nextShipButton);
    }

    private void OnDestroy() {
        SettingsDialog.OnVolumesChange -= UpdateVolumes;
    }
}