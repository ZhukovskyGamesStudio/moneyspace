using UnityEngine;

public class PlayerData {
    public bool isBot = false;
    public int AvatarIndex;
    public string Nickname = "defaultNickname";
    public int Kills, Deaths, Assists;
    public Team Team;

    public static PlayerData RandomBot(string name) =>
        new PlayerData {
            isBot = true,
            Nickname = name,
            AvatarIndex = Random.Range(0,AvatarFactory.AvatarsCount)
        };
}