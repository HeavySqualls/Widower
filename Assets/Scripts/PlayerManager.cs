using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats:")]
    public float pMoveSpeed = 8;
    public float runCooldownSeconds = 4;
    public float secondsCanRun = 2;
    public float pRunSpeed; // ----------- set in start
    public float pEatSpeed = 0.5f;
    public float pPoints; // ------------- used for widow stat check

    [Space]
    [Header("Player Upgradable Points:")]
    public float pointsToAdd;
    public float eatSpeedToAdd;
    public float moveSpeedToAdd;

    [Space]
    [Header("Player PickUp:")]
    public int pointPickups;
    public int eatPickups;
    public int movePickups;

    [Space]
    [Header("Player Score:")]
    private Text playerStats; //will change the text from the UI
    private GameObject statusPanel;
    private Button respawnButton;



    [Space]
    [Header("Player References:")]
    private PlayerController playerController;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pMoveSpeed * 2;

        statusPanel = GameObject.Find("StatusPanel");
        respawnButton = statusPanel.GetComponentInChildren<Button>();
        playerStats = statusPanel.GetComponentInChildren<Text>();
        respawnButton.onClick.AddListener(RespawnPlayer);
        statusPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FreezePlayer()
    {
        playerController.processMovement = false;       
    }

    public void UnFreezePlayer()
    {
        playerController.processMovement = true;
    }

    public void DisplayScore()
    {
        gameManager.isGameOver = true;

        UpgradeStats();

        statusPanel.SetActive(true);

        playerStats.text = "Level Stats: " + "\n"
                           + "\n"
                           + "Level: " + gameManager.currentLevel + "\n"
                           + "\n"
                           + "Total Point-bugs Eaten: " + "\n"
                           + pointPickups + "\n"
                           + "Points Aquired: " + "\n"
                           + pPoints + " + " + pointsToAdd + "\n"
                           + "\n"
                           + "Total Eat-bugs Eaten: " + "\n"
                           + pointPickups + "\n"
                           + "Eating Speed Aquired: " + "\n"
                           + pEatSpeed + " + " + eatSpeedToAdd + "\n"
                           + "\n"
                           + "Total Move-bugs eaten: " + "\n"
                           + pointPickups + "\n"
                           + "Move Speed Aquired: " + "\n"
                           + pMoveSpeed + " + " + moveSpeedToAdd + "\n";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpgradeStats()
    {
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

    public void ResetPlayerManager()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pMoveSpeed * 2;

        statusPanel = GameObject.Find("StatusPanel");
        playerStats = statusPanel.GetComponentInChildren<Text>();
    }

    private void RespawnPlayer()
    {
        print("Player Respawned");

        pPoints += pointsToAdd;
        pEatSpeed += eatSpeedToAdd;
        pMoveSpeed += moveSpeedToAdd;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Instantiate Player
    }
}
