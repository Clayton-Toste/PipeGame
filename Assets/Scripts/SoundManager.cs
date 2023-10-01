using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Slider musicVolumeSlider, soundVolumeSlider;
    private int musicVolume, soundVolume;

    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;

            if (sound.name == "BackgroundMusic")
            {
                sound.source.loop = true;
                sound.source.playOnAwake = true;
            }
        }
    }

    public void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetInt("musicVolume", 70);
        } 
        musicVolume = PlayerPrefs.GetInt("musicVolume");
        musicVolumeSlider.value = musicVolume/100f;

        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetInt("soundVolume", 70);
        } 
        soundVolume = PlayerPrefs.GetInt("soundVolume");
        soundVolumeSlider.value = soundVolume/100f;

        Play("BackgroundMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No music");
            return;
        }
        s.source.Play();
    }

    public void Refresh()
    {
        musicVolume = (int)(musicVolumeSlider.value*100);
        PlayerPrefs.SetInt("musicVolume", musicVolume);

        soundVolume = (int)(soundVolumeSlider.value*100);
        PlayerPrefs.SetInt("soundVolume", soundVolume);
    }

    public void PlaySound()
    {

    }
}
