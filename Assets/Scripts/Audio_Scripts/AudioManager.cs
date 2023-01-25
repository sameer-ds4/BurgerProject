using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume;
        public bool loop;

        public AudioSource audioSource;

        public void SetAudioSource(AudioSource _audioSource)
        {
            audioSource = _audioSource;
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSource.volume = volume;
        }

        public void SetAudioSource()
        {
            if (audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.loop = loop;
                audioSource.volume = volume;
            }
        }

        public void Play()
        {
            audioSource.Play();
        }

        public void Stop()
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        public void StopImmidiate()
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }        
    }

    public Sound[] fxSounds;
    public Sound[] musics;

    public AudioMixer _mixer;

    private Sound _sound;
    private bool isMusicOn, isSFXOn;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(string name)
    {
        /*  if (string.IsNullOrEmpty(name) || !GameData.IsGameSound)
          {
              return;
          }*/

        Sound sound = Array.Find(fxSounds, s => s.name == name);
        if (sound != null)
        {
            if (sound.audioSource == null)
            {
                Transform _tempTransform = transform.Find(name);
                if (_tempTransform)
                {
                    sound.audioSource = _tempTransform.GetComponent<AudioSource>();
                }
            }

            sound.Play();
        }
        else
        {
            Debug.LogWarning("AudioManager -- Sound not found:" + name);
        }
    }
    public void StopSound(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        Sound sound = Array.Find(fxSounds, s => s.name == name);
        if (sound != null)
        {
            if (sound.audioSource == null)
            {
                Transform _tempTransform = transform.Find(name);
                if (_tempTransform)
                {
                    sound.audioSource = _tempTransform.GetComponent<AudioSource>();
                }
            }

            //sound.Stop();
            sound.StopImmidiate();
        }
        else
        {
            Debug.LogWarning("AudioManager -- Sound not found:" + name);
        }
    }

    public void StopAllSounds()
    {
        foreach (var item in fxSounds)
        {
            item.audioSource.Stop();
        }
    }

}
