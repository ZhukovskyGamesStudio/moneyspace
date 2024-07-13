using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillTrayLine : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _killerNameText, _victimNameText;

    [SerializeField]
    private Color _red, _blue, _player;
    
    [SerializeField]
    private Image _killerShipIcon;

    public void SetData(AbstractPilot killer, AbstractPilot victim) {
        if (killer) {
            _killerNameText.text = killer.PlayerData.Nickname;
            if (killer.PlayerData.isBot) {
                _killerNameText.color = killer.PlayerData.Team == Team.Blue ? _blue : _red;
            } else {
                _killerNameText.color = _player;
            }
           
            _killerShipIcon.sprite = ShipsFactory.Ships.First(s => s.ShipType == killer.ShipType).Icon;
        } else {
            _killerNameText.gameObject.SetActive(false);
            _killerShipIcon.gameObject.SetActive(false);
        }

        _victimNameText.text = victim.PlayerData.Nickname;
        if (victim.PlayerData.isBot) {
            _victimNameText.color = victim.PlayerData.Team == Team.Blue ? _blue : _red;
        } else {
            _victimNameText.color = _player;
        }
    }
}