using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool paused = false;
    private bool htpIsActive;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject htpMenu;
    [SerializeField] private GameObject altitude;
    //[SerializeField] private GameObject speedometer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //speedometer.SetActive(true);
        altitude.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        paused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        //speedometer.SetActive(false);
        altitude.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        paused = true;
    }

    public void HowToPlay()
    {
        htpMenu.SetActive(true);
        htpIsActive = true;
    }

    public void GoBackResume()
    {
        if (htpIsActive == true)
        {
            htpIsActive = false;
            htpMenu.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //This here is my pause menu!! So cool! So slay
}
