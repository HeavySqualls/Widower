using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Player States:")]
    public bool processMovement = true;
    public bool isRunning = false;
    public bool canRun = true;
    public bool isInteracting = false;
    private bool runWindDown = false;

    public PickupController interactedController;

    [SerializeField]
    private float runTime;

    [SerializeField]
    private float pCurrentMoveSpeed;

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
        //TODO: Move to GameManager
        if (Input.GetKeyUp(KeyCode.Return))
        {
            timeManager.StartCountDownTimer(gameManager.levelTime);
            Debug.Log("Time has started!");
        }

        if (Input.GetKey(KeyCode.E) && isInteracting)
        {
            Debug.Log("eat!");
            interactedController.StartCountdownTimer(playerManager.eatSpeed);
        }
    }

    void PlayerMovement()
    {
        if (processMovement)
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



    // ---- RUNNING METHODS ---- //

    void PlayerRunningCoolDown()
    {
        StartCoroutine(IRunningCooldown());
    }

    void RunWindDown()
    {
        if (runTime > 0 && runWindDown)
        {
            runTime -= 1 * Time.deltaTime;

            if (runTime <= 0 || isRunning)
            {
                runWindDown = false;
            }

            if (runTime <= 0)
            {
                runWindDown = false;
                runTime = 0;
            }
        }
    }

    public IEnumerator IRunningCooldown()
    {
        canRun = false;
        yield return new WaitForSeconds(Toolbox.GetInstance().GetPlayerManager().runCooldownSeconds);
        canRun = true;
        Debug.Log("Running Ready!");
    }
}
