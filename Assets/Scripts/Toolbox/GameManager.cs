
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public float countInTime = 3;

    public bool isGameOver = false;
    public bool isGameStart = false;

    public int gameDeathCount; // not used at the moment
    public int numberOfPlayers; //for knowing if it is multi player
    public int currentLevel;

    private Player_1_Manager p1_Manager;
    private Player_2_Manager p2_Manager;

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
        //Toolbox.GetInstance().GetTimer().ResetTimer();
        //Toolbox.GetInstance().GetPlayer_1_Manager().ResetPlayerManager();
        ResetGameManager();
    }

    private void Start() // references to scene objects must also be made in ResetGameManager() below
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();

        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        StartGame();
    }

    private void ResetGameManager()
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void changeLevel(int selectedLevel)
    {
        SceneManager.LoadScene(selectedLevel);
    }

    public void ResetAndChangeLevel()
    {
        p1_Manager.ResetPickups();
        isGameOver = false;
        isGameStart = false;
        SceneManager.LoadScene(currentLevel); // resets current level for now
    }

    private void StartGame()
    {
        if (!isGameStart && p1_Manager.isReady) //TODO: && p2_Manager.isReady)
        {
            Toolbox.GetInstance().GetTimeManager().StartCountDownTimer(countInTime);
            isGameStart = true;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        print("Game Over");
    }
}
