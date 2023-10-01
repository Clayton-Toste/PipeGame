using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider musicVolumeSlider, soundVolumeSlider;
    private int musicVolume, soundVolume;

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
