using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArShootAssist : MonoBehaviour {
   
    [Header("Shoot Assist")]
    [SerializeField]
    private GameObject _arShootHelper;

    [SerializeField]
    private GameObject _round, _roundInside, _arrow;

    [Header("Target Lock")]
    [SerializeField]
    private GameObject _targetLocked;

    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    private Image _sliderFill;

    [SerializeField]
    private TextMeshProUGUI _targetNickname;

    [SerializeField]
    private Color _hpColor, _shieldColor;

    private Ship _target, _owner;
    private bool _isActive;
    private float _laserSpeed;
    private static float SHOOT_HELPER_ARROW_RADIUS = 400;

    [SerializeField]
    private float _minLockDistance, _maxLockDistance;
    [SerializeField]
    private float _minLockSize, _maxLockSize;

    private void Start() {
        _laserSpeed = ShipsFactory.ShipStatsGeneralConfig.LaserSpeed;
        Deactivate();
    }

    public void Activate(Ship target, Ship ship) {
        _target = target;
        _owner = ship;
        _isActive = true;
        _arShootHelper.gameObject.SetActive(true);
        _targetLocked.gameObject.SetActive(false);
    }

    public void Deactivate() {
        _isActive = false;
        _arShootHelper.gameObject.SetActive(false);
        _targetLocked.gameObject.SetActive(false);
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
        Transform targetAnchor = target.GetTargetLockAnchor;
        bool isVisible = CheckTargetVisible(target, 40);
        if (!isVisible) {
            Vector3 finPos = Camera.main.WorldToScreenPoint(targetAnchor.position);
            _arrow.SetActive(true);
            _round.SetActive(false);
            
            finPos.z = 0;
            Vector3 dirFromCenter = finPos - transform.position;
            dirFromCenter.z = 0;
            finPos = transform.position + dirFromCenter.normalized * SHOOT_HELPER_ARROW_RADIUS;
           
            LerpShootHelper(finPos);
            _arrow.transform.localRotation = Quaternion.FromToRotation(Vector3.up, dirFromCenter);
            _targetLocked.gameObject.SetActive(false);
            return;
        }
        _targetLocked.gameObject.SetActive(true);
        Vector3 lockPos = Camera.main.WorldToScreenPoint(targetAnchor.position);
        lockPos.z = 0;
        _targetLocked.transform.position = lockPos;
        
        _arrow.SetActive(false);
        bool isInShootRange = CheckTargetInShootDistance(target);
        _round.SetActive(isInShootRange);
        bool isInCloseRange = CheckTargetInCloseDistance(target);
        _roundInside.SetActive(isInCloseRange);
        
        Vector3 ACv = targetAnchor.position - owner.transform.position;
        float AC = Vector3.Magnitude(ACv);
        float angleA = Vector3.Angle(targetAnchor.forward, ACv);
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
        Vector3 posToShootAt = targetAnchor.position + targetAnchor.forward * AB;

        Vector3 posToShootPos = Camera.main.WorldToScreenPoint(posToShootAt);
        posToShootAt.z = 0;
        LerpShootHelper(posToShootPos);
        UpdateTargetLock();
    }

    public static bool CheckTargetVisible(Ship target, float maxAngle = 90) {
        if (target == null) {
            return false;
        }
        
        if (!target.gameObject.activeSelf) {
            return false;
        }
        Transform cameraTr = Camera.main.transform;
        float angle = Vector3.Angle(cameraTr.forward, target.transform.position - cameraTr.position);
        return angle < maxAngle;
    }

    public bool CheckTargetInShootDistance(Ship target) {
        float dist = (Camera.main.transform.position - target.transform.position).magnitude;
        return dist <= ShipsFactory.ShipStatsGeneralConfig.PrimeLaserLifetime * ShipsFactory.ShipStatsGeneralConfig.LaserSpeed;
    }
    
    public bool CheckTargetInCloseDistance(Ship target) {
        float dist = (Camera.main.transform.position - target.transform.position).magnitude;
        return dist <= ShipsFactory.ShipStatsGeneralConfig.SecondLaserLifetime * ShipsFactory.ShipStatsGeneralConfig.LaserSpeed;
    }

    private void UpdateTargetLock() {
        _targetNickname.text = _target.GetOwner().PlayerData.Nickname;
        _sliderFill.color = Color.Lerp(_hpColor, _shieldColor, Mathf.Max(_target.GetShieldPercent() - 0.1f, 0));
        _hpSlider.value = _target.GetHpPercent();


        Vector3 dist = _target.transform.position - Camera.main.transform.position;
        float sizePercent = 1 -Math.Clamp(dist.magnitude, _minLockDistance, _maxLockDistance) / _maxLockDistance;
        _targetLocked.transform.localScale = Vector3.one * Mathf.Lerp(_minLockSize, _maxLockSize, sizePercent);
    }

    private void LerpShootHelper(Vector3 target) {
        Vector3 lerpedPos = Vector3.Lerp(_arShootHelper.transform.position, target, 0.1f);
        lerpedPos.z = 0;
        _arShootHelper.transform.position = lerpedPos;
    }
}