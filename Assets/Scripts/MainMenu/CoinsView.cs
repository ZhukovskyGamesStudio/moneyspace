using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private Animation _animation;
    public void SetData(int amount) {
        _text.text = GetDottedView(amount);
    }

    public void ShowNotEnoughAnimation() {
        //todo add error sound
        _animation.Play("NotEnoughCoins");
    }
    
    public void ShowBoughtAnimation() {
        //todo add error sound
        _animation.Play("Bought");
    }

    public static string GetDottedView(int amount) {
        string txt = amount.ToString();
        if (amount >= 10000) {
            txt = txt.Insert(txt.Length - 3, ".");
        }

        if (amount >= 1000000) {
            txt = txt.Insert(txt.Length - 7, ".");
        }

        return txt;
    }
}