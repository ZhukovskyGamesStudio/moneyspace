using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShipConfig", fileName = "ShipConfig", order = 0)]
public class ShipConfig : ScriptableObject {

    public IShip Prefab;
    public ShipType ShipType;

    public int ShipMaxHp;

    public int ShipSpeedBase, ShipSpeedMax;
    public int ShipShieldBase, ShipShieldMax;
    public int ShipAttackBase, ShipAttackMax;
}