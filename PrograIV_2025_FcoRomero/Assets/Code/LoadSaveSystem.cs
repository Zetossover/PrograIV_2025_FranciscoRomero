using UnityEngine;
public class LoadSaveSystem : MonoBehaviour 
{
    string playerInfoDataKey = "PlayerInfo";

    public PlayerDataInfo LoadPlayerInfo()
    {
        string json = PlayerPrefs.GetString(playerInfoDataKey);
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        return loadData;
    }

    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);

        PlayerPrefs.SetString(playerInfoDataKey, json);
        Debug.Log("SaveData");
    }
}
