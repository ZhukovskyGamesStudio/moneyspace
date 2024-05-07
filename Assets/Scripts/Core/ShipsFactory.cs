using UnityEngine;

public class ShipsFactory : MonoBehaviour {
    [SerializeField]
    private IShip _ship;

    public static ShipsFactory Instance;

    private void Awake() {
        Instance = this;
    }

    public static IShip GetShip() {
        return Instantiate(Instance._ship);
    }
}