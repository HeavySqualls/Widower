using UnityEngine;

public class Player_2_Manager : MonoBehaviour
{
    [Header("Player Stats:")]
    private int playerLevel = 0;
    public float moveSpeed = 8;
    public float runCooldownSeconds = 4;
    public float secondsCanRun = 2;
    public float runSpeed; // ----------- set in start
    public float eatSpeed = 0.5f;
    public float points = 0; // ------------- used for widow stat check
    public bool isReady = false; // set by player controller - read by Game Manager to know when to start the coutdown
    public bool isRestart = false;
    public bool isWinner = false;

    [Space]
    [Header("Player Upgradable Points:")]
    public float pointsToAdd = 0f;
    public float eatSpeedToAdd = 0f;
    public float moveSpeedToAdd = 0f;
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
    public Player_UI pUI;
    private WidowController widowController;
    private GameManager gM;

    private void Start()
    {
        // References and set up is set up in ResetPlayerManager2() and is called by the GameManager 
    }

    public void ResetPlayerManager2()
    {
        gM = Toolbox.GetInstance().GetGameManager();
        widowController = GameObject.FindGameObjectWithTag("Widow").GetComponent<WidowController>();

        currentPlayer = GameObject.FindGameObjectWithTag("Player2");
        pController = currentPlayer.GetComponent<Player_Controller>();
        pUI = currentPlayer.GetComponentInChildren<Player_UI>();
        camController = currentPlayer.GetComponentInChildren<Camera_Controller>();

        runSpeed = moveSpeed * 2;
        FreezePlayer();
        isWinner = false;
        isReady = false;
        pController.runTime = 0;

        pUI.DisableStatPanel();
        pUI.readyPanel.SetActive(false);
    }

    public void DisplayScore()
    {
        FreezePlayer();

        // Determine who killed player
        WhoKilledPlayer();

        pUI.EnableStatPanel();

        if (gM.isGameOver)
        {
            pUI.respawnButtonObject.SetActive(false);
            pUI.restartButtonObject.SetActive(true);
        }

        pUI.level.text = playerLevel.ToString();
        pUI.death.text = deathCause;
        pUI.points.text = points + " + " + pointsToAdd;
        pUI.eatSpeed.text = eatSpeed + " + " + eatSpeedToAdd;
        pUI.moveSpeed.text = moveSpeed + " + " + moveSpeedToAdd;

        DisplayCursor();
        pController.predatorKilledPlayer = false;
    }

    private void WhoKilledPlayer()
    {
        if (!pController.predatorKilledPlayer)
        {
            deathCause = "Death By Widow";
            TallyPickups();
        }
        else
        {
            deathCause = "Death By Predator";
            ResetPickups();
        }
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

        if (points >= widowController.scoreToBeat)
        {
            print("Player 2 wins!");
            isWinner = true;
            gM.EndRound();
        }

        ResetPickups();
    }

    public void FreezePlayer()
    {
        pController.isDead = true;
        pController.processMovement = false;
        camController.isCameraMovement = false;
    }

    public void UnFreezePlayer()
    {
        pController.isDead = false;
        pController.processMovement = true;
        camController.isCameraMovement = true;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisplayCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

