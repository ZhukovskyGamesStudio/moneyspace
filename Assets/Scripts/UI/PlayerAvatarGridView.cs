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

    public void SetData(int iconIndex, bool isBought) {
        SetIsBought(isBought);
        _avatar.sprite = AvatarFactory.GetAvatar(iconIndex);
    }

    public void SetIsBought(bool isBought) {
        int cost = MainConfigTable.Instance.MainGameConfig.IconCost;
        _grayPanel.SetActive(!isBought);
        _text.text = isBought ? "Куплен" : CoinsView.GetDottedView(cost);
        _coinIcon.gameObject.SetActive(!isBought);
    }
}