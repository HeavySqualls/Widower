using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private float countDownTime;
    [SerializeField]
    private float countDownTimeStart;

    public Image eatProgress;
    public Text inputCall;

    private PlayerManager playerManager;
    private PlayerController playerController;
    private BoxCollider player;
    public Camera cameraTarget;

    private PickupID pickupID;

    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        pickupID = gameObject.GetComponentInChildren<PickupID>();
        cameraTarget = GameObject.Find("Player").GetComponentInChildren<Camera>();

        inputCall.enabled = false;
        eatProgress.enabled = false;
    }


    void Update()
    {
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            inputCall.enabled = false;
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
            playerController.InteractableObject(gameObject.GetComponent<PickupController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            //playerController.InteractableObject == null;
            inputCall.enabled = false;
            eatProgress.enabled = false;
        }
    }

    public float StartCountdownTimer(float time)
    {
        eatProgress.enabled = true;
        countDownTime = time;
        countDownTimeStart = countDownTime;
        return countDownTime;                               
    }
}
