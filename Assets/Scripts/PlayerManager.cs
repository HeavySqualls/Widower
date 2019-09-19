using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    [Space]
    [Header("Player Stats:")]
    public float playerPoints;
    public float eatSpeed = 10;
    public float pRunSpeed;
    
    [Header("Player PickUp:")]
    public int greyPickUps;
    public int orangePickUps;
    public int bluePickUps;
    
    public float runCooldownSeconds = 4;
    public float pWalkSpeed = 8;
    public float secondsCanRun = 2;

    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pWalkSpeed * 2;
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
        // grey pick ups 
        playerPoints += (greyPickUps * 10) + (orangePickUps * 5) + (bluePickUps * 5);
        eatSpeed += (orangePickUps * 0.3f);
        Debug.Log("Eat Speed: " + eatSpeed);
        pWalkSpeed += (bluePickUps * 0.30f);
    }

    public void ResetPickups()
    {
        greyPickUps = 0;
        orangePickUps = 0;
        bluePickUps = 0;
    }

    public void ResetPlayer()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        pRunSpeed = pWalkSpeed * 2;
    }
}
