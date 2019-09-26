
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

    private Text playerStats; //will change the text from the UI
    private PlayerManager playerManager;
    private GameObject statusPanel;
    private Button respawnButton;

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
        Toolbox.GetInstance().GetPlayerManager().ResetPlayer();
        ResetGameManager();
    }

    private void Start() // references to scene objects must also be made in ResetGameManager() below
    {
        statusPanel = GameObject.Find("StatusPanel");
        respawnButton = statusPanel.GetComponentInChildren<Button>();
        respawnButton.onClick.AddListener(RespawnPlayer);
        statusPanel.SetActive(false);

        //get text object from the scene
        playerStats = statusPanel.GetComponentInChildren<Text>();
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void ResetGameManager()
    {
        statusPanel = GameObject.Find("StatusPanel");
        playerStats = statusPanel.GetComponentInChildren<Text>();

        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void DisplayScore()
    {
        isGameOver = true;

        playerManager.UpgradeStats();

        statusPanel.SetActive(true);

        playerStats.text = "Level Stats: " + "\n"
                           + "\n"
                           + "Level: " + currentLevel + "\n"
                           + "\n"
                           + "Total Point-bugs Eaten: " + "\n" 
                           + playerManager.pointPickups + "\n"
                           + "Points Aquired: " + "\n" 
                           + playerManager.pPoints + " + " + playerManager.pointsToAdd + "\n"
                           + "\n"
                           + "Total Eat-bugs Eaten: " + "\n" 
                           + playerManager.pointPickups + "\n"
                           + "Eating Speed Aquired: " + "\n" 
                           + playerManager.pEatSpeed + " + " + playerManager.eatSpeedToAdd + "\n"
                           + "\n"
                           + "Total Move-bugs eaten: " + "\n" 
                           + playerManager.pointPickups + "\n"
                           + "Move Speed Aquired: " + "\n" 
                           + playerManager.pMoveSpeed + " + " + playerManager.moveSpeedToAdd + "\n";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    private void RespawnPlayer()
    {
        print("woohoo!");
    }
}
