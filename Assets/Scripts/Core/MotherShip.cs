using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour {
    [SerializeField]
    private GameObject _targetOfMatherShip;

    [SerializeField]
    private float _lasersDelay = 2;

    [SerializeField]
    private List<LaserCanon> _lasers = new List<LaserCanon>();

    [SerializeField]
    private List<LaserCanon> _bigLasers = new List<LaserCanon>();

    [SerializeField]
    private float _laserLifetime = 5;
    
    private void Start() {
        StartCoroutine(ShootCoroutine(_lasers));
        StartCoroutine(ShootCoroutine(_bigLasers));
    }

    private IEnumerator ShootCoroutine(List<LaserCanon> lasers) {
        while (true) {
            ShootLasers(lasers);
            yield return new WaitForSeconds(_lasersDelay);
        }
    }

    private void ShootLasers(List<LaserCanon> lasers) {
        if (_targetOfMatherShip == null) {
            return;
        }

        foreach (LaserCanon canon in lasers) {
            canon.Shoot(_targetOfMatherShip.transform.position,_laserLifetime, null);
        }
    }
}