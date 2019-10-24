using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Space]
    [Header("Time Stats:")]
    public float startTime;
    public float currentTime = 0;

    [Space]
    [Header("Time Status:")]
    public bool isCountDownTime = false;

    public void ResetTime()
    {
        isCountDownTime = false;
        currentTime = 0;
    }

    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (isCountDownTime)
        {
            currentTime = startTime -= Time.deltaTime;
        }
    }
}

