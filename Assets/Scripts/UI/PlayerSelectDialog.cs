using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectDialog : MonoBehaviour {
    [SerializeField]
    private Image _selectedIcon;

    [SerializeField]
    private TMP_InputField _nicknameInput;

    private List<PlayerAvatarGridView> _selectIconToggles = new List<PlayerAvatarGridView>();

    [SerializeField]
    private PlayerAvatarGridView _togglePrefab;

    [SerializeField]
    private Transform _avatarsHolder;

    [SerializeField]
    private ToggleGroup _toggleGroup;

    [SerializeField]
    private TextMeshProUGUI _approveButtonText;

    [SerializeField]
    private Button _approveButton;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    private int _selectedIconIndex = 0;

    private void Start() {
        CreateIcons();
    }

    private void CreateIcons() {
        int amount = MainConfigTable.Instance.MainGameConfig.IconCost.Count;
        for (int i = 0; i < amount; i++) {
            PlayerAvatarGridView view = Instantiate(_togglePrefab, _avatarsHolder);
            _selectIconToggles.Add(view);
            bool isBought = SaveLoadManager.Profile.BoughtIcons.Contains(i);
            _selectIconToggles[i].SetData(i, isBought);
            int noZamikanie = i;
            view.Toggle.onValueChanged.AddListener(isOn => { OnToggleIcon(isOn, noZamikanie); });
            view.Toggle.group = _toggleGroup;
        }

        _selectedIconIndex = SaveLoadManager.Profile.SelectedPlayerIcon;
        _selectIconToggles[_selectedIconIndex].Toggle.isOn = true;
    }

    public void Toggle() {
        if (_animationHandler.IsOn) {
            _animationHandler.ChangeWithAnimation(false);
            return;
        }

        _animationHandler.ChangeWithAnimation(true);
        UpdateView();
    }

    public void Close() {
        _animationHandler.ChangeWithAnimation(false);
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
            return;
        }

        UpdateView();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
    }

    public void OnToggleIcon(bool isOn, int iconId) {
        if (isOn) {
            _selectedIconIndex = iconId;
            UpdateApproveButton();
        }
    }

    private void UpdateApproveButton() {
        if (!SaveLoadManager.Profile.BoughtIcons.Contains(_selectedIconIndex)) {
            _approveButtonText.text = "Купить";
        } else {
            _approveButtonText.text = "Подтвердить";
        }
    }

    public void ApproveButton() {
        if (!SaveLoadManager.Profile.BoughtIcons.Contains(_selectedIconIndex)) {
            int cost = MainConfigTable.Instance.MainGameConfig.IconCost[_selectedIconIndex];
            if (SaveLoadManager.Profile.CoinsAmount >= cost) {
                SaveLoadManager.Profile.CoinsAmount -= cost;
                SaveLoadManager.Profile.BoughtIcons.Add(_selectedIconIndex);
                SaveLoadManager.Save();
                MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
                _selectIconToggles[_selectedIconIndex].SetIsBought(true);
                UpdateApproveButton();
            } else {
                MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
                return;
            }
        }

        SaveLoadManager.Profile.SelectedPlayerIcon = _selectedIconIndex;
        SaveLoadManager.Save();
        UpdateView();
        MainMenuUI.Instance.SetData(SaveLoadManager.Profile);
    }
}