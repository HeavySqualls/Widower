﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelTime = 60;

    public bool isGameOver = false;

    public int 
        gameDeathCount,
        numberOfPlayers,//for knowing if it is multi player
        currentLevel; 
    

    private Text playerStats; //will change the text from the UI
    private PlayerManager playerManager;

    private void Start()
    {
        //get text object from the scene
        playerStats = GameObject.Find("PlayerStatsInfoText").GetComponent<Text>();
        
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        
        currentLevel = SceneManager.GetActiveScene().buildIndex; // you'll need to specify in build setting
    }


    private void Update()
    {
        if (isGameOver)
        {
            Toolbox.GetInstance().GetPlayerManager().FreezePlayer();
            DisplayScore();
            
        }
    }

    private void DisplayScore()
    {


        playerStats.text = "Test Stats: "+ "\n"
                           + "level:" + currentLevel +"\n"
                           + "nextG: " + playerManager.greyPickUps + "\n" 
                           + "nextO: " + playerManager.orangePickUps + "\n" 
                           + "nextB: " + playerManager.bluePickUps;


        // tally & present player pick-ups



    }

}
