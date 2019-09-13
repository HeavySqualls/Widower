using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float countDownTimeStart;

    private float eatSpeed;

    public Image eatProgress;
    public Text inputCall;

    private PlayerManager playerManager;
    private PlayerController playerController;

    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        pickupID = gameObject.GetComponentInChildren<PickupID>();
        eatSpeed = playerManager.eatSpeed;
        inputCall.enabled = false;
        eatProgress.enabled = false;
    }


    void Update()
    {
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;

            eatProgress.fillAmount = (countDownTime / countDownTimeStart);

            playerManager.FreezePlayer();

            if (countDownTime <= 0)
            {
                playerManager.UnFreezePlayer();
                pickupID.IncrementStats();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            inputCall.enabled = true;
            eatProgress.enabled = true;
            playerController.InteractableObject(gameObject);
            //StartCountdownTimer(playerManager.eatSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            playerController.interObject = null;
            inputCall.enabled = false;
            eatProgress.enabled = false;
        }
    }

    public float StartCountdownTimer(float time)
    {
        countDownTime = time;
        countDownTimeStart = countDownTime;
        return countDownTime;                               
    }
}
