using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _killsCountText, _killsSumReward;

    [SerializeField]
    private TextMeshProUGUI _winHeaderText, _winRewardText, _sumAllText;

    [SerializeField]
    private Animation _devSupportBonus;

    [SerializeField]
    private Button _doubleRewardsButton;

    [SerializeField]
    private ShowHideAnimationHandler _animationHandler;

    private int _coinsRewardCount;

    public void Show(int killsCount, bool isWin) {
        _winHeaderText.text = isWin ? "ПОБЕДА" : "ПОРАЖЕНИЕ";
        _animationHandler.ChangeWithAnimation(true);
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        _killsCountText.text = killsCount + " x " + cnfg.RewardForKill;
        int sumForKills = killsCount * cnfg.RewardForKill;
        _killsSumReward.text = sumForKills.ToString();

        int sumForWin = isWin ? cnfg.RewardForWin : 0;
        _winRewardText.text = sumForWin.ToString();

        _coinsRewardCount = sumForKills + sumForWin;
        _sumAllText.text = _coinsRewardCount.ToString();
    }

    public void Collect() {
        SaveLoadManager.Profile.CoinsAmount += _coinsRewardCount;
        SaveLoadManager.Save();
        _animationHandler.ChangeWithAnimation(false);
        GameUI.Instance.LeaderboardDialog.SetEndGameState();
    }

    public void DoubleRewards() {
        _coinsRewardCount *= MainConfigTable.Instance.MainGameConfig.MultiplierForWatchAdInGame;
        _devSupportBonus.Play("RewardBonusActivatedIdle");
        _doubleRewardsButton.interactable = false;
        _sumAllText.text = _coinsRewardCount.ToString();
    }
}