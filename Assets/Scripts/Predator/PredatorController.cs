using UnityEngine;
using UnityEngine.AI;

public class PredatorController : MonoBehaviour
{
    enum State { Patrolling, Attacking, Dead};
    State currentState;

    [Space]
    [Header("Predator Spawn:")]
    public string thisSpawner;

    [Space]
    [Header("Predator Patrolling:")]
    private Transform patrolTarget;

    [Space]
    [Header("Predator Attacking:")]

    [Space]
    [Header("Predator References:")]
    private PredatorSpawner spawner;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        OnPatrolling();
    }

    public void Update()
    {
        switch (this.currentState)
        {
            case State.Patrolling: this.Patrolling();
                break;
            case State.Attacking: this.Attacking();
                break;
            case State.Dead: this.LeftArea();
                break;
        }
    }


    // ------ STATES ------ //


    private void Patrolling()
    {
        spawner = GameObject.FindGameObjectWithTag(thisSpawner).GetComponent<PredatorSpawner>();

        agent.destination = patrolTarget.position;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            this.currentState = State.Dead;
        }
    }

    private void Attacking()
    {
        // Move the unit rapidly to the players position. 
        // Once at the required position, move to Dead state. 
    }

    private void LeftArea()
    {
        spawner.predatorAlive = false;
        Destroy(gameObject); 
    }


    // ------ STATE INDUCING METHODS ------ //

    public void OnPatrolling()
    {
        // Randomly select a start point and move the unit from the start point to its 
        // childed end point.
        // If a player unit comes within a certain radius, move to attacking state.

        patrolTarget = gameObject.transform.parent.GetChild(0).transform;

        this.currentState = State.Patrolling;
    }

    public void OnAttacking()
    {

    }

    // ------ METHODS ------ //


    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitPredator playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitPredator>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnPredatorHit(other, this);
        }
    }
}
