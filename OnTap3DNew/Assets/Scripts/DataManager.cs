using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
	public int score;
}

public class DataManager : MonoBehaviour
{
	private const string PlayerDataKey = "PlayerData";

    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PlayerDataKey, json);
        PlayerPrefs.Save();
    }

    public PlayerData LoadData()
    {
        if (PlayerPrefs.HasKey(PlayerDataKey))
        {
            string json = PlayerPrefs.GetString(PlayerDataKey);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }
}
