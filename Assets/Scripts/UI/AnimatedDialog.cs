using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AnimatedDialog : MonoBehaviour
{
    public float lettersSpeed;
    public TextMeshProUGUI dialogBox;
    public GameObject nextDialog;
    public string[] dialogContent;


    public GameInfo[] gameInfos;
    // public Animator ui_animator;
    private int dialogSet;
    private int dialogIndex;


    private void OnEnable() 
    {
        StartCoroutine(AnimateText());
    }


    private IEnumerator AnimateText()
    {
        nextDialog.SetActive(false);
        dialogBox.text = "";

        foreach (var letter in gameInfos[dialogSet].dialogs[dialogIndex].ToCharArray())
        {
            dialogBox.text += letter;
            yield return new WaitForSeconds(1/lettersSpeed);
        }

        dialogIndex++;
        nextDialog.SetActive(true);

    }

    public void NextDialogBtn()
    {
        if(dialogIndex < gameInfos[dialogSet].dialogs.Length)
            StartCoroutine(AnimateText());
        else
        {
            switch (dialogSet)
            {
                case 0:
                    TutorialManager.Instance.GetOrder();
                    break;

                case 1:
                    TutorialManager.Instance.ShowArrows();
                    break;
            }
            dialogSet++;
            dialogIndex = 0;
        }
    }

    private void turnOffIntro()
    {
        TutorialManager.Instance.GetOrder();
        // gameObject.SetActive(false);
    }

}

[Serializable]
public class GameInfo
{
    public string[] dialogs;
}
