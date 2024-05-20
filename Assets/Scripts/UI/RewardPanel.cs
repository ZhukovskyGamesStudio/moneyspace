using TMPro;
using UnityEngine;

public class RewardPanel : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _killsCountText, _killsSumReward;

    [SerializeField]
    private TextMeshProUGUI _winRewardText, _sumAllText;

    [SerializeField]
    private Animation _killsBonus, _winBonus, _devSupportBonus, _allSum;

    private int _coinsRewardCount;

    public void Show(int killsCount, bool isWin) {
        gameObject.SetActive(true);
        MainGameConfig cnfg = MainConfigTable.Instance.MainGameConfig;
        _killsCountText.text = killsCount + " x " + cnfg.RewardForKill;
        int sumForKills = killsCount * cnfg.RewardForKill;
        _killsSumReward.text = sumForKills.ToString();

        int sumForWin = isWin ? cnfg.RewardForWin : 0;
        _winRewardText.text = sumForWin.ToString();

        _coinsRewardCount = sumForKills + sumForWin;
        _sumAllText.text = _coinsRewardCount.ToString();
        
        _devSupportBonus.gameObject.SetActive(false);
    }

    public void Collect() {
        SaveLoadManager.Profile.CoinsAmount += _coinsRewardCount;
        SaveLoadManager.Save();
        gameObject.SetActive(false);
    }

    public void DoubleRewards() {
        _coinsRewardCount *= MainConfigTable.Instance.MainGameConfig.MultiplierForWatchAdInGame;
        _devSupportBonus.gameObject.SetActive(true);
        _sumAllText.text = _coinsRewardCount.ToString();
    }
}