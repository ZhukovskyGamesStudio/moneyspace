using System;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    [SerializeField]
    private Texture2D _circle, _circleWithDot;

    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        SetCursor(CursorType.Circle);
    }

    public void SetCursor(CursorType type) {
        Texture2D txt = type switch {
            CursorType.Circle => _circle,
            CursorType.CircleWithDot => _circleWithDot,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        Cursor.SetCursor(txt, new Vector2(txt.width, txt.height) / 2, CursorMode.ForceSoftware);
    }
}

[Serializable]
public enum CursorType {
    Circle,
    CircleWithDot
}