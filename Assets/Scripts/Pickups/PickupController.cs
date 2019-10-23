using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    enum State { Idle, Interacted, GettingEaten, Respawn};
    State currentState;

    [Space]
    [Header("Countdown Vars")]
    public float maxHealth;
    public Image eatProgress;
    public GameObject inputCall;
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
    private GameObject interactedObj;
    public Camera cameraTarget;
    public bool interactObjInRange = false;

    [Space]
    [Header("Player Refs:")]
    public Player_Controller pCon;

    [Space]
    [Header("Self Refs:")]
    public GameObject eatEffect;
    private GameObject currentEatEffect;

    public GameObject PickUpObj;
    private BoxCollider myBoxCollider;
    private SphereCollider triggerZoneCollider;
    private PickupID pickupID;

    void Start()
    {
        this.currentState = State.Idle;

        pickupID = GetComponentInChildren<PickupID>();
        myBoxCollider = GetComponentInChildren<BoxCollider>();
        triggerZoneCollider = GetComponent<SphereCollider>();

        inputCall.SetActive(false);
        eatProgress.enabled = false;
    }


    void Update()
    {
        switch (this.currentState)
        {
            case State.Idle: this.Idle();
                break;
            case State.Interacted: this.Interacted();
                break;
            case State.GettingEaten: this.GettingEaten();
                break;
            case State.Respawn: this.Respawn();
                break;
        }
    }


    // ------ STATES ------ //


    private void Idle()
    {
        // Pickup is sitting in its idle state
    }

    private void Interacted()
    {
        // Pickup is sitting in its interacted state
    }

    private void GettingEaten()
    {
        maxHealth -= eatSpeed;

        eatProgress.fillAmount = (maxHealth / maxHealthStart);

        pCon.playerManager.FreezePlayer();

        if (!currentEatEffect)
        {
            currentEatEffect = Instantiate(eatEffect, transform.position, transform.rotation);
        }

        if (maxHealth <= 0)
        {
            pCon.isEating = false;
            pCon.playerManager.UnFreezePlayer();

            pickupID.IncrementStats();

            Deactivate();

            GoRespawn(timeToRespawn);
        }
    }

    private void Respawn()
    {
        respawnTime -= Time.deltaTime;

        if (respawnTime <= 0)
        {
            ReActivate();
            respawning = false;
            triggerZoneCollider.enabled = true;
            this.currentState = State.Idle;
        }
    }


    // ------ STATE INDUCING METHODS ------ //


    public void GoIdle()
    {
        eatProgress.enabled = false;
        eatSpeed = 0;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;

        inputCall.SetActive(false);
        cameraTarget = null;
        interactObjInRange = false;
        pCon = null;
        interactedObj = null;

        this.currentState = State.Idle;
    }

    public void GoInteracted()
    {
        inputCall.SetActive(true);
        interactObjInRange = true;

        pCon.InteractableObject(this);

        this.currentState = State.Interacted;
    }

    public float GoGettingEaten(float time)
    {
        inputCall.SetActive(false);
        eatProgress.enabled = true;

        eatSpeed = time;
        maxHealth = pickupID.hValue;
        maxHealthStart = maxHealth;

        triggerZoneCollider.enabled = false;
        myBoxCollider.enabled = false;

        this.currentState = State.GettingEaten;

        return eatSpeed;
    }

    public void StopGettingEaten()
    {
        if (interactObjInRange)
        {
            pCon.playerManager.UnFreezePlayer();
        }

        myBoxCollider.enabled = true;
        triggerZoneCollider.enabled = true;
        Destroy(currentEatEffect);
        GoIdle();
    }

    public float GoRespawn(float time)
    {
        Destroy(currentEatEffect);
        respawnTime = time;
        respawning = true;
        this.currentState = State.Respawn;
        return respawnTime;
    }


    // ------ METHODS ------ //


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Controller>() && this.currentState == State.Idle)
        {
            // Assign references to the player in trigger zone
            interactedObj = other.gameObject;
            pCon = interactedObj.GetComponent<Player_Controller>();
            cameraTarget = other.GetComponentInChildren<Camera>();

            // Set up for interaction state
            GoInteracted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player_Controller>() && this.currentState == State.Interacted)
        {
            pCon.isEating = false;
            pCon.isInteracting = false;
            GoIdle();
        }
        else if (other.GetComponent<Player_Controller>() && this.currentState == State.GettingEaten)
        {
            pCon.isEating = false;
            pCon.isInteracting = false;
            Destroy(currentEatEffect);
            StopGettingEaten();
        }
    }

    void Deactivate()
    {
        PickUpObj.SetActive(false);
        CanvasObj.SetActive(false);
        pCon.interactedController = null;
        interactedObj = null;
        myBoxCollider.enabled = false;
    }

    void ReActivate()
    {
        PickUpObj.SetActive(true);
        CanvasObj.SetActive(true);
        myBoxCollider.enabled = true;
    }
}
