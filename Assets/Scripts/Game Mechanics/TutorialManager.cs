using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set;}
    [SerializeField] public GameObject tutorialCard;
    [SerializeField] private AnimatedDialog animatedDialog;
    // [SerializeField] private GameObject chefCard;
    [SerializeField] private GameObject arrow;
    public List<GameObject> tutPlates = new List<GameObject>();


    private void Awake() 
    {
        Instance = this;
        arrow.SetActive(false);
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
        arrow.SetActive(true);
        GameManager.startPlay = true;
        AdjustArrow();
        // EndTutorial();
    }

    int arrowIndex = 0;

    public void AdjustArrow()
    {
        if(arrowIndex > tutPlates.Count - 3)
        {
            arrow.SetActive(false);
            RenamePlates();
            EndTutorial();
            return;
        }
        // (arrow.transform as RectTransform).position = tutPlates[0].transform.position;
        // arrow.transform.DOLocalMove(new Vector3(275, 151, -100), 0);
        arrow.transform.DOMove(tutPlates[arrowIndex].transform.position + new Vector3(0.5f, 0.5f, -0.75f), 0.5f);
        arrowIndex++;
    }

    public void EndTutorial()
    {
        tutorialCard.SetActive(false);
        GameManager.startPlay = true;
        SaveDataHandler.Instance.saveData.tutorial = false;
        PlayerInput.tagToTrack = "Plate";
    }

    private void RenamePlates()
    {
        foreach (var item in tutPlates)
        {
            item.tag = "Plate";
        }
    }
}
