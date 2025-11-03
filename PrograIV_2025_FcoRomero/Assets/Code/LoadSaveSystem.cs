using System;
using UnityEngine;
[System.Serializable]
public class LoadSaveSystem 
{
    string playerInfoDataKey = "PlayerInfo";

    public PlayerDataInfo LoadPlayerInfo(Action<PlayerDataInfo> onEndLoadData)
    {
        string json = PlayerPrefs.GetString(playerInfoDataKey);

        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);

        PlayfabLogin playFab = new PlayfabLogin();
        playFab.LoadData(playerInfoDataKey, (data, result) =>
        {
            if (result == true)
            {
                json = data;
                PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
                onEndLoadData(loadData);
            }
        });
        return loadData;
    }

    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);

        PlayerPrefs.SetString(playerInfoDataKey, json);
        Debug.Log("SaveData");
    }
}
