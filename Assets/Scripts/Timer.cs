using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static Timer instance;

    private float startTime;
    private float currentTime;
    private bool trackTime = false;

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
        if (trackTime)
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
        trackTime = true;
    }

    public void StopTimer()
    {
        trackTime = false;
    }

    public void ResetTimer()
    {
        minutes = 0.ToString();
        seconds = 0.ToString();
    }
}

