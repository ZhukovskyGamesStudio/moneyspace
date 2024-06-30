using UnityEngine;

public class ShowHideAnimationHandler : MonoBehaviour {
    [SerializeField]
    private AnimationClip _showClip, _hideClip;

    [SerializeField]
    private Animation _animation;

    private bool _isOn;
    public bool IsOn => _isOn;
    public void ChangeWithAnimation(bool isOn) {
        if (_isOn == isOn) {
            return;
        }

        _isOn = isOn;
        _animation.Play(isOn ? _showClip.name : _hideClip.name);
    }
}