﻿/**
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

    private void Start()
    {
        statusPanel = GameObject.Find("StatusPanel");
        statusPanel.SetActive(false);

        //get text object from the scene
        playerStats = statusPanel.GetComponentInChildren<Text>();
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        currentLevel = SceneManager.GetActiveScene().buildIndex; // you'll need to specify in build setting
    }

    void OnLevelWasLoaded()
    {

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
                           + "PlayerRunSpeed: " + playerManager.pRunSpeed + "\n";                
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





    //public void greyUpgrade( PlayerManager pm ) //may need better naming..
    //{
    //    pm.playerPoints += 10;
    //}    
    //public void orangeUpgrade( PlayerManager pm )
    //{
    //    pm.playerPoints += 5;
    //    pm.eatSpeed += pm.eatSpeed * 0.10f; //increase by 10%
    //}  

    //public void blueUpgrade( PlayerManager pm )
    //{
    //    pm.playerPoints += 5;
    //    pm.pRunSpeed +=  pm.pRunSpeed * 0.10f; //increase by 10%
    //}

    //change the level by calling game manager 



}
