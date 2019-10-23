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
    public GameObject gameOverPanel;

    [Space]
    [Header("Time References:")]
    public GameObject startText;
    public Text time;
    private Player_Manager p1_Manager;
    private Player_Manager p2_Manager;

    void Start()
    {
        // References and set up in ResetTimer() and is called by the GameManager 
    }

    public void ResetTimer()
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();

        time = GameObject.FindGameObjectWithTag("TimerUI").GetComponent<Text>();
        startText = GameObject.FindGameObjectWithTag("StartPrompt");
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverNotice");

        time.enabled = false;
        gameOverPanel.SetActive(false);

        isCountDownTime = false;

        minutes = "0";
        seconds = "0";
    }

    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (isCountDownTime)
        {
            startText.SetActive(false);
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

        p1_Manager.UnFreezePlayer();
        p1_Manager.pUI.readyPanel.SetActive(false);
        p1_Manager.HideCursor();

        p2_Manager.UnFreezePlayer();
        p2_Manager.pUI.readyPanel.SetActive(false);
        p2_Manager.HideCursor();

        Toolbox.GetInstance().GetGameManager().isGameStart = true;
    }

    public void SetGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}

