﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    [Space]
    [Header("Player Stats:")]
    public float playerPoints;
    public float eatSpeed = 3;
    public float pRunSpeed;
    
    [Header("Player PickUp:")]
    public int greyPickUps;
    public int orangePickUps;
    public int bluePickUps;
    
    public float runCooldownSeconds = 4;
    public float pWalkSpeed = 8;
    public float secondsCanRun = 2;

    private PlayerController playerController;

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
}
