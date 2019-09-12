using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Space]
    [Header("Player Stats:")]
    public float secondsCanRun = 2;
    public float runCooldownSeconds = 4;
    public float pRunSpeed = 16;
    public float pWalkSpeed = 8;

    public float eatSpeed = 3;

    public int greyPickUps;
    public int orangePickUps;
    public int bluePickUps;

    private PlayerController playerController;

    private void Awake()
    {
        // Add playerController
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void FreezePlayer()
    {
        playerController.processInputs = false;
    }

    public void UnFreezePlayer()
    {
        playerController.processInputs = true;
    }
}
