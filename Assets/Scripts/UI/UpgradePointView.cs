using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePointView : MonoBehaviour {
    [SerializeField]
    private Image _image;

    [SerializeField]
    private List<Sprite> _sprites;


    public void SetState(int state) {
        _image.sprite = _sprites[state];
    }
}
