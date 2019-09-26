using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Space]
    [Header("Time Stats:")]
    public string minutes;
    public string seconds;
    private float startTime;
    private float currentTime;

    [Space]
    [Header("Time Status:")]
    public bool isCountDownTime;
    private bool isCountDownStarted;

    [Space]
    [Header("Time References:")]
    public Text startText;
    public Text time;
    private Player_1_Manager playerManager;

    void Start()
    {
        playerManager = Toolbox.GetInstance().GetPlayer_1_Manager();
        time = GameObject.Find("TimerUI").GetComponent<Text>();
        startText = GameObject.Find("Start").GetComponent<Text>();
        time.enabled = false;
        isCountDownTime = false;
    }


    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (isCountDownTime)
        {
            startText.enabled = false;
            currentTime = startTime -= Time.deltaTime;

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
        if (!isCountDownStarted)
        {
            Debug.Log("Time has started!");
            time.enabled = true;
            isCountDownStarted = true;
            isCountDownTime = true;
            startTime = levelTime;
        }
        return startTime;
    }

    public void StopCountDownTimer()
    {
        isCountDownTime = false;
        time.enabled = false;
        isCountDownStarted = false;
        playerManager.DisplayScore();
    }

    public void ResetTimer()
    {
        playerManager = Toolbox.GetInstance().GetPlayer_1_Manager();
        time = GameObject.Find("TimerUI").GetComponent<Text>();
        startText = GameObject.Find("Start").GetComponent<Text>();
        time.enabled = false;
        isCountDownTime = false;

        minutes = "0";
        seconds = "0";
    }
}

