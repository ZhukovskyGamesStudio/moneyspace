using UnityEngine;

public class PlayerData {
    public bool isBot = false;
    public int AvatarIndex;
    public string Nickname = "defaultNickname";
    public int Kills, Deaths, Assists;
    public Team Team;

    public static PlayerData RandomBot() =>
        new PlayerData {
            isBot = true,
            Nickname = "Player#" + Random.Range(0, 999),
            AvatarIndex = Random.Range(0,AvatarFactory.AvatarsCount)
        };
}