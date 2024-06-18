using System;
using YG;
using Random = UnityEngine.Random;

public class YgHandler {
    private Action _onShown;
    private int _id;

    public void ShowRewarded(Action onShown) {
        _onShown = onShown;
        _id = Random.Range(0, 10000000);
        YandexGame.RewVideoShow(_id);
        YandexGame.RewardVideoEvent += CheckRewardedAndProceed;
    }

    private void CheckRewardedAndProceed(int id) {
        if (id == _id) {
            _onShown?.Invoke();
        }
    }
}