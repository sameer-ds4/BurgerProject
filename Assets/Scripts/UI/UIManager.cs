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
    [SerializeField] private GameObject inGameSc;
    [SerializeField] private Settings settingsMenu;
    public GameObject gameOver;
    public GameObject levelComp;

    [Space]
    [Header("Order Components")]
    public Image[] BurgerInfos;

    public OrderCard orderCard;
    public List<OrderCard> orderCards;
    
    
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

        switch (name)
        {
            case "Play":
                StartGame();
                break;
                
                // UpdateComps();
                // TutorialStart();
                // GameManager.startPlay = true;

            case "Settings":
                settingsMenu.gameObject.SetActive(true);
                break;

            case "Main":
                MainMenu_Set();
                ClosePages();
                break;

            case "Retry":
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

    private void StartGame()
    {
        inGameSc.SetActive(true);
        // GameManager.startPlay = true;

        if(!SaveDataHandler.Instance.saveData.tutorial)
        {
            GameManager.Instance.InitializeBurgerComps();
            UpdateComps();
            OrderManager.Instance.OrderPlace();
            GameManager.startPlay = true;
        }
        else
        {
            GameManager.Instance.TutorialInitialize();
            TutorialStart();
        }
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
}
