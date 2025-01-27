using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [FormerlySerializedAs("AudioEventChannel")] [SerializeField] private AudioEventChannelSO audioEventChannel;
    [FormerlySerializedAs("buttonclickclip")] [SerializeField] private AudioClipSO buttonClickAudioClip;
    bool pause = false;
    bool mainmenu = true;

    private void Start()
    {
        pause = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && pause == false)
        {
            audioEventChannel.PlayAudio(buttonClickAudioClip);
            pause = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
       

        if(Input.GetKeyDown(KeyCode.P))
        {
            mainmenu = false;
        }
    }
    public void PlayGame()
    {
        mainmenu = false;
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Home()
    {
        pause = false;
        mainmenu = true;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void PlayClickSound()
    {
        audioEventChannel.PlayAudio(buttonClickAudioClip);
    }
}
