using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    [Space]
    [Header("Countdown Vars")]
    public float maxHealth;
    public Image eatProgress;
    public Text inputCall;
    public GameObject CanvasObj;
    private float maxHealthStart;
    private float eatSpeed;

    [Space]
    [Header("Respawn Vars")]
    public float timeToRespawn;
    private float respawnTime;
    private bool respawning = false;

    [Space]
    [Header("Universal Interaction obj Refs:")]
    private GameObject interactedObj; // Assigned in OnTriggerEnter()
    public Camera cameraTarget;
    public bool interactObjInRange = false;

    [Space]
    [Header("Player Refs:")]
    private Player_Controller player_1_Controller;
    private Player_1_Manager p1_Manager;


    [Space]
    [Header("Self Refs:")]
    public GameObject PickUpObj;
    private PickupID pickupID;

    void Start()
    {    
        pickupID = gameObject.GetComponentInChildren<PickupID>();

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
        if (other.GetComponentInParent<Player_Controller>())
        {
            p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
            interactedObj = other.gameObject;
            cameraTarget = other.GetComponentInChildren<Camera>();
            player_1_Controller = other.GetComponent<Player_Controller>();

            inputCall.enabled = true;
            player_1_Controller.InteractableObject(gameObject.GetComponent<PickupController>());

            interactObjInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player_Controller>())
        {
            p1_Manager = null;
            interactedObj = null;
            cameraTarget = null;
            other.GetComponent<Player_Controller>().isInteracting = false;
            player_1_Controller = null;

            inputCall.enabled = false;
            eatProgress.enabled = false;

            interactObjInRange = false;
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

    public void StopEatCountdownTimer()
    {
        eatProgress.enabled = false;
        eatSpeed = 0;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;
        p1_Manager.UnFreezePlayer();
    }

    private void EatCountDown()
    {

        if (player_1_Controller.isEating && maxHealth > 0)
        {
            maxHealth -= eatSpeed;
            inputCall.enabled = false;

            //eat time needs to build up instead of down. 
            eatProgress.fillAmount = (maxHealth / maxHealthStart);

            p1_Manager.FreezePlayer();

            if (maxHealth <= 0 && !respawning)
            {
                player_1_Controller.isEating = false;
                p1_Manager.UnFreezePlayer();
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
        interactedObj.GetComponent<Player_Controller>().interactedController = null;
        interactedObj = null;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void ReActivate()
    {
        PickUpObj.SetActive(true);
        CanvasObj.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
