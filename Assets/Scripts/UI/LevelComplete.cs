using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public TextMeshProUGUI scoreFinalText;
    public TextMeshProUGUI highScoreFinalText;

    private void OnEnable() 
    {
        GetInfo();
    }

    private void GetInfo()
    {
        ScoreManager.UpdateHighscore();
        scoreFinalText.text = ScoreManager.scoreMain.ToString();
        highScoreFinalText.text = ScoreManager.UpdateHighscore().ToString();
    }
}
