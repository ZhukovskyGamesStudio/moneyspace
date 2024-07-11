using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {
    void Start() {
        SceneManager.LoadSceneAsync("MenuScene");
    }
}