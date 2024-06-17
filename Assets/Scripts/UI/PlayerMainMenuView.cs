using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainMenuView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _nicknameText;

    [SerializeField]
    private Image _avatarIcon;

    public void UpdateData(SaveProfile profile) {
        _nicknameText.text = profile.Nickname;
        _avatarIcon.sprite = AvatarFactory.GetAvatar(profile.SelectedPlayerIcon);
    }
}