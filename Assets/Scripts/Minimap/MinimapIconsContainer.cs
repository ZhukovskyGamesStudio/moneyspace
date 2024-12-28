using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconsContainer : MonoBehaviour {
    public static MinimapIconsContainer Instance;

    [SerializeField]
    private ShipMinimapIcon _iconPrefab;

    [SerializeField]
    private float _positionDivider = 200;

    private Dictionary<AbstractPilot, ShipMinimapIcon> _iconsDict = new Dictionary<AbstractPilot, ShipMinimapIcon>();

    private ShipMinimapIcon _playerTargetIcon;

    private void Awake() {
        Instance = this;
    }

    public void AddIcon(AbstractPilot pilot) {
        ShipMinimapIcon icon = Instantiate(_iconPrefab, transform);
        _iconsDict.Add(pilot, icon);
        if (!pilot.PlayerData.isBot) {
            icon.SetColor(MinimapShipColorType.Player);
        } else {
            icon.SetColor(pilot.PlayerData.Team == Team.Blue ? MinimapShipColorType.Blue : MinimapShipColorType.Red);
        }
    }

    public void SetPlayerTarget(AbstractPilot pilot) {
        if (_playerTargetIcon != null) {
            _playerTargetIcon.SetColor(MinimapShipColorType.Red);
        }

        _playerTargetIcon = _iconsDict[pilot];
        _playerTargetIcon.SetColor(MinimapShipColorType.TargetedEnemy);
    }

    private void Update() {
        foreach (AbstractPilot pilot in _iconsDict.Keys) {
            if (pilot.Ship.gameObject.activeInHierarchy) {
                _iconsDict[pilot].gameObject.SetActive(true);
                _iconsDict[pilot].Move(pilot.Ship.transform.position / _positionDivider, -pilot.Ship.transform.eulerAngles.y);
            } else {
                _iconsDict[pilot].gameObject.SetActive(false);
            }
        }
    }
}