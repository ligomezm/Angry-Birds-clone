using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private float defaultMusicVolume = 0.5f;
    [SerializeField] private float defaultSFXVolume = 1f;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = GetComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.volume = defaultMusicVolume;
        sfxSource.volume = defaultSFXVolume;
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
            return;
        }
        else
        {
            musicSource.clip = sound.audioClip;
            musicSource.Play();
            
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found" + name);
            return;
        }
        else
        {
            sfxSource.clip = sound.audioClip;
            sfxSource.PlayOneShot(sound.audioClip);
        }
    }
}
