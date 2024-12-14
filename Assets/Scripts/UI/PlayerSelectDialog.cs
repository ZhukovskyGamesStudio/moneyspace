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
            bool isBought = MoneyspaceSaveLoadManager.Profile.BoughtIcons.Contains(i);
            _selectIconToggles[i].SetData(i, isBought);
            int noZamikanie = i;
            view.Toggle.onValueChanged.AddListener(isOn => { OnToggleIcon(isOn, noZamikanie); });
            view.Toggle.group = _toggleGroup;
        }

        _selectedIconIndex = MoneyspaceSaveLoadManager.Profile.SelectedPlayerIcon;
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
        _selectedIcon.sprite = AvatarFactory.GetAvatar(MoneyspaceSaveLoadManager.Profile.SelectedPlayerIcon);
        _nicknameInput.SetTextWithoutNotify(MoneyspaceSaveLoadManager.Profile.Nickname);
    }

    public void OnEndNameInput(string newValue) {
        string removedSpaces = newValue.TrimStart(' ').TrimEnd(' ');
        if (removedSpaces.Length > 5) {
            MoneyspaceSaveLoadManager.Profile.Nickname = removedSpaces;
            MoneyspaceSaveLoadManager.Save();
        } else {
            _nicknameInput.SetTextWithoutNotify(MoneyspaceSaveLoadManager.Profile.Nickname);
            return;
        }

        UpdateView();
        MainMenuUI.Instance.SetData(MoneyspaceSaveLoadManager.Profile);
    }

    public void OnToggleIcon(bool isOn, int iconId) {
        if (isOn) {
            _selectedIconIndex = iconId;
            UpdateApproveButton();
        }
    }

    private void UpdateApproveButton() {
        if (!MoneyspaceSaveLoadManager.Profile.BoughtIcons.Contains(_selectedIconIndex)) {
            _approveButtonText.text = "Купить";
        } else {
            _approveButtonText.text = "Подтвердить";
        }
    }

    public void ApproveButton() {
        if (!MoneyspaceSaveLoadManager.Profile.BoughtIcons.Contains(_selectedIconIndex)) {
            int cost = MainConfigTable.Instance.MainGameConfig.IconCost[_selectedIconIndex];
            if (MoneyspaceSaveLoadManager.Profile.CoinsAmount >= cost) {
                MoneyspaceSaveLoadManager.Profile.CoinsAmount -= cost;
                MoneyspaceSaveLoadManager.Profile.BoughtIcons.Add(_selectedIconIndex);
                MoneyspaceSaveLoadManager.Save();
                
                YGWrapper.SendYandexMetrica("buyAvatar",_selectedIconIndex.ToString());
                
                MainMenuUI.Instance.CoinsView.ShowBoughtAnimation();
                _selectIconToggles[_selectedIconIndex].SetIsBought(true);
                UpdateApproveButton();
            } else {
                MainMenuUI.Instance.CoinsView.ShowNotEnoughAnimation();
                return;
            }
        }

        MoneyspaceSaveLoadManager.Profile.SelectedPlayerIcon = _selectedIconIndex;
        MoneyspaceSaveLoadManager.Save();
        UpdateView();
        MainMenuUI.Instance.SetData(MoneyspaceSaveLoadManager.Profile);
    }
}