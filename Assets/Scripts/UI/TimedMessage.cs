using System.Collections;
using TMPro;
using UnityEngine;

public class TimedMessage : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    [SerializeField]
    private AudioClip _deathSound, _killSound;

    [SerializeField]
    private TypedAudioSource _audioSource;
    
    private Coroutine _showingCoroutine;

    public void ShowText(string text, float delay, Color textColor) {
        if (_showingCoroutine != null) {
            StopCoroutine(_showingCoroutine);
        }
        gameObject.SetActive(true);
        _text.text = text;
        _text.color = (textColor + Color.white)/2;
        _showingCoroutine = StartCoroutine(ShowingCoroutine(delay));
    }

    private IEnumerator ShowingCoroutine(float delay) {
        _animationHandler.ChangeWithAnimation(true);
        yield return new WaitWhile(() => _animationHandler.IsPlaying);
        yield return new WaitForSeconds(delay);
        _animationHandler.ChangeWithAnimation(false);
        yield return new WaitWhile(() => _animationHandler.IsPlaying);
    }

    public void ForceHide() {
        if (_showingCoroutine != null) {
            StopCoroutine(_showingCoroutine);
        }
        gameObject.SetActive(false);
    }

    public void PlaySound(bool isKill) {
        _audioSource.PlayOneShot(isKill ? _killSound : _deathSound);
    }
}