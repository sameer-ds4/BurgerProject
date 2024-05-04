using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set;}
    [SerializeField] public GameObject tutorialCard;
    [SerializeField] private AnimatedDialog animatedDialog;
    public List<GameObject> tutPlates = new List<GameObject>();


    private void Awake() 
    {
        Instance = this;
    }

    public void GetOrder()
    {
        OrderManager.Instance.OrderPlace();
        tutorialCard.SetActive(false);

        DOVirtual.DelayedCall(1.4f, () =>
        {
            SecondVerse();
        });
    }

    public void SecondVerse()
    {
        tutorialCard.SetActive(true);
    }

    public void ShowArrows()
    {
        tutorialCard.SetActive(false);
        EndTutorial();
    }

    public void EndTutorial()
    {
        GameManager.startPlay = true;
        SaveDataHandler.Instance.saveData.tutorial = false;
        PlayerInput.tagToTrack = "Plate";
        foreach (var item in tutPlates)
        {
            item.tag = "Plate";
        }
    }
}
