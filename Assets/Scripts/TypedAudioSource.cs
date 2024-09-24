using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TypedAudioSource : MonoBehaviour {
    [SerializeField]
    private AudioType _type;

    [SerializeField]
    private bool _cancelIfPlaying = false; 
    
    private AudioSource _source;
    private float _startVolume;
    private float _volumePercent = 1;

    private void Awake() {
        _source = GetComponent<AudioSource>();
        _startVolume = _source.volume;
    }

    private void Start() {
        UpdateVolume();
    }

    public void SetVolumePercent(float percent) {
        _volumePercent = percent;
    }

    public void UpdateVolume() {
        float multiplier = MoneyspaceSaveLoadManager.Profile.MasterVolume * (_type == AudioType.Effect
            ? MoneyspaceSaveLoadManager.Profile.EffectVolume
            : MoneyspaceSaveLoadManager.Profile.MusicVolume);
        _source.volume = _startVolume * _volumePercent * multiplier;
    }

    public void PlayOneShot(AudioClip clip) {
        if (_cancelIfPlaying && _source.isPlaying) {
            return;
        }
        UpdateVolume();
        _source.PlayOneShot(clip);
    }

    public void Play() {
        if (_cancelIfPlaying && _source.isPlaying) {
            return;
        }
        UpdateVolume();
        _source.Play();
    }
}

[Serializable]
public enum AudioType {
    Music,
    Effect
}