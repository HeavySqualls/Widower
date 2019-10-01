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
    [Header("Player 1 Refs:")]
    public bool isPlayer1 = false;
    private Player_1_Controller player_1_Controller;
    private Player_1_Manager p1_Manager;

    [Space]
    [Header("Player 2 Refs:")]
    //private bool isPlayer2 = false;
    //private Player_2_Controller player_2_Controller;
    //private Player_2_Manager p2_Manager;

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
        if (other.GetComponentInParent<Player_1_Controller>()) //|| )
        {
            isPlayer1 = true;
            p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
            interactedObj = other.gameObject;
            cameraTarget = other.GetComponentInChildren<Camera>();
            player_1_Controller = other.GetComponent<Player_1_Controller>();

            inputCall.enabled = true;
            player_1_Controller.InteractableObject(gameObject.GetComponent<PickupController>());

            interactObjInRange = true;
        }
        //else if (other.GetComponentInParent<Player_2_Controller>())
        //{
        //    isPlayer2 = true;
        //    p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();
        //    interactedObj = other.gameObject;
        //    cameraTarget = other.GetComponentInChildren<Camera>();
        //    player_2_Controller = other.GetComponent<Player_2_Controller>();

        //    inputCall.enabled = true;
        //    player_2_Controller.InteractableObject(gameObject.GetComponent<PickupController>());

        //    interactObjInRange = true;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player_1_Controller>()) //|| other.GetComponentInParent<Player_2_Controller>())
        {
            isPlayer1 = false;
            p1_Manager = null;
            interactedObj = null;
            cameraTarget = null;
            other.GetComponent<Player_1_Controller>().isInteracting = false;
            player_1_Controller = null;

            inputCall.enabled = false;
            eatProgress.enabled = false;

            interactObjInRange = false;
        }
        //else if (other.GetComponentInParent<Player_2_Controller>())
        //{
        //    isPlayer2 = false;
        //    p2_Manager = null;
        //    interactedObj = null;
        //    cameraTarget = null;
        //    player_2_Controller = null;

        //    inputCall.enabled = false;
        //    eatProgress.enabled = false;

        //    interactObjInRange = false;
        //}
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
        if (isPlayer1)
        {
            if (player_1_Controller.isEating && maxHealth > 0) //* || other.GetComponentInParent<Player_2_Controller>())//* && maxHealth > 0)
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
        //else if (isPlayer2)
        //{
        //    if (player_2_Controller.isEating && maxHealth > 0) //* || other.GetComponentInParent<Player_2_Controller>())//* && maxHealth > 0)
        //    {
        //        maxHealth -= eatSpeed;
        //        inputCall.enabled = false;

        //        //eat time needs to build up instead of down. 
        //        eatProgress.fillAmount = (maxHealth / maxHealthStart);

        //        playerManager.FreezePlayer();

        //        if (maxHealth <= 0 && !respawning)
        //        {
        //            player_2_Controller.isEating = false;
        //            playerManager.UnFreezePlayer();
        //            pickupID.IncrementStats();
        //            Deactivate();
        //            StartRespawnTime(timeToRespawn);
        //        }
        //    }
        //}
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
        interactedObj.GetComponent<Player_1_Controller>().interactedController = null;
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
