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
    private float countDownTime;
    [SerializeField]

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        pickupID = gameObject.GetComponentInChildren<PickupID>();
    }


    void Update()
    {
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            Toolbox.GetInstance().GetPlayerManager().FreezePlayer();

            if (countDownTime <= 0)
            {
                Toolbox.GetInstance().GetPlayerManager().UnFreezePlayer();
                pickupID.IncrementStats();
                Destroy(gameObject);
            }
        }
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
        countDownTime = time;
        return countDownTime;                               
    }
}
