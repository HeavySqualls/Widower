
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public float levelTime = 5;

    public bool isGameOver = false;
    public bool isGameStart = false;

    public int gameDeathCount; // not used at the moment
    public int numberOfPlayers; //for knowing if it is multi player
    public int currentLevel;

    private PlayerManager playerManager;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Toolbox.GetInstance().GetTimer().ResetTimer();
        Toolbox.GetInstance().GetPlayerManager().ResetPlayerManager();
        ResetGameManager();
    }

    private void Start() // references to scene objects must also be made in ResetGameManager() below
    {


        //get text object from the scene

        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void ResetGameManager()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void changeLevel(int selectedLevel)
    {
        SceneManager.LoadScene(selectedLevel);
    }

    public void ResetAndChangeLevel()
    {
        playerManager.ResetPickups();
        isGameOver = false;
        isGameStart = false;
        SceneManager.LoadScene(currentLevel); // resets current level for now
    }
}
