using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectDialog : MonoBehaviour {
    [SerializeField]
    private Image _selectedIcon;

    [SerializeField]
    private TMP_InputField _nicknameInput;

    [SerializeField]
    private List<Toggle> _selectIconToggles;

    private void Awake() {
        for (int index = 0; index < _selectIconToggles.Count; index++) {
            Toggle iconToggle = _selectIconToggles[index];

            iconToggle.GetComponent<Image>().sprite = AvatarFactory.GetAvatar(index);
            int noZamikanie = index;
            iconToggle.onValueChanged.AddListener(isOn => { OnToggleIcon(isOn, noZamikanie); });
        }
    }

    public void Toggle() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        UpdateView();
    }

    private void UpdateView() {
        _selectedIcon.sprite = AvatarFactory.GetAvatar(SaveLoadManager.Profile.SelectedPlayerIcon);
        _nicknameInput.SetTextWithoutNotify(SaveLoadManager.Profile.Nickname);
    }

    public void OnEndNameInput(string newValue) {
        string removedSpaces = newValue.TrimStart(' ').TrimEnd(' ');
        if (removedSpaces.Length > 5) {
            SaveLoadManager.Profile.Nickname = removedSpaces;
            SaveLoadManager.Save();
        } else {
            _nicknameInput.SetTextWithoutNotify(SaveLoadManager.Profile.Nickname);
        }

        UpdateView();
    }

    public void OnToggleIcon(bool isOn, int iconId) {
        if (isOn) {
            SaveLoadManager.Profile.SelectedPlayerIcon = iconId;
            SaveLoadManager.Save();
            UpdateView();
        }
    }
}