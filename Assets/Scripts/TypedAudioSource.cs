using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TypedAudioSource : MonoBehaviour {
    [SerializeField]
    private AudioType _type;

    private AudioSource _source;
    private float _startVolume;

    private void Awake() {
        _source = GetComponent<AudioSource>();
        _startVolume = _source.volume;
    }

    private void Start() {
        UpdateVolume();
    }

    public void UpdateVolume() {
        float multiplier = SaveLoadManager.Profile.MasterVolume * (_type == AudioType.Effect
            ? SaveLoadManager.Profile.EffectVolume
            : SaveLoadManager.Profile.MusicVolume);
        _source.volume = _startVolume * multiplier;
    }

    public void PlayOneShot(AudioClip clip) {
        UpdateVolume();
        _source.PlayOneShot(clip);
    }

    public void Play() {
        _source.Play();
    }
}

[Serializable]
public enum AudioType {
    Music,
    Effect
}