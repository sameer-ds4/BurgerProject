using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
        UpdateComps();
    }


    private void UpdateComps()
    {
        currImg.sprite = GameManager.Instance.currentBurgerItem.image;
        nxtImg.sprite = GameManager.Instance.nextBurgerItem.image;
    }
}
