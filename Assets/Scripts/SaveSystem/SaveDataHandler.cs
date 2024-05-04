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
            // Debug.LogError("kjndlskbdlsdb");
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
 
    }

    private void onSaveInitialized()
    {
        saveData.tutorial = true;

        saveData.musicVol = 0;
        saveData.soundVol = 0;
        saveData.hapticState = 1;
        saveData.vibrationOn = true;

        saveData.pl_name = "Player";
        saveData.levelID = 1;
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
