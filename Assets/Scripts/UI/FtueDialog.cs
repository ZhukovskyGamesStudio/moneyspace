using System;
using UnityEngine;

public class FtueDialog : MonoBehaviour {
    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    private Action _onClosed;

    public void Show(Action onClosed) {
        _onClosed = onClosed;
        _animationHandler.ChangeWithAnimation(true);
    }

    public void Hide() {
        _animationHandler.ChangeWithAnimation(false);
        _onClosed?.Invoke();
    }
}