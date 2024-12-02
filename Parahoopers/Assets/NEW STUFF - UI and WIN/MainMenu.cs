using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool creditsIsActive = false;
    private bool optionsIsActive = false;
    [SerializeField] private bool howToPlayIsActive = false;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        optionsIsActive = true;
        optionsPanel.SetActive(true);
    }

    public void Credits()
    {
        creditsIsActive = true;
        creditsPanel.SetActive(true);
    }

    public void HowToPlay()
    {
        howToPlayIsActive = true;
        howToPlayPanel.SetActive(true);
    }

    public void GoBack()
    {
        if (optionsIsActive == true)
        {
            optionsPanel.SetActive(false);
            optionsIsActive = false;
        }

        if (creditsIsActive == true)
        {
            creditsPanel.SetActive(false);
            creditsIsActive = false;
        }

        if (howToPlayIsActive == true)
        {
            howToPlayPanel.SetActive(false);
            howToPlayIsActive = false;
        }
    }

    public void GoBackHTP()
    {
        if (howToPlayIsActive == true)
        {
            howToPlayPanel.SetActive(false);
            howToPlayIsActive = false;
        }
    }

    public void GoBackCredits()
    {
        if (creditsIsActive == true)
        {
            creditsPanel.SetActive(false);
            creditsIsActive = false;
        }
    }
}
