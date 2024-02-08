using System;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Audio[] music;
    public Audio[] sounds;

    //public SpatialAudio[] spatialSounds;

    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup bgmMixer;

    int audioCount;
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        Instance = this;
        InitializeAudio();
    }

    private void OnEnable()
    {
        Settings.ChangeMusicVolume += MusicVolume_Update;
        Settings.ChangeSoundVolume += SoundVolume_Update;

        Settings.DefaultSettings += UpdateAudioSettings;
    }

    private void OnDisable()
    {
        Settings.ChangeMusicVolume -= MusicVolume_Update;
        Settings.ChangeSoundVolume -= SoundVolume_Update;

        Settings.DefaultSettings -= UpdateAudioSettings;
    }

    private void InitializeAudio()
    {
        audioCount = 0;
        if (sounds != null)
        {
            foreach (Audio s in sounds)
            {
                s.audioSource = transform.GetChild(audioCount).GetComponent<AudioSource>();
                audioCount++;
                s.audioSource.name = s.name;
                s.audioSource.clip = s.audioClip;
                s.audioSource.volume = s.volume;
                s.audioSource.pitch = s.pitch;
                s.audioSource.loop = s.loop;
                s.audioSource.playOnAwake = s.playOnAwake;

                s.audioSource.outputAudioMixerGroup = sfxMixer;
            }
        }

        if (music != null)
        {
            foreach (Audio m in music)
            {
                m.audioSource = transform.GetChild(audioCount).GetComponent<AudioSource>();
                m.audioSource.name = m.name;
                m.audioSource.clip = m.audioClip;
                m.audioSource.volume = m.volume;
                m.audioSource.pitch = m.pitch;
                m.audioSource.loop = m.loop;
                m.audioSource.playOnAwake = m.playOnAwake;

                m.audioSource.outputAudioMixerGroup = bgmMixer;
            }
        }
    }

    public void PlaySound(string name)
    {
        Audio audio = Array.Find(sounds, sound => sound.name == name);
        audio.audioSource.Play();
    }

    public void StopSound(string name)
    {
        Audio audio = Array.Find(sounds, sound => sound.name == name);
    
        if(audio != null)
            audio.audioSource.Stop();
    }

    public void PlayMusic(string name)
    {
        Audio audio = Array.Find(music, sound => sound.name == name);
        audio.audioSource.Play();
    }

    public void StopMusic(string name)
    {
        Audio audio = Array.Find(music, sound => sound.name == name);
        audio.audioSource.Stop();
    }

    public void ChangeVol_Music()   //Only for BGM when present as 1
    {
        // Audio audio = Array.Find(music, sound => sound.name == name);
        AudioSource audioSource = transform.GetChild(audioCount).GetComponent<AudioSource>();
        audioSource.DOFade(0.2f, 1);
    }

    //--------------------- Audio Settings -------------------------//

    private void SoundVolume_Update(float vol)
    {
        sfxMixer.audioMixer.SetFloat("SFXvolume", vol);
        // SaveDataHandler.Instance.saveData.soundVol = vol;
        SaveDataHandler.Instance.saveData.soundVol = vol;
    }

    private void MusicVolume_Update(float vol)
    {
        bgmMixer.audioMixer.SetFloat("BGMvolume", vol);
        // SaveDataHandler.Instance.saveData.musicVol = vol;
        SaveDataHandler.Instance.saveData.musicVol = vol;
    }

    private void UpdateAudioSettings()
    {
        // sfxMixer.audioMixer.SetFloat("SFXvolume", SaveDataHandler.Instance.saveData.soundVol);
        // bgmMixer.audioMixer.SetFloat("BGMvolume", SaveDataHandler.Instance.saveData.musicVol);
        sfxMixer.audioMixer.SetFloat("SFXvolume", SaveDataHandler.Instance.saveData.soundVol);
        bgmMixer.audioMixer.SetFloat("BGMvolume", SaveDataHandler.Instance.saveData.musicVol);
    }
}



[System.Serializable]
public class Audio
{
    [Header("Audio Properties")]
    public string name;

    public AudioSource audioSource;

    public AudioClip audioClip;
    [Range(0,1)]
    public float volume;
    [Range(-3, 3)]
    public float pitch;

    public bool loop;

    public bool playOnAwake;
}

[System.Serializable]
public class SpatialAudio : Audio
{
    [Header("3D Audio Settings")]
    [Range(0, 1)]
    public float spatialBlend;
    [Range(1, 50)]
    public float minDistance;
    [Range(50, 500)]
    public float maxDistance;
}