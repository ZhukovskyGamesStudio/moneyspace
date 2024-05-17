using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour
{
    [SerializeField]
    private Light _thrustLight;

    public void SetThrustLight(float shipSpeedPercent)
    {
        if (shipSpeedPercent <= 0)
        {
            _thrustLight.intensity = 0;
        }
        else
        {
            _thrustLight.intensity = 6 * shipSpeedPercent;
        }
    }
}
