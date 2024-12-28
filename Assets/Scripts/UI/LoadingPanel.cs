using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour {
    public static LoadingPanel Instance;

    [SerializeField]
    private GameObject _loadingCanvas;

    [SerializeField]
    private Animation _animation;

    public Action<string> OnSceneLoaded;
    public Action<string> OnLoadingPanelHided;

    private void Awake() {
        if (Instance != null) {
            Destroy(_loadingCanvas);
            return;
        }
        DontDestroyOnLoad(_loadingCanvas);
        Instance = this;
        SceneManager.sceneLoaded += (s, b) => {
            OnSceneLoaded?.Invoke(s.name);
            Hide(() => { OnLoadingPanelHided?.Invoke(s.name); });
        };
    }

    public static void ShowAndLoadScene(string sceneName) {
        Instance.Show(delegate { SceneManager.LoadSceneAsync(sceneName); });
    }

    public void Show(Action callback) {
        StartCoroutine(WaitAnimCallback("LoadingPanelShow", callback));
    }

    public void Hide(Action callback) {
        StartCoroutine(WaitAnimCallback("LoadingPanelHide", callback));
    }

    private IEnumerator WaitAnimCallback(string clipName, Action callback) {
        _animation.Play(clipName);
        yield return new WaitWhile(() => _animation.isPlaying);
        callback?.Invoke();
    }
}