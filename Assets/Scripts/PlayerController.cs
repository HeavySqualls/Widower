using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Player States:")]
    public bool processMovement = true;
    public bool isRunning = false;
    public bool canRun = true;
    public bool isInteracting = false;
    public bool isEating = false;
    [SerializeField] private float runTime;
    [SerializeField] private float pCurrentMoveSpeed;
    private bool runWindDown = false;


    [Space]
    [Header("Control Options:")]
    public bool isGamePad = false;


    [Space]
    [Header("Player Refrences:")]
    public Canvas CanvasObj;
    public Image staminaBar;

    public PickupController interactedController;
    private PlayerManager playerManager;
    private GameManager gameManager;
    private TimeManager timeManager;


    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        gameManager = Toolbox.GetInstance().GetGameManager();
        timeManager = Toolbox.GetInstance().GetTimer();
    }

    void Start()
    {
        processMovement = false;
        staminaBar.enabled = false;
        pCurrentMoveSpeed = playerManager.pWalkSpeed;
    }

    void Update()
    {
        PlayerMovement();
        PlayerInputs();
        RunWindDown();
    }

    public void InteractableObject(PickupController interObject)
    {
        interactedController = interObject;

        if (interObject != null)
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }

    void PlayerInputs()
    {
        if (isGamePad)
        {
            if (Input.GetButtonDown("STARTButton"))
            {
                processMovement = true;
                timeManager.StartCountDownTimer(gameManager.levelTime);
                Debug.Log("Time has started!");
            }

            if (Input.GetButtonDown("XButton") && isInteracting && interactedController != null && !isEating)
            {
                Debug.Log("eat!");
                isEating = true;
                interactedController.StartEatCountdownTimer(playerManager.eatSpeed);
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Return) && gameManager.isGameOver == true)
            {
                gameManager.ResetAndChangeLevel();
            }
            else if (Input.GetKeyUp(KeyCode.Return) && gameManager.isGameStart == false)
            {
                processMovement = true;
                timeManager.StartCountDownTimer(gameManager.levelTime);
                //Debug.Log("Time has started!");
                //timeManager.ResetTimer(); <<< ----- Cannot have this here!!
                gameManager.isGameStart = true;
            }

            if (Input.GetKey(KeyCode.E) && isInteracting && interactedController != null && !isEating)
            {
                Debug.Log("eat!");
                isEating = true;
                interactedController.StartEatCountdownTimer(playerManager.eatSpeed);
            }
        }
    }

    void PlayerMovement()
    {
        if (processMovement)
        {
            if (isGamePad)
            {
                float hor = Input.GetAxis("Horizontal_Gamepad");
                float vert = Input.GetAxis("Vertical_Gamepad");
                Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * pCurrentMoveSpeed * Time.deltaTime;
                transform.Translate(playerMovement, Space.Self);

                // Start Running
                if (Input.GetButton("Sprint_Gamepad") && pCurrentMoveSpeed != 0 && canRun == true)
                {
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pRunSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    staminaBar.enabled = true;
                    staminaBar.fillAmount = (runTime / 2);
                }

                // Stop Running Case - Player let go of key
                if (Input.GetButtonUp("Sprint_Gamepad"))
                {
                    Debug.Log("Player State: Stopped Running");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pWalkSpeed;
                    runWindDown = true;
                    Debug.Log("Run wind down!");
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > Toolbox.GetInstance().GetPlayerManager().secondsCanRun)
                {
                    Debug.Log("Player State: Out of stamina");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pWalkSpeed;
                    runTime = 0f;
                    isRunning = false;
                    canRun = false;
                    PlayerRunningCoolDown();
                }
            }
            else
            {
                float hor = Input.GetAxis("Horizontal");
                float vert = Input.GetAxis("Vertical");
                Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * pCurrentMoveSpeed * Time.deltaTime;
                transform.Translate(playerMovement, Space.Self);

                // Start Running
                if (Input.GetKey(KeyCode.LeftShift) && pCurrentMoveSpeed != 0 && canRun == true)
                {
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pRunSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    staminaBar.enabled = true;
                    staminaBar.fillAmount = (runTime / 2);
                }

                // Stop Running Case - Player let go of key
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Debug.Log("Player State: Stopped Running");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pWalkSpeed;
                    runWindDown = true;
                    Debug.Log("Run wind down!");
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > Toolbox.GetInstance().GetPlayerManager().secondsCanRun)
                {
                    Debug.Log("Player State: Out of stamina");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayerManager().pWalkSpeed;
                    runTime = 0f;
                    isRunning = false;
                    canRun = false;
                    PlayerRunningCoolDown();
                }
            }
        }      
    }

    // ---- RUNNING METHODS ---- //

    void PlayerRunningCoolDown()
    {
        staminaBar.GetComponent<Image>().color = Color.white;
        StartCoroutine(IRunningCooldown());
    }

    void RunWindDown()
    {
        if (runTime > 0 && runWindDown)
        {
            runTime -= 1 * Time.deltaTime;
            staminaBar.fillAmount = (runTime / 2);

            if (runTime <= 0 || isRunning)
            {
                staminaBar.enabled = false;
                runWindDown = false;
            }

            if (runTime <= 0)
            {
                runWindDown = false;
                runTime = 0;
                staminaBar.enabled = false;
            }
        }
    }

    public IEnumerator IRunningCooldown()
    {
        canRun = false;
        yield return new WaitForSeconds(Toolbox.GetInstance().GetPlayerManager().runCooldownSeconds);
        canRun = true;
        staminaBar.GetComponent<Image>().color = Color.yellow;
        staminaBar.enabled = false;
        Debug.Log("Running Ready!");
    }
}
