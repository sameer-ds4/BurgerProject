using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private LevelSelect levelMenu;
    [SerializeField] private GameObject inGameSc;
    [SerializeField] private GameObject pauseSc;
    [SerializeField] private Settings settingsMenu;
    public GameObject gameOver;
    public GameObject levelComp;

    // [Space]
    [Header("Order Components")]
    public Image[] BurgerInfos;

    // public OrderCard orderCard;
    // public List<OrderCard> orderCards;
    
    
    private void Awake() 
    {
        Instance = this;    
    }
    private void OnEnable() 
    {
        GameManager.UpdateBurgerInfo += UpdateComps;    
    }
    private void OnDisable() 
    {
        GameManager.UpdateBurgerInfo -= UpdateComps;    
    }
    void Start()
    {
        MainMenu_Set();
        ClosePages();
    }

    public void OnBtn_Click(string name)
    {
        mainMenu.SetActive(false);
        AudioManager.Instance.PlaySound("Button");

        switch (name)
        {
            case "Play":
                // StartGame();
                if(!SaveDataHandler.Instance.saveData.tutorial)
                    levelMenu.gameObject.SetActive(true);
                else
                {
                    inGameSc.SetActive(true);
                    GameManager.Instance.TutorialInitialize();
                    TutorialStart();
                }
                break;
                
                // UpdateComps();
                // TutorialStart();
                // GameManager.startPlay = true;

            case "Start":
                StartGame();
                break;

            case "Settings":
                settingsMenu.gameObject.SetActive(true);
                break;

            case "Pause":
                PauseGame();
                break;

            case "Resume":
                ResumeGame();
                break;

            case "Main":
                MainMenu_Set();
                ClosePages();
                break;

            case "Retry":
                SceneManager.LoadScene(0);
                break;

            case "Home":
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void MainMenu_Set()
    {
        mainMenu.SetActive(true);
    }

    private void ClosePages()
    {
        inGameSc.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        gameOver.SetActive(false);
        levelComp.SetActive(false);
    }

    public void StartGame()
    {
        inGameSc.SetActive(true);
        GameManager.Instance.EnableManagers();
        UpdateComps();
        // OrderManager.Instance.OrderPlace();
        GameManager.startPlay = true;

    }

    private void UpdateComps()
    {
        for (int i = 0; i < BurgerInfos.Length; i++)
        {
            BurgerInfos[i].sprite = GameManager.Instance.burgerItemsList[i].image;
        }
    }

    private void TutorialStart()
    {
        TutorialManager.Instance.tutorialCard.SetActive(true);

        for (int i = 0; i < BurgerInfos.Length; i++)
        {
            BurgerInfos[i].sprite = GameManager.Instance.burgerItemsList[i].image;
        }
    }

    public void LevelComplete()
    {
        levelComp.gameObject.SetActive(true);
    }

    private void PauseGame()
    {
        inGameSc.SetActive(false);
        pauseSc.SetActive(true);
        GameManager.startPlay = false;
        AudioManager.Instance.ChangeVol_Music();
    }

    private void ResumeGame()
    {
        pauseSc.SetActive(false);
        inGameSc.SetActive(true);
        GameManager.startPlay = true;
        AudioManager.Instance.bgmMixer.audioMixer.SetFloat("BGMvolume", 1);
    }
}
