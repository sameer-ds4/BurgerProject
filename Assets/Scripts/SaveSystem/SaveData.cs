using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("Game variable status")]
    public bool firstPlay;
    public float soundVol;
    public float musicVol;
    public bool vibrationOn;

    [Header("Player Details")]
    public string pl_name;
    public int levelID;

}
