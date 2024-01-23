using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameSc;
    public Settings settingsMenu;
    public GameObject burgerMain_P;

    public Image[] BurgerInfos;
    public Image currImg;
    public Image nxtImg;
    
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
        burgerMain_P.SetActive(false);

        switch (name)
        {
            case "Play":
                inGameSc.SetActive(true);
                UpdateComps();
                GameManager.startPlay = true;
                break;

            case "Settings":
                settingsMenu.gameObject.SetActive(true);
                break;

            case "Main":
                MainMenu_Set();
                ClosePages();
                break;
        }
    }

    private void MainMenu_Set()
    {
        mainMenu.SetActive(true);
        burgerMain_P.SetActive(true);
    }

    private void ClosePages()
    {
        inGameSc.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
    }

    private void UpdateComps()
    {
        for (int i = 0; i < BurgerInfos.Length; i++)
        {
            BurgerInfos[i].sprite = GameManager.Instance.burgerItemsList[i].image;
        }
        // currImg.sprite = GameManager.Instance.currentBurgerItem.image;
        // nxtImg.sprite = GameManager.Instance.nextBurgerItem.image;
    }
}
