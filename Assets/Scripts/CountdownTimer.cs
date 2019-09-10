using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    int countdownStartValue = 5;

    void Start()
    {
        Countdown();
    }

    void Update()
    {
        
    }

    void Countdown()
    {
        if (countdownStartValue > 0)
        {
            countdownStartValue--;
            Invoke("countdownTimer", 1.0f);
        }
        else
        {
            Debug.Log("Round over");
        }
    }
}
