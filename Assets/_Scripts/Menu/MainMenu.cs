using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool pause = false;
    bool mainmenu = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && pause == false && mainmenu == false)
        {
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
}
