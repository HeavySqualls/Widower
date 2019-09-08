using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Test : MonoBehaviour
{
    [Space]
    [Header("PLayer Statistics:")]
    public Vector3 movementDirection;
    public float pRunSpeed;
    public float movementSpeed;
    public float pMoveSpeedStart;
    public float speedDelaySeconds;
    public float speedReduction;
    public float pCurrentMoveSpeed;
    public float rotSpeed;
    public bool processInputs = true;
    public bool isRunning = true;
    public bool canRun = true;

    public Rigidbody rb;

    [Space]
    [Header("Audio:")]


    [Space]
    [Header("Stamina Flash:")]

    public float secondsCanRun;
    public float runCooldownSeconds;
    [SerializeField] float runTime;


    void Start()
    {
        pCurrentMoveSpeed = pMoveSpeedStart;
    }

    void Update()
    {
        ProcessInputs();
        Move();
        Running();
    }

    void ProcessInputs()
    {
        if (processInputs == true)
        {
            Vector3 movementDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 0.5f);
            movementDirection.Normalize();

            Vector3 input = new Vector3(0, Input.GetAxis("Horizontal"), 0);
            transform.Rotate(input * rotSpeed * Time.deltaTime);

        }
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * pCurrentMoveSpeed;
    }


    void Rotate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.Rotate(input * rotSpeed * Time.deltaTime);
    }


    void Running()
    {
        // Start Running
        if (Input.GetKey(KeyCode.LeftShift) && movementSpeed != 0 && canRun == true)
        {
            movementSpeed = 1.0f;
            Debug.Log("Player State: Running");
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


    public void PlayerReduceSpeed()
    {
        StartCoroutine(IDamageSustainedTime(speedReduction));
    }

    public IEnumerator IDamageSustainedTime(float reducedSpeed)
    {
        pCurrentMoveSpeed -= reducedSpeed;
        canRun = false;
        Debug.Log("Current move speed: " + pCurrentMoveSpeed);

        yield return new WaitForSeconds(speedDelaySeconds);

        pCurrentMoveSpeed = pMoveSpeedStart;
        canRun = true;
        Debug.Log("Current move speed: " + pCurrentMoveSpeed);
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

