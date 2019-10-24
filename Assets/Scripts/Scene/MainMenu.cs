using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    private bool isSettingsMenu;

    void Start()
    {
        isSettingsMenu = false;
        settingsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Player quit game.");
        Application.Quit();
    }

    public void SettingsMenu()
    {
        if (!isSettingsMenu)
        {
            settingsMenu.SetActive(true);
            isSettingsMenu = true;
        }
        else if (isSettingsMenu)
        {
            settingsMenu.SetActive(false);
            isSettingsMenu = false;
        }
    }

    public void LoadPrototype()
    {
        SceneManager.LoadScene(2);
    }
}
