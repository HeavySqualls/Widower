using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    static TimeManager instance;

    private float startTime;
    private float currentTime;
    private bool countUpTime = false;

    public Text time;

    public string minutes;
    public string seconds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        time = GameObject.Find("Time").GetComponent<Text>();
    }

    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (countUpTime)
        {
            currentTime = Time.time - startTime;

            minutes = ((int)currentTime / 60).ToString();
            seconds = (currentTime % 60).ToString("f2");

            time.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        countUpTime = true;
    }

    public void StopTimer()
    {
        countUpTime = false;
    }

    public void ResetTimer()
    {
        minutes = 0.ToString();
        seconds = 0.ToString();
    }
}

