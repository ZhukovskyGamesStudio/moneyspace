using System;
using UnityEngine;
using UnityEngine.Events;

public class VisibleChecker : MonoBehaviour {
    public Action OnVisibleAction, OnInvisibleAction;
    
    private void OnBecameVisible() {
        OnVisibleAction?.Invoke();
    }

    private void OnBecameInvisible() {
        OnInvisibleAction?.Invoke();
    }
}