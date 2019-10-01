using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_2_Manager : MonoBehaviour
{
    [Header("Player Stats:")]
    private int playerLevel = 0;
    public float moveSpeed = 8;
    public float runCooldownSeconds = 4;
    public float secondsCanRun = 2;
    public float runSpeed; // ----------- set in start
    public float eatSpeed = 0.5f;
    public float points; // ------------- used for widow stat check
    public bool isReady = false; // set by player controller - read by Game Manager to know when to start the coutdown

    [Space]
    [Header("Player Upgradable Points:")]
    public float pointsToAdd;
    public float eatSpeedToAdd;
    public float moveSpeedToAdd;
    private string deathCause;

    [Space]
    [Header("Player PickUp:")]
    public int pointPickups;
    public int eatPickups;
    public int movePickups;
    private GameObject currentPlayer;

    [Space]
    [Header("Player References:")]
    public Camera_Controller camController;
    public Player_Controller pController;
    private WidowController widowController;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();
        widowController = GameObject.FindGameObjectWithTag("Widow").GetComponent<WidowController>();

        currentPlayer = GameObject.FindGameObjectWithTag("Player2");
        pController = currentPlayer.GetComponent<Player_Controller>();
        camController = currentPlayer.GetComponentInChildren<Camera_Controller>();
        runSpeed = moveSpeed * 2;

        FreezePlayer();

        pController.statusPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisplayScore()
    {
        FreezePlayer();

        if (!pController.predatorKilledPlayer)
        {
            deathCause = "Death By Widow";
            TallyPickups();

            if ((points + pointsToAdd) >= widowController.scoreToBeat)
            {
                print("Player 1 wins!");
                gameManager.EndGame();
            }
        }
        else
        {
            deathCause = "Death By Predator";
            ResetPickups();
        }


        pController.statusPanel.SetActive(true);

        pController.level.text = playerLevel.ToString();
        pController.death.text = deathCause;
        pController.points.text = points + " + " + pointsToAdd;
        pController.eatSpeed.text = eatSpeed + " + " + eatSpeedToAdd;
        pController.moveSpeed.text = moveSpeed + " + " + moveSpeedToAdd;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pController.predatorKilledPlayer = false;
    }

    public void TallyPickups()
    {
        playerLevel += 1;
        pointsToAdd = (pointPickups * 10) + (eatPickups * 5) + (movePickups * 5);
        eatSpeedToAdd = (eatPickups * 0.25f);
        moveSpeedToAdd = (movePickups * 0.30f);
    }

    public void ResetPickups()
    {
        pointPickups = 0;
        eatPickups = 0;
        movePickups = 0;

        pointsToAdd = 0;
        eatSpeedToAdd = 0;
        moveSpeedToAdd = 0;
    }

    public void AddPoints()
    {
        points += pointsToAdd;
        eatSpeed += eatSpeedToAdd;
        moveSpeed += moveSpeedToAdd;

        ResetPickups();
    }

    public void FreezePlayer()
    {
        pController.processMovement = false;
        camController.isCameraMovement = false;
    }

    public void UnFreezePlayer()
    {
        pController.processMovement = true;
        camController.isCameraMovement = true;
    }
}

