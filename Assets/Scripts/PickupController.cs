using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    private float countDownTime;
    private float countDownTimeStart;

    public float timeToRespawn;
    private float respawnTime;
    private bool respawning = false;

    public Image eatProgress;
    public Text inputCall;

    public GameObject PickUpObj;
    public GameObject CanvasObj;

    private PlayerManager playerManager;
    private PlayerController playerController;
    private BoxCollider player;
    public Camera cameraTarget;

    private PickupID pickupID;

    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayerManager();
    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        pickupID = gameObject.GetComponentInChildren<PickupID>();
        cameraTarget = GameObject.Find("Player").GetComponentInChildren<Camera>();
        inputCall.enabled = false;
        eatProgress.enabled = false;
    }


    void Update()
    {
        EatCountDown();
        Respawn();
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
            playerController.interactedController = null;
            inputCall.enabled = false;
            eatProgress.enabled = false;
        }
    }

    public float StartEatCountdownTimer(float time)
    {
        eatProgress.enabled = true;
        countDownTime = time;
        countDownTimeStart = countDownTime;
        return countDownTime;                               
    }

    private void EatCountDown()
    {
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            inputCall.enabled = false;
            eatProgress.fillAmount = (countDownTime / countDownTimeStart);

            playerManager.FreezePlayer();

            if (countDownTime <= 0 && !respawning)
            {
                playerController.isEating = false;
                playerManager.UnFreezePlayer();
                pickupID.IncrementStats();
                Deactivate();
                StartRespawnTime(timeToRespawn);
            }
        }
    }

    private float StartRespawnTime(float time)
    {
        respawnTime = time;
        respawning = true;
        return respawnTime;
    }

    private void Respawn()
    {
        if (respawnTime > 0 && respawning)
        {
            respawnTime -= Time.deltaTime;

            if (respawnTime <= 0)
            {
                ReActivate();
                respawning = false;
            }
        }
    }

    void Deactivate()
    {
        PickUpObj.SetActive(false);
        CanvasObj.SetActive(false);
    }

    void ReActivate()
    {
        PickUpObj.SetActive(true);
        CanvasObj.SetActive(true);
    }
}
