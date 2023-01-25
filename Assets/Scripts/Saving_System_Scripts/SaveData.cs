using UnityEngine;
[System.Serializable]
public class SaveData
{
    [Header ("Settings Save Data")]
    public bool vibrationOn;
    public bool soundOn;

    [Header("Save Data")]
    public int coins;

    [Header("Coins Save Data")]
    public int levelID;
    public int cash;

    [Header("Player Data")]
    public int player_index;
}