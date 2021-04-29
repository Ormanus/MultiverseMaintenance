using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public class Data
    {
        public bool[] collected = new bool[3];
    }

    public static Data saveData;

    public void Save()
    {
        string data = "";
        for (int i = 0; i < saveData.collected.Length; i++)
        {
            data += saveData.collected[i] ? "1" : "0";
        }
        PlayerPrefs.SetString("Data", data);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Data"))
        {
            string s = PlayerPrefs.GetString("Data");
            saveData = new Data();
            for (int i = 0; i < s.Length; i++)
            {
                saveData.collected[i] = s[i] == '1';
            }
        }
    }

    public void Collect(int level)
    {
        if (saveData == null)
            saveData = new Data();
        saveData.collected[level] = true;
        Save();
    }

    public bool isCollected(int level)
    {
        if (saveData != null)
        {
            return saveData.collected[level];
        }
        else
        {
            return false;
        }
    }

    public bool allCollected()
    {
        if (saveData != null)
        {
            for (int i = 0; i < saveData.collected.Length; i++)
            {
                if (!saveData.collected[i])
                    return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Restart()
    {
        saveData = new Data();
        PlayerPrefs.DeleteAll();
    }
}
