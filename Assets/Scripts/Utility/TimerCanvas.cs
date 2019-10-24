using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCanvas : MonoBehaviour
{
    public string minutes;
    public string seconds;

    public GameObject gameOverPanel;
    public GameObject startText;
    public Text time;

    private bool isCountDownStarted = false;

    private TimeManager tMan;
    private Player_Manager p1_Man;
    private Player_Manager p2_Man;
    private GameManager gMan;


    void Start()
    {
        tMan = Toolbox.GetInstance().GetTimeManager();
        gMan = Toolbox.GetInstance().GetGameManager();
        p1_Man = Toolbox.GetInstance().GetPlayer_1_Manager();
        p2_Man = Toolbox.GetInstance().GetPlayer_2_Manager();

        time.enabled = false;
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        UpdateTime();
    }

    public void StartCountDownTimer()
    {
        Debug.Log("Time has started!");
        time.enabled = true;
        tMan.startTime = 3;
        isCountDownStarted = true;
        tMan.isCountDownTime = true;
    }

    public void StopCountDownTimer()
    {
        isCountDownStarted = false;
        ResetTimer();
        time.enabled = false;

        p1_Man.UnFreezePlayer();
        p1_Man.pUI.readyPanel.SetActive(false);
        p1_Man.HideCursor();

        p2_Man.UnFreezePlayer();
        p2_Man.pUI.readyPanel.SetActive(false);
        p2_Man.HideCursor();

        gMan.isGameStart = true;
    }

    public void ResetTimer()
    {
        tMan.ResetTime();
        minutes = "0";
        seconds = "0";
    }

    public void UpdateTime()
    {
        if (isCountDownStarted)
        {
            startText.SetActive(false);

            minutes = ((int)tMan.currentTime / 60).ToString();
            seconds = (tMan.currentTime % 60).ToString("f1");

            time.text = minutes + ":" + seconds;

            if (tMan.currentTime < 0)
            {
                StopCountDownTimer();
            }
        }
    }

    public void SetGameOverPanel()
    {
        if (gameOverPanel.activeSelf == false)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverPanel.activeSelf == true)
        {
            gameOverPanel.SetActive(false);
        }
    }
}
