using System.Collections;
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

    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
    }

    private void Start()
    {
        //get text object from the scene
        playerStats = GameObject.Find("PlayerStatsInfoText").GetComponent<Text>();
        
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
                           + "level: " + currentLevel +"\n"
                           + "nextG: " + playerManager.greyPickUps + "\n" 
                           + "nextO: " + playerManager.orangePickUps + "\n" 
                           + "nextB: " + playerManager.bluePickUps;
        
        //todo: 

        // tally & present player pick-up
    }

    private void upgradePlayerStats( PlayerManager pm ) //TODO: why are the Stats to be updated
    {
        //pm.
    }



}
