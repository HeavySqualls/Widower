using System.Collections;
using UnityEngine;
using UnityEditor;

public class Player_Controller : MonoBehaviour
{
    [Header("Player Status:")]
    public bool processMovement = false;
    public bool processInputs = true;
    public bool isRunning = false;
    public bool canRun = true;
    public bool isInteracting = false;
    public bool isEating = false;
    public bool isDead = false;
    public bool predatorKilledPlayer = false;

    [Space]
    [Header("Player States:")]
    public float runTime;
    private float pCurrentMoveSpeed;

    [Space]
    [Header("Control Options:")]
    public bool isGamePad = false;
    public bool isPlayer1 = true;

    [Space]
    [Header("Hook Shot:")]
    public float reachingTime = 0.5f;
    public bool attachedToTarget;
    public Transform shootPosition;
    private GameObject currentShot;
    private GameObject projectilePrefab;
    private ReachTargetInTime reachTargetInTime;

    [Space]
    [Header("Player Spawn:")]
    private float spawnDelay = 3f;
    public GameObject playerCamera;
    public Transform playerCamAnchor;
    private Transform cameraAnchor;
    private Transform playerSpawnAnchor;

    [Space]
    [Header("Player Refrences:")]
    public PickupController interactedController;
    public GameObject playerModel;
    private GameManager gameManager;
    private Player_UI pUI;

    private Player_1_Manager p1_Manager;
    private Player_2_Manager p2_Manager;
    public dynamic playerManager;
    public ControlProfile controlProfile;

    private void Awake()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();

        //projectilePrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/GrapplingHook/Projectile.prefab");
        projectilePrefab = Resources.Load<GameObject>("Resources/Projectile");

        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();

        controlProfile = this.gameObject.AddComponent<ControlProfile>();
        pUI = GetComponentInChildren<Player_UI>();

        if (isPlayer1)
        {
            // have a variable that is assigned to the player 1 manager 
            playerManager = p1_Manager;

            // set controller profile to player 1
            controlProfile.ControlProfile1();
        }
        else // is player 2
        {
            // have a variable that is assigned to the player 2 manager
            playerManager = p2_Manager;

            // set controller profile to player 1
            controlProfile.ControlProfile2();
        }
    }

    void Start()
    {
        pCurrentMoveSpeed = playerManager.moveSpeed;
        pUI.DisableStaminaBar();

        playerManager.isRestart = false;

        cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchorPoint").transform;
        playerSpawnAnchor = GameObject.FindGameObjectWithTag("Player1_SpawnPoint").transform;
    }

    void Update()
    {
        PlayerMovement();
        PlayerInputs();
    }

    void PlayerInputs()
    {
        if (processInputs)
        {
            if (isGamePad)
            {
                if (Input.GetButtonDown(controlProfile.Gamepad_Start) && gameManager.isGameStart == false)
                {
                    playerManager.isReady = true;
                    pUI.readyPanel.SetActive(true);
                }

                if (Input.GetButtonDown(controlProfile.X_Button) && isInteracting && interactedController != null && !isEating)
                {
                    isEating = true;
                    interactedController.GoGettingEaten(playerManager.eatSpeed);
                }

                if (Input.GetButtonDown(controlProfile.O_Button) && isInteracting && interactedController != null && isEating)
                {
                    isEating = false;
                    interactedController.StopGettingEaten();
                    interactedController = null;
                    isInteracting = false;
                }

                if (Input.GetButtonDown(controlProfile.Respawn_Gamepad) && pUI.statPanelActive == true)
                {
                    PlayerRespawn();
                }

                // Shoot projectile when button pressed down
                if (Input.GetButtonDown(controlProfile.Hookshot_Gamepad) && currentShot == null)
                {
                    currentShot = Instantiate(projectilePrefab, shootPosition);
                    currentShot.GetComponent<Projectile>().player = this.gameObject;
                }

                // Go to projectile when button raised up
                if (Input.GetButtonUp(controlProfile.Hookshot_Gamepad) && currentShot && currentShot.GetComponent<Projectile>().isCollided && !reachTargetInTime)
                {
                    reachTargetInTime = this.gameObject.AddComponent<ReachTargetInTime>();

                    reachTargetInTime.SetInitialValue(currentShot.transform, reachingTime, attachedToTarget);
                }
            }
            else
            {
                if (Input.GetKeyUp(controlProfile.Enter_Key) && gameManager.isGameStart == false)
                {
                    playerManager.isReady = true;
                    pUI.readyPanel.SetActive(true);
                }

                if (Input.GetKeyUp(controlProfile.Eat_Key) && isInteracting && interactedController != null && !isEating)
                {
                    isEating = true;
                    interactedController.GoGettingEaten(playerManager.eatSpeed);
                }

                if (Input.GetKeyUp(controlProfile.QuitEat_Key) && isInteracting && interactedController != null && isEating)
                {
                    isEating = false;

                    interactedController.StopGettingEaten();
                    interactedController = null;
                }

                // Shoot projectile when button pressed down
                if (Input.GetKeyUp(controlProfile.Respawn_Key) && pUI.statPanelActive == true)
                {
                    PlayerRespawn();
                }

                if (Input.GetKeyDown(controlProfile.Hookshot_Key) && currentShot == null)
                {
                    currentShot = Instantiate(projectilePrefab, shootPosition);
                    currentShot.GetComponent<Projectile>().player = this.gameObject;
                }

                // go to projectile when button raised up
                if (Input.GetKeyUp(controlProfile.Hookshot_Key) && currentShot.GetComponent<Projectile>().isCollided && !reachTargetInTime && currentShot != null)
                {
                    reachTargetInTime = this.gameObject.AddComponent<ReachTargetInTime>();
                    reachTargetInTime.SetInitialValue(currentShot.transform, reachingTime, attachedToTarget);
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
                float hor = Input.GetAxis(controlProfile.Horizontal_Gamepad);
                float vert = Input.GetAxis(controlProfile.Vertical_Gamepad);
                Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * pCurrentMoveSpeed * Time.deltaTime;
                transform.Translate(playerMovement, Space.Self);

                // Start Running
                if (Input.GetButton(controlProfile.Sprint_Gamepad) && pCurrentMoveSpeed != 0 && canRun == true)
                {
                    pCurrentMoveSpeed = playerManager.runSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    pUI.EnableStaminaBar();
                }

                // Stop Running Case - Player let go of key
                if (Input.GetButtonUp(controlProfile.Sprint_Gamepad))
                {
                    pCurrentMoveSpeed = playerManager.moveSpeed;
                    pUI.runWindDown = true;
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > playerManager.secondsCanRun)
                {
                    pCurrentMoveSpeed = playerManager.moveSpeed;
                    runTime = 0f;
                    isRunning = false;
                    canRun = false;
                    pUI.PlayerRunningCoolDown();
                }
            }
            else
            {
                float hor = Input.GetAxis(controlProfile.Horizontal);
                float vert = Input.GetAxis(controlProfile.Vertical);
                Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * pCurrentMoveSpeed * Time.deltaTime;
                transform.Translate(playerMovement, Space.Self);

                // Start Running
                if (Input.GetKey(controlProfile.Sprint_Key) && pCurrentMoveSpeed != 0 && canRun == true)
                {
                    pCurrentMoveSpeed = playerManager.runSpeed;
                    runTime += 1 / (1 / Time.deltaTime);
                    isRunning = true;
                    pUI.EnableStaminaBar();
                }

                // Stop Running Case - Player let go of key
                if (Input.GetKeyUp(controlProfile.Sprint_Key))
                {
                    pCurrentMoveSpeed = playerManager.moveSpeed;
                    pUI.runWindDown = true;
                    isRunning = false;
                }

                // Stop Running Case - Player ran out of stamina
                if (isRunning == true && runTime > playerManager.secondsCanRun)
                {
                    pCurrentMoveSpeed = playerManager.moveSpeed;
                    runTime = 0f;
                    isRunning = false;
                    canRun = false;
                    pUI.PlayerRunningCoolDown();
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
    
    // RESPAWN

    public void PlayerRespawn()
    {
        if (interactedController != null)
            interactedController.interactObjInRange = false;

        playerManager.AddPoints();

        StartCoroutine(ISpawnCooldown());
    }

    public IEnumerator ISpawnCooldown()
    {
        // Hide the player model & respawn button
        pCurrentMoveSpeed = playerManager.moveSpeed;
        playerModel.SetActive(false);
        isInteracting = false;
        isEating = false;

        pUI.respawnButton.gameObject.SetActive(false);
        pUI.DisableStaminaBar();

        // Move player camera to overhead camera anchor
        gameObject.transform.position = playerSpawnAnchor.transform.position;
        gameObject.transform.rotation = playerSpawnAnchor.transform.rotation;

        // Move player game object to spawn point 
        playerCamera.transform.parent = cameraAnchor.transform;
        playerCamera.transform.position = cameraAnchor.position;
        playerCamera.transform.rotation = cameraAnchor.rotation;

        yield return new WaitForSeconds(spawnDelay);

        // Move player camera back to player 
        playerCamera.transform.parent = playerCamAnchor;
        playerCamera.transform.position = playerCamAnchor.position;
        playerCamera.transform.rotation = playerCamAnchor.rotation;

        // Enable model & disable stats panel
        playerModel.SetActive(true);
        pUI.DisableStatPanel();

        // Give control back to player
        playerManager.UnFreezePlayer();
        isDead = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
