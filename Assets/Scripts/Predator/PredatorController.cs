using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PredatorController : MonoBehaviour
{
    enum State { Patrolling, Attacking, Idle, Dead};
    State currentState;

    [Space]
    [Header("Predator Spawn:")]
    public string thisSpawnerName;
    public GameObject thisSpawnerGO;

    [Space]
    [Header("Predator Patrolling:")]
    private Transform patrolTarget;

    [Space]
    [Header("Predator Attacking:")]
    public float attackingSpeed = 10f;
    public float cooldownTime = 2f;
    public float deathZone = 2.5f;
    private Transform target;
    private bool isAttacking;

    [Space]
    [Header("Predator References:")]
    public PredatorSpawner spawner;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        thisSpawnerGO = GameObject.Find(thisSpawnerName);
        spawner = thisSpawnerGO.GetComponent<PredatorSpawner>();
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
            case State.Idle: this.Idle();
                break;
            case State.Dead: this.LeftArea();
                break;
        }
    }


    // ------ STATES ------ //


    private void Patrolling()
    {
        agent.destination = patrolTarget.position;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            this.currentState = State.Dead;
        }
    }

    private void Attacking()
    {
        agent.destination = target.position;
        agent.speed = attackingSpeed;

        if (target.gameObject.GetComponent<Player_Controller>().isDead == true)
        {
            this.currentState = State.Patrolling;
        }

        if (!agent.pathPending && agent.remainingDistance < deathZone)
        {
            target.gameObject.GetComponent<Player_Controller>().predatorKilledPlayer = true;
            target.gameObject.GetComponent<Player_Controller>().playerManager.DisplayScore();

            CoolDown();

            this.currentState = State.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, deathZone);
    }

    private void Idle()
    {
        // Sit idle and wait for PredatorEatCooldown() to finish
    }

    private void LeftArea()
    {
        spawner.OnSpawmCoolDown();

        Destroy(gameObject); 
    }


    // ------ STATE INDUCING METHODS ------ //

    public void OnPatrolling()
    {
        patrolTarget = gameObject.transform.parent.GetChild(0).transform;

        this.currentState = State.Patrolling;
    }

    private void CoolDown()
    {
        StartCoroutine(PredatorEatCooldown());
    }



    // ------ METHODS ------ //


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Controller>())
        {
            target = other.gameObject.transform;
            this.currentState = State.Attacking;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Controller>())
        {
            target = null;
            this.currentState = State.Patrolling;
        }
    }

    private IEnumerator PredatorEatCooldown()
    {
        agent.isStopped = true;
        print("Predator Cooldown Start");

        yield return new WaitForSeconds(cooldownTime);

        print("Predator Cooldown End");
        agent.isStopped = false;
        this.currentState = State.Patrolling;
    }
}
