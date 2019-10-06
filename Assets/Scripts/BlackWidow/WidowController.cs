using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WidowController : MonoBehaviour
{
    enum State { Patrolling, Eating};
    State currentState;

    [Space]
    [Header("Widow States:")]

    public int scoreToBeat = 2;
    public bool isEating = false;
    private bool isCoolDown = false;

    [Space]
    [Header("Widow Patrolling:")]
    public Transform[] waypoints;

    [Space]
    [Header("Widow Eating:")]
    private float cooldownTime = 3f;

    [Space]
    [Header("Widow References:")]
    private NavMeshAgent agent;
    private GameManager gM;

    private void Start()
    {
        gM = Toolbox.GetInstance().GetGameManager();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        this.currentState = State.Patrolling;
    }

    private void Update()
    {
        if (gM.isGameStart)
        {
            switch (this.currentState)
            {
                case State.Patrolling:
                    this.Patrolling();
                    break;
                case State.Eating:
                    this.Eating();
                    break;
            }
        }
    }


    // ------ STATES ------ //


    public void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }       
    }

    private void GoToNextPoint()
    {
        if (waypoints.Length == 0)
        {
            print("No waypoints set");
            return;
        }

        int randomDest = Random.Range(0, waypoints.Length);

        agent.destination = waypoints[randomDest].position;
    }

    public void Eating()
    {
        if (isEating && !isCoolDown)
        {
            StartCoroutine(WidowEatCooldown());
            isCoolDown = true;
        }
    }


    // ------ STATE INDUCING METHODS ------ //


    public void GoPatrolling()
    {
        this.currentState = State.Patrolling;
    }

    public void GoEating()
    {
        isEating = true;
        this.currentState = State.Eating;
    }


    // ------ METHODS ------ //


    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitWidow playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitWidow>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnWidowHit(other, this);
        }
    }

    private IEnumerator WidowEatCooldown()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(cooldownTime);

        isEating = false;
        isCoolDown = false;
        agent.isStopped = false;
        GoPatrolling();
    }
}
