using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataHandler : Singleton<SaveDataHandler>
{ 
    public SaveData saveData;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
        if (File.Exists(FileSearch.filePath))
        {
            ReadSave();
            LoadData();
        }
        else
        {
            WriteSave();
            onSaveInitialized();
        }
    }

    private void WriteSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(FileSearch.filePath, FileMode.Create);
        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    private void ReadSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(FileSearch.filePath, FileMode.Open);
        saveData = formatter.Deserialize(fileStream) as SaveData;
        fileStream.Close();
    }

    private void LoadData()
    {
        if(PlayerPrefs.GetInt("Override", 0) == 0)
        {
            saveData.firstPlay = false;
            saveData.ContinueState = false;
            saveData.ghostSpeed = 1.34f + 0.03f * saveData.levelID % 3;

            PlayerPrefs.SetInt("Override", 1);
        }
    }

    private void onSaveInitialized()
    {
        for (int i = 0; i < 3; i++)
        {
            saveData.Opened.Add(false);
        }

        saveData.firstPlay = true;

        saveData.musicVol = 0;
        saveData.soundVol = 0;
        saveData.vibrationOn = true;

        saveData.pl_name = "Player";
        saveData.levelID = 1;

        saveData.highScore = 0;
        saveData.cupCakes = 0;
        saveData.splItems = 0;

        saveData.ContinueState = false;
        saveData.ghostSpeed = 1.34f;


        // saveData.testParam = 20;
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
            WriteSave();
    }

    public void SaveData()
    {
        WriteSave();
    }

    public static class FileSearch
    {
        public static string filePath = Application.persistentDataPath + "/SaveData.sv";
    }

}

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(this);
    }
}
