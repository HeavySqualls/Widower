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
    //public float speedReduction = 4;

    public float eatSpeed;

    public int greyPickUps;
    public int orangePickUps;
    public int bluePickUps;

    public void AddScore()
    {
        // tally & present player pick-ups
    }
}
