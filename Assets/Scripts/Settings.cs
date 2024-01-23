using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
// using Lofelt.NiceVibrations;


public class Settings : MonoBehaviour
{
    public Slider soundController;
    public Slider musicController;

    public Image vibrationStatus;

    public Sprite toggleOn, toggleOff;

    public delegate void ChangeVolume(float vol);
    public static event ChangeVolume ChangeMusicVolume;
    public static event ChangeVolume ChangeSoundVolume;

    public delegate void StartDefaults();
    public static event StartDefaults DefaultSettings;

    private void OnEnable()
    {
        DefaultSettings += SlidersUpdate;
        DefaultSettings += UpdateVibrations;
    }

    private void OnDisable()
    {
        DefaultSettings -= SlidersUpdate;
        DefaultSettings -= UpdateVibrations;
    }

    private void Start()
    {
        DefaultSettings?.Invoke();

        DOVirtual.DelayedCall(3, () =>
        {
            //Tweening.TweenPunch(testObject, 2, Vector3.one * 6);
            //Tweening.AlphaFadeOut(testObject, 1.4f);
            //Tweening.TweenOut(testObject, 1.3f);
            //Tweening.AlphaFadeOut(testObject, 1.3f);
        });
    }


    public void SoundUpdate()
    {
        soundController.onValueChanged.AddListener((v) => ChangeSoundVolume?.Invoke(v));

        if(soundController.value < -25f)
            soundController.onValueChanged.AddListener((v) => ChangeSoundVolume?.Invoke(-80f));

    }

    public void MusicUpdate()
    {
        musicController.onValueChanged.AddListener((v) => ChangeMusicVolume?.Invoke(v));

        if(musicController.value < -25f)
            musicController.onValueChanged.AddListener((v) => ChangeMusicVolume?.Invoke(-80f));
    }

    private void SlidersUpdate()
    {
        soundController.value = SaveDataHandler.Instance.saveData.soundVol;
        musicController.value = SaveDataHandler.Instance.saveData.musicVol;

        if(soundController.value < -25f)
            ChangeSoundVolume?.Invoke(-80f);

        if(musicController.value < -25f)
            ChangeMusicVolume?.Invoke(-80f);
    }

    public void ToggleVibrations()
    {
        vibrationStatus.transform.DOPunchScale(Vector3.one * 0.5f, 0.2f);

        if (SaveDataHandler.Instance.saveData.vibrationOn)
        {
            SaveDataHandler.Instance.saveData.vibrationOn = false;
            // AudioManager.Instance.PlaySound("ToggleOff");
            vibrationStatus.sprite = toggleOff;
        }
        else
        {
            SaveDataHandler.Instance.saveData.vibrationOn = true;
            // AudioManager.Instance.PlaySound("ToggleOn");
            // HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
            vibrationStatus.sprite = toggleOn;
        }
    }

    private void UpdateVibrations()
    {
        if (SaveDataHandler.Instance.saveData.vibrationOn)
            vibrationStatus.sprite = toggleOn;
        else
            vibrationStatus.sprite = toggleOff;
    }

    private void MuteSound()
    {
        soundController.value = -80f;
    }

    private void MuteMusic()
    {
        musicController.value = -80f;
    }
}
