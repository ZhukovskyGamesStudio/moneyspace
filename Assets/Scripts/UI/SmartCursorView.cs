using System;
using UnityEngine;

public class SmartCursorView : MonoBehaviour {
    [SerializeField]
    private RectTransform _dotInCenter;

    [SerializeField]
    private LineRenderer _lineRenderer;

    public void UpdatePos() {
        _dotInCenter.position = Input.mousePosition;
        Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height) / 2).GetPoint(10);
        _lineRenderer.SetPosition(0, Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height) / 2).GetPoint(10));
        _lineRenderer.SetPosition(1, Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10));
    }
}