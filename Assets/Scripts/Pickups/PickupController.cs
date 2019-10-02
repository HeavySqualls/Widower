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
    private bool isOccupied = false;
    private bool isBeingEaten = false;

    [Space]
    [Header("Universal Interaction obj Refs:")]
    private GameObject interactedObj; // Assigned in OnTriggerEnter()
    public Camera cameraTarget;
    public bool interactObjInRange = false;

    [Space]
    [Header("Player Refs:")]
    private Player_Controller player_Controller;

    [Space]
    [Header("Self Refs:")]
    public GameObject PickUpObj;
    private BoxCollider myBoxCollider;
    private PickupID pickupID;

    void Start()
    {    
        pickupID = GetComponentInChildren<PickupID>();
        myBoxCollider = GetComponent<BoxCollider>();

        inputCall.enabled = false;
        eatProgress.enabled = false;
    }


    void Update()
    {
        if (player_Controller != null)
        {
            EatCountDown();
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player_Controller>() && !isOccupied)
        {
            interactedObj = other.gameObject;
            player_Controller = interactedObj.GetComponent<Player_Controller>();
            cameraTarget = other.GetComponentInChildren<Camera>();

            inputCall.enabled = true;
            player_Controller.InteractableObject(this);

            interactObjInRange = true;
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player_Controller>())
        {
            if (isBeingEaten)
            {
                player_Controller.isEating = false;
                player_Controller.isInteracting = false;

                StopEatCountdownTimer();
            }
            else
            {
                player_Controller = null;
                interactedObj = null;

                inputCall.enabled = false;
                eatProgress.enabled = false;
                cameraTarget = null;

                interactObjInRange = false;
                isOccupied = false;
            }
        }
    }

    public float StartEatCountdownTimer(float time)
    {
        isBeingEaten = true;
        eatProgress.enabled = true;
        eatSpeed = time;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;

        return eatSpeed;                               
    }

    public void StopEatCountdownTimer()
    {
        player_Controller.playerManager.UnFreezePlayer();

        if (!interactObjInRange)
        {
            player_Controller = null;
            interactedObj = null;
        }

        eatProgress.enabled = false;
        eatSpeed = 0;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;

        isOccupied = false;
        isBeingEaten = false;
    }

    private void EatCountDown()
    {
        if (player_Controller.isEating && maxHealth > 0 && isBeingEaten)
        {
            maxHealth -= eatSpeed;
            inputCall.enabled = false;

            eatProgress.fillAmount = (maxHealth / maxHealthStart); //TODO: Eat time needs to build up instead of down. 

            player_Controller.playerManager.FreezePlayer();

            if (maxHealth <= 0 && !respawning)
            {
                player_Controller.isEating = false;
                player_Controller.playerManager.UnFreezePlayer();
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
        player_Controller.interactedController = null;
        interactedObj = null;
        myBoxCollider.enabled = false;
        isOccupied = false;
    }

    void ReActivate()
    {
        PickUpObj.SetActive(true);
        CanvasObj.SetActive(true);
        myBoxCollider.enabled = true;
    }
}
