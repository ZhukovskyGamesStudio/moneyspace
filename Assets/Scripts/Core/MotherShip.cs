using System;
using System.Collections;
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

        [SerializeField] private float _lasersDelay = 2;

        [SerializeField] private List<LaserCanon> _lasers = new List<LaserCanon>();
        [SerializeField] private List<LaserCanon> _bigLasers = new List<LaserCanon>();


        private void Start()
        {
            StartCoroutine(ShootCoroutine());
            StartCoroutine(BigShootCoroutine());
        }

        private void ShootLasers()
        {
            foreach (var VARIABLE in _lasers)
            {
                VARIABLE.Shoot(_targetOfMatherShip.transform.position, null);
            }
            
        }
        
        private void ShootBigLasers()
        {
            foreach (var VARIABLE in _bigLasers)
            {
                VARIABLE.Shoot(_targetOfMatherShip.transform.position, null);
            }
            
        }

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                ShootLasers();
                yield return new WaitForSeconds(_lasersDelay);
            }
        }
        
        private IEnumerator BigShootCoroutine()
        {
            while (true)
            {
                ShootBigLasers();
                yield return new WaitForSeconds(_lasersDelay);
            }
        }

    }

    
}