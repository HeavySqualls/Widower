/**
 *                                    GAME_MANAGER TASK
 * 
- [x]  Present end screen (enable end screen canvas) with breakdown of players round
    - [x]  how many bugs and what kind (Color bugs for now)
    - [x]  what level played
    - [x]  display player stats and the amount they are increasing for next round
- [x]  Give instructions to player manager to upgrade player stats
- [x]  Handle game reset with level change in mind
    - [x]  level change would look like GM giving the instruction to SceneManager to change to "x" level
 */

/**
 *
 * 
 * - Grey pick ups rewards the player with 10 points per unit
 * - Orange pick ups rewards the player with 5 points + 10% eating speed per unit
 * - Blue pick ups rewards the player with 5 points + 10% running speed per unit
 * 
 * at the moment "running speed" is walking speed *2, so we could either increase walking speed by 10% 
 * or have running speed be its own defined variable.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public float levelTime = 20;

    public bool isGameOver = false;
    public bool isGameStart = false;

    public int 
            gameDeathCount, // not used at the moment
            numberOfPlayers,//for knowing if it is multi player
            currentLevel; 
    

    private Text playerStats; //will change the text from the UI
    private PlayerManager playerManager;
    private GameObject statusPanel;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon 
        //as this script is enabled.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Toolbox.GetInstance().GetTimer().ResetTimer();
        Toolbox.GetInstance().GetPlayerManager().ResetPlayer();
        ResetGameManager();
    }

    private void Start()
    {
        statusPanel = GameObject.Find("StatusPanel");
        statusPanel.SetActive(false);

        //get text object from the scene
        playerStats = statusPanel.GetComponentInChildren<Text>();
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex; // you'll need to specify in build setting
    }
    
    public void DisplayScore()
    {
        isGameOver = true;

        playerManager.UpgradeStats();

        statusPanel.SetActive(true);
        
        playerStats.text = "Level Stats: "+ "\n"
                           + "\n"
                           + "Level: " + currentLevel +"\n"
                           + "\n"
                           + "GreyPickUp: " + playerManager.greyPickUps + "\n"
                           + "\n"
                           + "OrangePickUp: " + playerManager.orangePickUps + "\n"
                           + "\n"
                           + "BluePickUp: " + playerManager.bluePickUps + "\n"
                           + "\n"
                           + "PlayerTotalPoints: " + playerManager.playerPoints + "\n"
                           + "\n"
                           + "PlayerEatSpeed: " + playerManager.eatSpeed + "\n"
                           + "\n"
                           + "PlayerRunSpeed: " + playerManager.pWalkSpeed + "\n";                
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

    private void ResetGameManager()
    {
        statusPanel = GameObject.Find("StatusPanel");
        playerStats = statusPanel.GetComponentInChildren<Text>();

        //get text object from the scene

        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex; // you'll need to specify in build setting
    }
}
