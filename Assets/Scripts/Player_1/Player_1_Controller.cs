﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_1_Controller : MonoBehaviour
{
    [Space]
    [Header("Player Status:")]
    public bool processMovement = false;
    public bool processInputs = true;

    public bool isRunning = false;
    public bool canRun = true;
    public bool isInteracting = false;
    public bool isEating = false;
    private bool runWindDown = false;

    [Space]
    [Header("Player States:")]
    private float runTime;
    private float pCurrentMoveSpeed;

    [Space]
    [Header("Control Options:")]
    public bool isGamePad = false;

    [Space]
    [Header("Player Refrences:")]
    public Canvas CanvasObj;
    public Image staminaBar;
    public PickupController interactedController;
    private Player_1_Manager p1_Manager;
    private GameManager gameManager;
    private TimeManager timeManager;

    private void Awake()
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        gameManager = Toolbox.GetInstance().GetGameManager();
        //timeManager = Toolbox.GetInstance().GetTimer();
    }

    void Start()
    {
        staminaBar.enabled = false;
        pCurrentMoveSpeed = p1_Manager.p1_moveSpeed;
    }

    void Update()
    {
        PlayerMovement();
        PlayerInputs();
        RunWindDown();
    }

    void PlayerInputs()
    {
        if (processInputs)
        {
            if (isGamePad)
            {
                if (Input.GetButtonDown("STARTButton") && gameManager.isGameStart == false)
                {
                    p1_Manager.isReady = true;
                }

                if (Input.GetButtonDown("XButton") && isInteracting && interactedController != null && !isEating)
                {
                    Debug.Log("eat!");
                    isEating = true;
                    interactedController.StartEatCountdownTimer(p1_Manager.p1_eatSpeed);
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Return) && gameManager.isGameStart == false)
                {
                    p1_Manager.isReady = true;
                }

                if (Input.GetKey(KeyCode.E) && isInteracting && interactedController != null && !isEating)
                {
                    Debug.Log("eat!");
                    isEating = true;
                    interactedController.StartEatCountdownTimer(p1_Manager.p1_eatSpeed);
                }
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
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_runSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    staminaBar.enabled = true;
                    staminaBar.fillAmount = (runTime / 2);
                }

                // Stop Running Case - Player let go of key
                if (Input.GetButtonUp("Sprint_Gamepad"))
                {
                    Debug.Log("Player State: Stopped Running");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_moveSpeed;
                    runWindDown = true;
                    Debug.Log("Run wind down!");
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > Toolbox.GetInstance().GetPlayer_1_Manager().p1_secondsCanRun)
                {
                    Debug.Log("Player State: Out of stamina");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_moveSpeed;
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
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_runSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    staminaBar.enabled = true;
                    staminaBar.fillAmount = (runTime / 2);
                }

                // Stop Running Case - Player let go of key
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Debug.Log("Player State: Stopped Running");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_moveSpeed;
                    runWindDown = true;
                    Debug.Log("Run wind down!");
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > Toolbox.GetInstance().GetPlayer_1_Manager().p1_secondsCanRun)
                {
                    Debug.Log("Player State: Out of stamina");
                    pCurrentMoveSpeed = Toolbox.GetInstance().GetPlayer_1_Manager().p1_moveSpeed;
                    runTime = 0f;
                    isRunning = false;
                    canRun = false;
                    PlayerRunningCoolDown();
                }
            }
        }      
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
        yield return new WaitForSeconds(Toolbox.GetInstance().GetPlayer_1_Manager().p1_runCooldownSeconds);
        canRun = true;
        staminaBar.GetComponent<Image>().color = Color.yellow;
        staminaBar.enabled = false;
        Debug.Log("Running Ready!");
    }

    public void DestroyInstance_p1()
    {
        if (interactedController != null)
            interactedController.interactObjInRange = false;

        Destroy(gameObject);
    }
}
