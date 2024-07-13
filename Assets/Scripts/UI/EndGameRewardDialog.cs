using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameRewardDialog : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _killsCountText, _killsSumReward;

    [SerializeField]
    private TextMeshProUGUI _assistCountText, _assistSumReward;

    [SerializeField]
    private TextMeshProUGUI _winHeaderText, _winRewardText, _sumAllText;

    [SerializeField]
    private Animation _devSupportBonus;

    [SerializeField]
    private Button _doubleRewardsButton;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    [Header("Звуки")]
    [SerializeField]
    private TypedAudioSource _typedAudioSource;

    [SerializeField]
    private AudioClip _winAudio, _loseAudio;

    private int _coinsRewardCount;

    public void Show(int killsCount, int supportCount, bool isWin) {
        Cursor.lockState = CursorLockMode.Confined;
        _typedAudioSource.PlayOneShot(isWin ? _winAudio : _loseAudio);
        _winHeaderText.text = isWin ? "ПОБЕДА" : "ПОРАЖЕНИЕ";
        _animationHandler.ChangeWithAnimation(true);
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        _killsCountText.text = killsCount + " x " + cnfg.RewardForKill;
        int sumForKills = killsCount * cnfg.RewardForKill;
        _killsSumReward.text = sumForKills.ToString();

        _assistCountText.text = supportCount + " x " + cnfg.RewardForAssist;
        int sumForAssist = supportCount * cnfg.RewardForAssist;
        _assistSumReward.text = sumForAssist.ToString();

        int sumForWin = isWin ? cnfg.RewardForWin : 0;
        _winRewardText.text = sumForWin.ToString();

        _coinsRewardCount = sumForKills + sumForAssist + sumForWin;
        _sumAllText.text = _coinsRewardCount.ToString();
    }

    public void Collect() {
        SaveLoadManager.Profile.CoinsAmount += _coinsRewardCount;
        SaveLoadManager.Save();
        _animationHandler.ChangeWithAnimation(false);
        GameUI.Instance.LeaderboardDialog.SetEndGameState();
    }

    public void DoubleRewards() {
#if UNITY_EDITOR
        DoubleCoinsAfterRewAd();
#else
        YgHandler handler = new YgHandler();
        handler.ShowRewarded(DoubleCoinsAfterRewAd);
#endif
    }

    private void DoubleCoinsAfterRewAd() {
        _coinsRewardCount *= MainConfigTable.Instance.MainGameConfig.MultiplierForWatchAdInGame;
        _devSupportBonus.Play("RewardBonusActivatedIdle");
        _doubleRewardsButton.interactable = false;
        _sumAllText.text = _coinsRewardCount.ToString();
    }
}