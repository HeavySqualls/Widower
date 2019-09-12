using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [Space]
    [Header("Player States:")]
    public bool processInputs = true;
    public bool isRunning = true;
    public bool canRun = true;

    [Space]
    [Header("Player Movement:")]
    public float secondsCanRun;
    public float runCooldownSeconds;
    public float pRunSpeed;
    public float pMoveSpeedStart;
    public float speedReduction;

    [SerializeField]
    private float runTime;
    [SerializeField]
    private float pCurrentMoveSpeed;


    void Start()
    {
        pCurrentMoveSpeed = pMoveSpeedStart;
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (processInputs)
        {
            float hor = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * pCurrentMoveSpeed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);

            // Start Running
            if (Input.GetKey(KeyCode.LeftShift) && pCurrentMoveSpeed != 0 && canRun == true)
            {
                pCurrentMoveSpeed = 1.0f;
                //Debug.Log("Player State: Running");
                pCurrentMoveSpeed = pRunSpeed;
                runTime += 1 / (1 / Time.deltaTime);
                isRunning = true;
            }

            // Stop Running Case - Player let go of key
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("Player State: Stopped Running");
                pCurrentMoveSpeed = pMoveSpeedStart;
                runTime = 0f;
                isRunning = false;
            }

            // Stop Running Case - Player ran out of stamina
            if (isRunning == true && runTime > secondsCanRun)
            {
                Debug.Log("Player State: Out of stamina");
                pCurrentMoveSpeed = pMoveSpeedStart;
                runTime = 0f;
                isRunning = false;
                PlayerRunningCoolDown();
            }
        }       
    }

    void PlayerRunningCoolDown()
    {
        StartCoroutine(IRunningCooldown());
    }

    public IEnumerator IRunningCooldown()
    {
        canRun = false;
        yield return new WaitForSeconds(runCooldownSeconds);
        canRun = true;
        Debug.Log("Running Ready!");
    }
}
