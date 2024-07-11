using System.Collections.Generic;
using UnityEngine;

public class FactoriesManager : MonoBehaviour {
    private static bool _isInited = false;
    [SerializeField]
    private List<BaseFactory> _factories = new List<BaseFactory>();

    private void Awake() {
        if (!_isInited) {
            DontDestroyOnLoad(gameObject);
            InitFactories();
            _isInited = true;
        } else {
            Destroy(gameObject);
        }
    }

    private void InitFactories() {
        foreach (BaseFactory factory in _factories) {
            factory.InitInstance();
        }
    }
}