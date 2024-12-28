using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipMinimapIcon : MonoBehaviour {
    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private Color _red, _blue, _player, _targetedEnemy;

    public void SetColor(MinimapShipColorType type) {
        _iconImage.color = type switch {
            MinimapShipColorType.Red => _red,
            MinimapShipColorType.Blue => _blue,
            MinimapShipColorType.Player => _player,
            MinimapShipColorType.TargetedEnemy => _targetedEnemy,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public void Move(Vector3 pos, float angle) {
        transform.localPosition = pos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

[SerializeField]
public enum MinimapShipColorType {
    Red,
    Blue,
    Player,
    TargetedEnemy
}