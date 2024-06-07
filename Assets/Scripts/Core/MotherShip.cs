using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MotherShip : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _littleGunsOfMatherShip = new List<GameObject>();

        [SerializeField] private List<GameObject> _bigGunsOfMatherShip = new List<GameObject>();

        [SerializeField] private GameObject _littleBullet;
        
        [SerializeField] private GameObject _BigBullet;

        [SerializeField] private GameObject _targetOfMatherShip;

        [SerializeField] private bool _isRed = false;
        
        
        

    }

    
}