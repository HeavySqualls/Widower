using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelTime = 60;

    public bool isGameOver = false;

    public int 
        gameDeathCount,
        bugEatenOrange,
        bugEatenBlue,
        bugEatenGrey,
        numberOfPlayers; //for knowing if it is multi player


    private Text playerStats; //will change the text from the UI
    private PlayerManager playerManager;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerStatsInfoText").GetComponent<Text>();
        playerManager = Toolbox.GetInstance().GetPlayerManager();
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


        playerStats.text = "Test Stats: " + playerManager.greyPickUps + "\n" 
                           + "nextO: " + playerManager.orangePickUps + "\n" 
                           + "nextB: " + playerManager.bluePickUps;


        // tally & present player pick-ups



    }

}
