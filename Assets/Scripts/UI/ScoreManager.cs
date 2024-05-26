using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    public static int scoreMain;

    private void OnEnable() 
    {
        GridManager.IncrementScore += UpdateScore;
    }

    private void OnDisable()
    {
        GridManager.IncrementScore -= UpdateScore;
    }

    void Start()
    {
        scoreMain = 0;
        scoreDisplay.text = "Score: " + scoreMain.ToString();
    }

    private void UpdateScore(int amount)
    {
        scoreMain += amount;
        transform.DOPunchScale(Vector3.one * 0.5f, 0.4f, 1, 0.2f);
        scoreDisplay.text = "Score " + scoreMain.ToString();
    }

    public static int UpdateHighscore()
    {
        if(scoreMain > SaveDataHandler.Instance.saveData.highScore)
            SaveDataHandler.Instance.saveData.highScore = scoreMain;

        return SaveDataHandler.Instance.saveData.highScore;
    }
}
