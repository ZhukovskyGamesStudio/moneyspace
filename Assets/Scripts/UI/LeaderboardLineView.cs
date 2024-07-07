using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardLineView : MonoBehaviour {
    [SerializeField]
    private Image _avatarIcon;

    [SerializeField]
    private TextMeshProUGUI _nameText, _killCount, deathCount, assistCount;

    [SerializeField]
    private GameObject _playerIndicator;

    public void SetData(PlayerData data) {
        gameObject.SetActive(true);
        _nameText.text = data.Nickname;
        _killCount.text = data.Kills.ToString();
        deathCount.text = data.Deaths.ToString();
        assistCount.text = data.Assists.ToString();
        _avatarIcon.sprite = AvatarFactory.GetAvatar(data.AvatarIndex);
       
        _playerIndicator.SetActive(!data.isBot);
    }

    public void SetInactive() {
        gameObject.SetActive(false);
    }
}