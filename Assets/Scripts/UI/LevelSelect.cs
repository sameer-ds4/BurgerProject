using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelInd;
    [SerializeField] private TextMeshProUGUI gridInfo;
    [SerializeField] private TextMeshProUGUI orderInfo;

    [SerializeField] private LevelInfo[] levelInfos;

    private void Start() 
    {
        UpdateLevelInfo_Display(0);    
    }

    public void UpdateLevelInfo_Display(int levelDifficulty)
    {
        AudioManager.Instance.PlaySound("Click");
        GameManager.difficultyIndex = levelDifficulty;
        levelInd.text = levelInfos[levelDifficulty].level;
        gridInfo.text = "Grid Size - " + levelInfos[levelDifficulty].gridContent;
        orderInfo.text = "Max Orders - " + levelInfos[levelDifficulty].orderContent;
    }

    public void StartGame()
    {
        UIManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class LevelInfo
{
    public string level;
    public string gridContent;
    public string orderContent;
}
