using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Settings Panel")]
    public GameObject settings;
    public GameObject settingsBot;
    public GameObject settingsPanel;
   
    public GameObject soundOnImg;
    public GameObject soundOffImg;
    public GameObject soundOffCrossImg;

    public GameObject vibrationOnImg;
    public GameObject vibrationOffImg;    
    public GameObject vibrationCrossOffImg;

    public GameObject closeBot;
    private void Start()
    {
        OnStartCalls();
        CheckSoundandVibration();
    }
    private void OnStartCalls()
    {
        settingsBot.SetActive(true);
        settings.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void SettingsBot()
    {
        StartCoroutine(SettingsRoutine());
    }
    public IEnumerator SettingsRoutine()
    {
        Button tempCloseBot = closeBot.GetComponent<Button>();
        tempCloseBot.enabled = false;

        AudioManager.Instance.PlaySound("Button_Click");
        HapticTouchManager.LightHapticTouch();

        settingsPanel.SetActive(true);
        settings.SetActive(true);

        Fading.OnFadeInAndScaleUP(settings, 0.5f);

        Fading.OnScaleOut(settingsBot,0.3f,Vector3.one*0);

        Fading.OnFade(settingsPanel, 0.5f, 0f, 0.8f);

        yield return new WaitForSeconds(0.3f);
        settingsBot.SetActive(false);
        Time.timeScale = 0;

        tempCloseBot.enabled = true;
    }
    public void CloseSettingsBot()
    {
        StartCoroutine(CloseSettingsRoutine());        
    }
    public IEnumerator CloseSettingsRoutine()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlaySound("Button_Click");
        HapticTouchManager.LightHapticTouch();

        Fading.OnScaleOut(settings, 0.5f,Vector3.one * 0);

        settingsBot.SetActive(true);
        Fading.OnBubleFX(settingsBot, 0.3f, Vector3.one * 0,Vector3.one);
                
        Fading.OnFade(settingsPanel, 0.5f, 0.8f, 0);
        yield return new WaitForSeconds(0.5f);
        settingsPanel.SetActive(false);
        
    }
    public void SoundOffBot()
    {
        AudioManager.Instance.PlaySound("Button_Click");
        HapticTouchManager.LightHapticTouch();

        if (SaveDataHandler.Instance.SoundOn)
        {
            SaveDataHandler.Instance.SoundOn = false;
        }
        else
        {
            SaveDataHandler.Instance.SoundOn = true;
        }
        CheckSoundandVibration();
    }
    public void VibrationOffBot()
    {
        AudioManager.Instance.PlaySound("Button_Click");
        HapticTouchManager.LightHapticTouch();

        if (SaveDataHandler.Instance.VibrationOn)
        {
            SaveDataHandler.Instance.VibrationOn = false;
        }
        else
        {
            SaveDataHandler.Instance.VibrationOn = true;
        }
        CheckSoundandVibration();
    }
    private void CheckSoundandVibration()
    {
        if (SaveDataHandler.Instance.SoundOn)
        {
            soundOnImg.SetActive(true);
            soundOffImg.SetActive(false);
            soundOffCrossImg.SetActive(false);
            AudioManager.Instance._mixer.SetFloat("Val", 0f);
        }
        else
        {
            soundOnImg.SetActive(false);
            soundOffImg.SetActive(true);
            soundOffCrossImg.SetActive(true);
            AudioManager.Instance._mixer.SetFloat("Val", -80f);
        }

        if (SaveDataHandler.Instance.VibrationOn)
        {
            vibrationOnImg.SetActive(true);
            vibrationOffImg.SetActive(false);
            vibrationCrossOffImg.SetActive(false);
            HapticTouchManager.IsHaptic = true;
        }
        else
        {
            vibrationOnImg.SetActive(false);
            vibrationOffImg.SetActive(true);
            vibrationCrossOffImg.SetActive(true);
            HapticTouchManager.IsHaptic = false;
        }
    }
}