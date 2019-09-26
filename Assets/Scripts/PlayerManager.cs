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
    [Header("Player References:")]
    private PlayerController playerController;
    private Text playerStats; //will change the text from the UI

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pMoveSpeed * 2;

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

    public void UpgradeStats()
    {
        pointsToAdd = (pointPickups * 10) + (eatPickups * 5) + (movePickups * 5);
        eatSpeedToAdd = (eatPickups * 0.25f);
        moveSpeedToAdd = (movePickups * 0.30f);

        // Add these in a respawn method before calling ResetPickups()
        //pPoints += pointsToAdd;
        //pEatSpeed += eatSpeedToAdd;
        //pMoveSpeed += moveSpeedToAdd;
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

    public void ResetPlayer()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pMoveSpeed * 2;
    }
}
