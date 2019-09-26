using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    private float eatSpeed;
    public float maxHealth;
    private float maxHealthStart;

    public float timeToRespawn;
    private float respawnTime;
    private bool respawning = false;

    public Image eatProgress;
    public Text inputCall;

    public GameObject PickUpObj;
    public GameObject CanvasObj;

    private Player_1_Manager playerManager;
    private Player_1_Controller playerController;
    private BoxCollider player;
    public Camera cameraTarget;

    private PickupID pickupID;

    private void Awake()
    {
        playerManager = Toolbox.GetInstance().GetPlayer_1_Manager();
    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Player_1_Controller>();
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
        eatSpeed = time;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;

        return eatSpeed;                               
    }

    private void EatCountDown()
    {
        if (playerController.isEating && maxHealth > 0)
        {
            maxHealth -= eatSpeed;
            inputCall.enabled = false;

            //eat time needs to build up instead of down. 
            eatProgress.fillAmount = (maxHealth / maxHealthStart);

            playerManager.FreezePlayer();

            if (maxHealth <= 0 && !respawning)
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
