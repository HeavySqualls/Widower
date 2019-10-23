using UnityEngine;
public class Player_Manager : MonoBehaviour
{
    //static int players = 1;
    public int playerID;

    [Header("Player Stats:")]
    public int playerLevel = 0;
    public float moveSpeed = 8;
    public float runCooldownSeconds = 4;
    public float secondsCanRun = 2;
    public float runSpeed; // ----------- set in start
    public float eatSpeed = 0.5f;
    public float points = 0f; // ------------- used for widow stat check
    public bool isReady = false; // set by player controller - read by Game Manager to know when to start the coutdown
    public bool isRestart = false;
    public bool isWinner = false;

    [Space]
    [Header("Player Upgradable Points:")]
    public float pointsToAdd = 0f;
    public float eatSpeedToAdd = 0f;
    public float moveSpeedToAdd = 0f;
    public string deathCause;

    [Space]
    [Header("Player PickUp:")]
    public int pointPickups;
    public int eatPickups;
    public int movePickups;
    private GameObject currentPlayer;

    [Space]
    [Header("Player References:")]
    public Camera_Controller camController;
    public Player_Controller pCon;
    public Player_UI pUI;
    private WidowController widowController;
    private GameManager gM;

    public void ResetPlayerManager1()
    {
        gM = Toolbox.GetInstance().GetGameManager();

        // Sets up player manager to the proper player controller 
        if (playerID == 1)
            currentPlayer = GameObject.FindGameObjectWithTag("Player1");
        else if (playerID == 2)
            currentPlayer = GameObject.FindGameObjectWithTag("Player2");


        widowController = GameObject.FindGameObjectWithTag("Widow").GetComponent<WidowController>();

        pCon = currentPlayer.GetComponent<Player_Controller>();
        pUI = currentPlayer.GetComponentInChildren<Player_UI>();
        camController = currentPlayer.GetComponentInChildren<Camera_Controller>();

        runSpeed = moveSpeed * 2;
        FreezePlayer();
        isWinner = false;
        isReady = false;

        pCon.runTime = 0;

        pUI.DisableStatPanel();
        pUI.readyPanel.SetActive(false);
    }

    public void DisplayScore()
    {

        pCon.HideBody();
        FreezePlayer();
        pCon.isDead = true;

        WhoKilledPlayer();

        pUI.DisplayStatsPanel();
        

        DisplayCursor();
        pCon.predatorKilledPlayer = false;
    }

    private void WhoKilledPlayer()
    {
        if (!pCon.predatorKilledPlayer)
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
            print("WINNER!");
            isWinner = true;
            gM.EndRound();
        }

        ResetPickups();
    }

    public void FreezePlayer()
    {
        pCon.processMovement = false;
        camController.isCameraMovement = false;
    }

    public void UnFreezePlayer()
    {
        pCon.processMovement = true;
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

