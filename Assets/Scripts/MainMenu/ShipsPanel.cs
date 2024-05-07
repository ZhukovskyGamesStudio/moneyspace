using UnityEngine;

public class ShipsPanel : MonoBehaviour {
    public void ChangeShip(bool isRight) {
        Debug.Log("ChangeShip to " + (isRight ? "right" : "left"));
    }
}