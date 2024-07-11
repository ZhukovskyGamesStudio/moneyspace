using System.Collections.Generic;
using UnityEngine;

public class AvatarFactory : BaseFactory {
    [SerializeField]
    private List<Sprite> _avatars = new List<Sprite>();

    public static AvatarFactory Instance;

    public static int AvatarsCount => Instance._avatars.Count;

    public override void InitInstance() {
        base.InitInstance();
        Instance = this;
    }

    public static Sprite GetAvatar(int icon) {
        return Instance._avatars[icon];
    }
}