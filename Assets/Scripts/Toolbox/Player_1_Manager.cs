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

    [Space]
    [Header("Player Score:")]
    private GameObject p1_statusPanel;
    private Text playerStats;
    private Button respawnButton;

    [Space]
    [Header("Player Spawn:")]
    private Transform p1_spawnPoint;

    [Space]
    [Header("Player References:")]
    private Player_1_Controller p1_Controller;
    private GameManager gameManager;
    private GameObject p1_objectPrefab;

    private void Start()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();

        SetUpPlayerInManager();

        FreezePlayer();

        p1_statusPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetUpPlayerInManager()
    {
        // Player Controller
        p1_Controller = GameObject.Find("Player_1-Prefab").GetComponent<Player_1_Controller>();
        p1_runSpeed = p1_moveSpeed * 2;

        // Player Status Panel
        p1_statusPanel = GameObject.Find("StatusPanel");
        playerStats = p1_statusPanel.GetComponentInChildren<Text>();

        // Player Re-spawn Point
        p1_spawnPoint = GameObject.FindGameObjectWithTag("Player1_SpawnPoint").transform;
        respawnButton = p1_statusPanel.GetComponentInChildren<Button>();
        respawnButton.onClick.AddListener(RespawnPlayer);
    }

    public void FreezePlayer()
    {
        p1_Controller.processMovement = false;       
    }

    public void UnFreezePlayer()
    {
        p1_Controller.processMovement = true;
    }

    public void DisplayScore()
    {
        gameManager.isGameOver = true;

        FreezePlayer();

        UpgradeStats();

        p1_statusPanel.SetActive(true);

        playerStats.text = "Level Stats: " + "\n"
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

    public void UpgradeStats()
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

    public void ResetPlayerManager()
    {
        p1_Controller = GameObject.Find("Player_1-Prefab").GetComponent<Player_1_Controller>();
        p1_runSpeed = p1_moveSpeed * 2;

        p1_statusPanel = GameObject.Find("StatusPanel");
        playerStats = p1_statusPanel.GetComponentInChildren<Text>();
    }

    private void RespawnPlayer()
    {
        print("Player 1 Respawned");

        p1_points += p1_pointsToAdd;
        p1_eatSpeed += p1_eatSpeedToAdd;
        p1_moveSpeed += p1_moveSpeedToAdd;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        p1_Controller.DestroyInstance_p1();

        p1_objectPrefab = Resources.Load<GameObject>("Player_1-Prefab");

        Instantiate(p1_objectPrefab, p1_spawnPoint.position, p1_spawnPoint.rotation);
        ResetPlayerManager();

        p1_statusPanel.SetActive(false);

        UnFreezePlayer();
    }
}
