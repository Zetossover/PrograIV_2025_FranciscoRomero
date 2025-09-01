using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    public string keyName;
    string info;

    public Monsito monsitoBubu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetData();
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetString(keyName, info);
    }
    public void GetData()
    {
        info = PlayerPrefs.GetString(keyName);
    }
    public void Save()
    {
        string data = JsonUtility.ToJson(monsitoBubu);
        PlayerPrefs.SetString(keyName, data);
    }
    public void Load()
    {
        string data = PlayerPrefs.GetString(keyName, "NULL");
        if (data != "NULL")
        {
            print(data);
            monsitoBubu = JsonUtility.FromJson<Monsito>(data);
        }
    }
}
