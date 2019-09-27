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

        TallyPickups();

        p1_Controller.statusPanel.SetActive(true);

        p1_Controller.playerStats.text = "Level Stats: " + "\n"
                           + "\n"
                           + "Level: " + gameManager.currentLevel + "\n"
                           + "\n"
                           + "Total Point-bugs Eaten: " + "\n"
                           + p1_pointPickups + "\n"
                           + "Points Aquired: " + "\n"
                           + p1_points + " + " + p1_pointsToAdd + "\n"
                           + "\n"
                           + "Total Eat-bugs Eaten: " + "\n"
                           + p1_pointPickups + "\n"
                           + "Eating Speed Aquired: " + "\n"
                           + p1_eatSpeed + " + " + p1_eatSpeedToAdd + "\n"
                           + "\n"
                           + "Total Move-bugs eaten: " + "\n"
                           + p1_pointPickups + "\n"
                           + "Move Speed Aquired: " + "\n"
                           + p1_moveSpeed + " + " + p1_moveSpeedToAdd + "\n";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
