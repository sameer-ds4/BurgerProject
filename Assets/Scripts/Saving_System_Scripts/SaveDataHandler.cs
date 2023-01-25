using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataHandler : Singleton <SaveDataHandler>
{
    //[HideInInspector]
    public SaveData SaveData;

    #region Save Properties
    public int LevelID
    {
        get
        {
            return SaveData.levelID;
        }
        set
        {
            SaveData.levelID = value;
            SavingData();
        }
    }
    public bool VibrationOn
    {
        get
        {
            return SaveData.vibrationOn;
        }
        set
        {
            SaveData.vibrationOn = value;
            SavingData();
        }
    }
    public bool SoundOn
    {
        get
        {
            return SaveData.soundOn;
        }
        set
        {
            SaveData.soundOn = value;
            SavingData();
        }
    }
    public int Cash
    {
        get
        {
            return SaveData.cash;
        }
        set
        {
            SaveData.cash = value;
            SavingData();
        }
    }
    #endregion

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
        if (File.Exists(WordMapExtension.path))
        {
            ReadData();
            LoadData();
        }
        else
        {
            WriteData();
            InitializeData();
        }
    }

    private void WriteData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(WordMapExtension.path, FileMode.Create);
        formatter.Serialize(stream, SaveData);
        stream.Close();
    }

    private void ReadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(WordMapExtension.path, FileMode.Open);
        SaveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();
    }

    public void LoadData()
    {

    }

    private void InitializeData()
    {
        SaveData.soundOn = true;
        SaveData.vibrationOn = true;
        SaveData.cash = 100;
        SaveData.levelID = 0;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SavingData();
        }
    }

    public void SavingData()
    {
        WriteData();
    }
}

public static class WordMapExtension
{
    public static string path = Application.persistentDataPath + "/GameSaveData.kb";
}
