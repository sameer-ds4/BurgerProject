using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("ScratchCard Data")]
    public List<bool> Opened;

    [Header("SpinWheel Reward")]
    public int rewardIndex;

    [Header("Game variable status")]
    public bool firstPlay;
    public float soundVol;
    public float musicVol;
    public bool vibrationOn;

    [Header("Player Details")]
    public string pl_name;
    public int levelID;

    [Header("PacMan Data")]
    public int highScore;
    public int cupCakes;
    public int splItems;

    public bool ContinueState;

    public float ghostSpeed;

    // public int testParam;
}
