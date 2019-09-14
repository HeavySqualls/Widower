using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float startTime;
    private float currentTime;

    private bool countDownTime = false;

    public Text startText;
    public Text time;

    public string minutes;
    public string seconds;

    public GameManager gameManager;

    void Start()
    {
        //TODO: check to see if this is needed or not
        gameManager = Toolbox.GetInstance().GetGameManager();
        time = GameObject.Find("TimerUI").GetComponent<Text>();
        startText = GameObject.Find("Start").GetComponent<Text>();
    }


    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (countDownTime)
        {
            startText.enabled = false;

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
        gameManager.isGameOver = true;
    }

    public void ResetTimer()
    {
        minutes = "0";
        seconds = "0";
    }
}

