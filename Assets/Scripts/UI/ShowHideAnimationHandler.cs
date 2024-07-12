using UnityEngine;

public class ShowHideAnimationHandler : MonoBehaviour {
    [SerializeField]
    private AnimationClip _showClip, _hideClip;

    [SerializeField]
    private Animation _animation;

    [SerializeField]
    private bool _ignoreIsOn;
    
    private bool _isOn;
    public bool IsOn => _isOn;
    public void ChangeWithAnimation(bool isOn) {
        if (!_ignoreIsOn) {
            if (_isOn == isOn) {
                return;
            }

            _isOn = isOn;
        }
        _animation.Play(isOn ? _showClip.name : _hideClip.name);
    }

    public bool IsPlaying => _animation.isPlaying;
}