using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Renderer _shield;

    public void ShowShield()
    {
        _shield.material.color
    }
}
