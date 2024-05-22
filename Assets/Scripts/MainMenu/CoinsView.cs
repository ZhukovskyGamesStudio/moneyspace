using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _text;

    public void SetData(int amount) {
        string txt = amount.ToString();
        if (amount >= 10000) {
            txt = txt.Insert(txt.Length - 3, ".");
        }
        
        if (amount >= 1000000) {
            txt = txt.Insert(txt.Length - 7, ".");
        }

        _text.text = txt;

    }
}