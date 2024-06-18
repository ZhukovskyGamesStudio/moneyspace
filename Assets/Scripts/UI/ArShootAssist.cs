using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArShootAssist : MonoBehaviour {
    [SerializeField]
    private GameObject _arShootHelper;

    private Ship _target, _owner;
    private bool _isActive;
    private float _laserSpeed;

    private void Start() {
        _laserSpeed = ShipsFactory.ShipStatsGeneralConfig.LaserSpeed;
    }

    public void Activate(Ship target, Ship ship) {
        _target = target;
        _owner = ship;
        _isActive = true;
        _arShootHelper.gameObject.SetActive(true);
    }

    public void Deactivate() {
        _isActive = false;
        _arShootHelper.gameObject.SetActive(false);
    }

    public void UpdatePos() {
        if (!_isActive) {
            return;
        }

        if (_target && _owner) {
            RecalculateArHelperPos(_target, _owner);
        }
    }

    private void RecalculateArHelperPos(Ship target, Ship owner) {
        if (!target.VisibleChecker.IsVisible) {
            _arShootHelper.SetActive(false);
            return;
        }

        _arShootHelper.SetActive(true);
        Vector3 ACv = target.transform.position - owner.transform.position;
        float AC = Vector3.Magnitude(ACv);
        float angleA = Vector3.Angle(target.transform.forward, ACv);
        double cosA = Math.Cos(angleA);
        float shipSpeed = target.ShipSpeed;

        double a = (_laserSpeed * _laserSpeed - shipSpeed * shipSpeed) / (shipSpeed * shipSpeed * AC);
        double b = 2 * cosA;
        double c = -AC;

        List<double> rootsList = QuadraticSolver.SolveEquation(a, b, c);
        if (rootsList.Count == 0) {
            Debug.Log("Ну и ты математик бля. Дискриминант меньше нуля,так что пошёл нахуй отсюда >:( ");
            return;
        }

        float AB = (float)rootsList.First(r => r > 0);
        Vector3 posToShootAt = target.transform.position + target.transform.forward * AB;
        Vector3 lerpedPos = Vector3.Lerp(_arShootHelper.transform.position, Camera.main.WorldToScreenPoint(posToShootAt), 0.1f);
        _arShootHelper.transform.position = lerpedPos;
    }
}