using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private AudioEventChannelSO AudioEventChannel;
    [SerializeField] private AudioClipSO buttonclickclip;
    bool pause = false;
    bool mainmenu = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && pause == false && mainmenu == false)
        {
            AudioEventChannel.PlayAudio(buttonclickclip);
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
        AudioEventChannel.PlayAudio(buttonclickclip);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        AudioEventChannel.PlayAudio(buttonclickclip);
        Application.Quit();
    }

    public void Home()
    {
        AudioEventChannel.PlayAudio(buttonclickclip);
        pause = false;
        mainmenu = true;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        AudioEventChannel.PlayAudio(buttonclickclip);
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
    }
}
