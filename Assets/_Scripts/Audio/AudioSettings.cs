using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
        
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        
        SetMusicVolume();
        SetSFXVolume();
    }

    public void IncreaseMusicVolume()
    {
        musicSlider.value = musicSlider.value + 10;
    }
    
    public void DecreaseMusicVolume()
    {
        musicSlider.value = musicSlider.value - 10;
    }
    
    public void IncreaseSFXVolume()
    {
        SFXSlider.value = SFXSlider.value + 10; 
    }
    
    public void DecreaseSFXVolume()
    {
        SFXSlider.value = SFXSlider.value - 10;
    }
}
