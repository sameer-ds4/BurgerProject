using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI anim Info")]
    public float uiAnimsSpeed;

    [Header("Level Complete")]
    public GameObject levelCompletePanel;
    public Text addCashText;
    public GameObject[] levelCompleteObjs;

    [Header("Level Faild")]
    public GameObject levelFaildPanel;
    public GameObject[] levelFaildObjs;

    [Header("Tutorial Panel")]
    public GameObject tutorialPanel;

    [Header("SettingsMenu Ref")]
    public SettingsMenu _settingsMenu;

    [Header("Top Panel")]
    public GameObject topPanel;
    public GameObject settingsBot;
    public GameObject levelIDDisplayImag;
    public Text levelIDText;
    public GameObject cashDisplayImag;
    public Text cashText;

    [Header("InputLocker Panel")]
    public GameObject inputLocker;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnLevelStartCalls();
    }
    public void Update()
    {
       /* if (isFirstTuch)
        {
            SetOnOff(tutorialPanel, false);
        }*/
    }
    private void OnLevelStartCalls()
    {
       /* SetLevelIDNumberDisplay();
        SetCashDisplay();
        SetOnOff(topPanel, true);
        (topPanel.transform as RectTransform).DOAnchorPosY(-150f, uiAnimsSpeed);
        SetOnOff(levelCompletePanel, false);
        SetOnOff(levelFaildPanel, false);
        SetOnOff(tutorialPanel, true);*/
    }

    private void SetLevelIDNumberDisplay()
    {
        levelIDText.text = "Level  " + (SaveDataHandler.Instance.LevelID + 1).ToString();
    }
    private void SetCashDisplay()
    {
        cashText.text = SaveDataHandler.Instance.Cash.ToString();
    }

    public IEnumerator OnLevelCompleteSequenceIn()
    {
        (topPanel.transform as RectTransform).DOAnchorPosY(150f, uiAnimsSpeed);
        inputLocker.SetActive(true);
        addCashText.text = "";
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < levelCompleteObjs.Length; i++)
        {
            Fading.OnBubleFX(levelCompleteObjs[i].gameObject, uiAnimsSpeed, Vector3.zero, Vector3.one);
        }
        SetOnOff(levelCompletePanel, true);

        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySound("Coins_Sound");
        inputLocker.SetActive(false);

    }
    private IEnumerator OnLevelCompleteSequenceOut()
    {
        yield return new WaitForSeconds(0.6f);
        /*Fading.OnScaleOut(levelCompletePanel, 0.5f, Vector3.one * 0f);
        // Fading.OnFade(levelCompletePanelBG, 0.5f, 0.7f, 0);
        LevelContainer.Instance._levelGenerator.ClearGrid();
        LevelContainer.Instance.descriptionButton.gameObject.SetActive(false);
        Fading.OnScaleOut(LevelContainer.Instance.descriptionButton.gameObject, 0.6f, Vector3.one * 0);//
        LevelContainer.Instance.DescriptionButtonChecker(false);
        SetOnOff(levelCompletePanelBG, false);
        _pointsSystem.ResetPointsSystem();*/
    }

    public IEnumerator OnLevelFaildSequenceIn()
    {
        (topPanel.transform as RectTransform).DOAnchorPosY(150f, uiAnimsSpeed);
        inputLocker.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < levelFaildObjs.Length; i++)
        {
            Fading.OnBubleFX(levelFaildObjs[i].gameObject, uiAnimsSpeed, Vector3.zero, Vector3.one);
        }
        levelFaildPanel.SetActive(true);

        inputLocker.SetActive(false);
    }
    public void SetOnOff(GameObject gameObjectName, bool Value)
    {
        gameObjectName.SetActive(Value);
    }

    #region Buttons System
    public void OnTutorialBot()
    {
        SetOnOff(tutorialPanel, false);
        //CarMovement.Instance.SetCarSpeed(CarMovement.Instance._carcontroller._CurrentCar.speed);
    }

    public void OnPlayBot()
    {
        StartCoroutine(OnPlayButtonRoutine());

        Debug.LogError("level_start_" + (SaveDataHandler.Instance.LevelID + 1));

        //AnalyticsHandler_BigCode.Instance.SetReachedPage("level_start_" + (SaveDataHandler.Instance.LevelID + 1));

        //GameAnalytics_Bigcode.Instance.progressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "level_" + (SaveDataHandler.Instance.LevelID + 1));
    }

    private IEnumerator OnPlayButtonRoutine()
    {
        HapticTouchManager.MediumHapticTouch();
        //AudioManager.Instance.PlaySound("Button_Click");
        SetOnOff(tutorialPanel, false);
        
        yield return new WaitForSeconds(0.5f);
    }

    public void OnBackBot()
    {
        StartCoroutine(OnBackButtonRoutine());
    }
    private IEnumerator OnBackButtonRoutine()
    {
        HapticTouchManager.MediumHapticTouch();
        //AudioManager.Instance.PlaySound("Button_Click");
        yield return new WaitForSeconds(0.5f);
    }
    public void OnNextBot()
    {
        StartCoroutine(OnNextButtonRoutine());
    }
    private IEnumerator OnNextButtonRoutine()
    {
        inputLocker.SetActive(true);
        HapticTouchManager.MediumHapticTouch();
        //AudioManager.Instance.PlaySound("Button_Click");

        OnLevelStartCalls();
        GameManager.Instance.CreateLevel();
        inputLocker.SetActive(false);
        yield return null;
    }

    public void OnRetryBot()
    {
        StartCoroutine(OnRetryButtonRoutine());
    }
    private IEnumerator OnRetryButtonRoutine()
    {
        inputLocker.SetActive(true);
        HapticTouchManager.MediumHapticTouch();
        //AudioManager.Instance.PlaySound("Button_Click");

        OnLevelStartCalls();
        GameManager.Instance.CreateLevel();
        inputLocker.SetActive(false);
        yield return null;
    }
    #endregion
}