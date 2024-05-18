using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipsPanel : MonoBehaviour {
    [SerializeField]
    private MenuShipsView _menuShipsView;

    [SerializeField]
    private Button _leftButton, _rightButton;

    private int _curShipIndex;

    private void Start() {
        UpdateButtonsState(_curShipIndex);
    }

    public void ChangeShip(bool isRight) {
        _curShipIndex += isRight ? 1 : -1;
        UpdateButtonsState(_curShipIndex);
        _menuShipsView.Move(isRight);
    }

    private void UpdateButtonsState(int curIndex) {
        _leftButton.interactable = curIndex != 0;
        _rightButton.interactable = curIndex != ShipsFactory.Ships.Count - 1;
    }
}