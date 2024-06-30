using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatarGridView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private Image _avatar, _coinIcon;

    [SerializeField]
    private GameObject _grayPanel;

    [SerializeField]
    private Toggle _toggle;

    public Toggle Toggle => _toggle;

    private int _iconIndex;

    public void SetData(int iconIndex, bool isBought) {
        _iconIndex = iconIndex;
        SetIsBought(isBought);
        _avatar.sprite = AvatarFactory.GetAvatar(iconIndex);
    }

    public void SetIsBought(bool isBought) {
        int cost = MainConfigTable.Instance.MainGameConfig.IconCost[_iconIndex];
        _grayPanel.SetActive(!isBought);
        _text.text = isBought ? "Куплен" : CoinsView.GetDottedView(cost);
        _coinIcon.gameObject.SetActive(!isBought);
    }
}