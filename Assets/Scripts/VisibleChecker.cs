using System;
using UnityEngine;
using UnityEngine.Events;

public class VisibleChecker : MonoBehaviour {
    public Action OnVisibleAction, OnInvisibleAction;
    public bool IsVisible => _isVisible;
    private bool _isVisible;

    //TODO rework via angle check
    private void OnBecameVisible() {
        OnVisibleAction?.Invoke();
        _isVisible = true;
    }

    private void OnBecameInvisible() {
        OnInvisibleAction?.Invoke();
        _isVisible = false;
    }
}