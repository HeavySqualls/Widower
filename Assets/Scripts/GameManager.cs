using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float levelTime = 60;

    public bool isGameOver = false;

    public int gameScore,
        gameDeathCount,
        gameBugEaten,
        numberOfplayers; //for knowing if it is multi player

    private void Update()
    {
        if (isGameOver)
        {
            Toolbox.GetInstance().GetPlayerManager().FreezePlayer();
            DisplayScore();
        }
    }

    public void DisplayScore()
    {
        // tally & present player pick-ups
    }

}
