using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_1_Manager : MonoBehaviour
{
    [Header("Player Stats:")]
    public float p1_moveSpeed = 8;
    public float p1_runCooldownSeconds = 4;
    public float p1_secondsCanRun = 2;
    public float p1_runSpeed; // ----------- set in start
    public float p1_eatSpeed = 0.5f;
    public float p1_points; // ------------- used for widow stat check
    public bool isReady = false; // set by player controller - read by Game Manager to know when to start the coutdown

    [Space]
    [Header("Player Upgradable Points:")]
    public float p1_pointsToAdd;
    public float p1_eatSpeedToAdd;
    public float p1_moveSpeedToAdd;
    private string deathCause;

    [Space]
    [Header("Player PickUp:")]
    public int p1_pointPickups;
    public int p1_eatPickups;
    public int p1_movePickups;
    private GameObject currentPlayer;

    [Space]
    [Header("Player References:")]
    private Player_1_Controller p1_Controller;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();

        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        p1_Controller = currentPlayer.GetComponent<Player_1_Controller>();
        p1_runSpeed = p1_moveSpeed * 2;

        FreezePlayer();

        p1_Controller.statusPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisplayScore()
    {
        FreezePlayer();

        if (!p1_Controller.predatorKilledPlayer)
        {
            deathCause = "Death By Widow";
            TallyPickups();
        }
        else
        {
            deathCause = "Death By Predator";
            ResetPickups();
        }


        p1_Controller.statusPanel.SetActive(true);

        p1_Controller.level.text = gameManager.currentLevel.ToString();
        p1_Controller.death.text = deathCause;
        p1_Controller.points.text = p1_points + " + " + p1_pointsToAdd;
        p1_Controller.eatSpeed.text = p1_eatSpeed + " + " + p1_eatSpeedToAdd;
        p1_Controller.moveSpeed.text = p1_moveSpeed + " + " + p1_moveSpeedToAdd;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        p1_Controller.predatorKilledPlayer = false;
    }

    public void TallyPickups()
    {
        p1_pointsToAdd = (p1_pointPickups * 10) + (p1_eatPickups * 5) + (p1_movePickups * 5);
        p1_eatSpeedToAdd = (p1_eatPickups * 0.25f);
        p1_moveSpeedToAdd = (p1_movePickups * 0.30f);
    }

    public void ResetPickups()
    {
        p1_pointPickups = 0;
        p1_eatPickups = 0;
        p1_movePickups = 0;

        p1_pointsToAdd = 0;
        p1_eatSpeedToAdd = 0;
        p1_moveSpeedToAdd = 0;
    }

    public void AddPoints()
    {
        p1_points += p1_pointsToAdd;
        p1_eatSpeed += p1_eatSpeedToAdd;
        p1_moveSpeed += p1_moveSpeedToAdd;

        ResetPickups();
    }

    public void FreezePlayer()
    {
        p1_Controller.processMovement = false;
    }

    public void UnFreezePlayer()
    {
        p1_Controller.processMovement = true;
    }
}
