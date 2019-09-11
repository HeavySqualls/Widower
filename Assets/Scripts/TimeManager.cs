using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float startTime;
    private float currentTime;

    private bool countDownTime = false;

    public Text time;

    public string minutes;
    public string seconds;

    void Start()
    {
        //TODO: check to see if this is needed or not
        time = GameObject.Find("TimerUI").GetComponent<Text>();
    }


    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (countDownTime)
        {
            currentTime = startTime - Time.time;

            minutes = ((int)currentTime / 60).ToString();
            seconds = (currentTime % 60).ToString("f1");

            time.text = minutes + ":" + seconds;

            if (currentTime <= 0)
            {
                StopCountDownTimer();
            }
        }
    }

    public float StartCountDownTimer(float levelTime)
    {
        startTime = levelTime;
        countDownTime = true;

        return startTime;
    }

    public void StopCountDownTimer()
    {
        countDownTime = false;
    }

    public void ResetTimer()
    {
        minutes = 0.ToString();
        seconds = 0.ToString();
    }
}

