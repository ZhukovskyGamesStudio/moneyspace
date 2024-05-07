using System;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;

        public RespawnManager RespawnManager = new RespawnManager();
        private void Awake() {
            Instance = this;
        }
    }
}