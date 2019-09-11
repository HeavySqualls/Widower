using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    /// <summary>
    /// ONCE TESTING IS DONE, PLACE THIS BACK ON THE GAME OBJECT
    /// </summary>

    private BoxCollider player;

    private PickupID pickupID;

    [SerializeField]
    private float currentTime;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private bool countDown = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        pickupID = gameObject.GetComponentInChildren<PickupID>();
    }


    void Update()
    {
        TrackTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            Debug.Log("Call StartCountdownTimer");
            StartCountdownTimer(Toolbox.GetInstance().GetPlayerManager().eatSpeed);
        }
    }

    private float StartCountdownTimer(float time)
    {
        Debug.Log("StartCountdownTimer");
        startTime = time;
        countDown = true;
        return startTime;                               
    }

    private void TrackTime()
    {
        if (countDown)
        {
            currentTime = startTime - Time.time;

            if (currentTime <= 0)
            {
                pickupID.IncrementStats();
                countDown = false;
                Destroy(gameObject);
            }
        }
    }
}
