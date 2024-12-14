public class MoneyspaceSaveLoadManager : SaveLoadManager<MoneyspaceSaveLoadManager, MoneyspaceSaveProfile> {
    public void EarnCoins(int amount, string from) {
        Profile.CoinsAmount += amount;

        YGWrapper.SendYandexMetrica("earnCoins", from);

        Save();
    }
}