using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    //プロパティとしてUserIdを作成する
    public static string UserId
    {
        set
        {
            PlayerPrefs.SetString("UserId", value);
            PlayerPrefs.Save();
        }
        get => PlayerPrefs.GetString("UserId");
    }
}
