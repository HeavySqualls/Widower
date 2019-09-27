using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WidowController : MonoBehaviour
{
    enum State { Patrolling, Eating};
    State currentState;

    public bool isEating = false;
    private bool isCoolDown = false;
    private bool isMoving = false;
    private bool isOnFloor = false;

    private float cooldownTime = 2f;

    public Transform[] waypoints;
    int currentTarget;

    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    private void Start()
    {


        this.currentState = State.Patrolling;


    }

    private void Update()
    {
        switch (this.currentState)
        {
            case State.Patrolling: this.Patrolling();
                break;
            case State.Eating: this.Eating();
                break;
        }
    }

    public void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
        
    }

    private void GoToNextPoint()
    {
        if (waypoints.Length == 0) return;

        agent.destination = waypoints[currentTarget].position;

        currentTarget = (currentTarget + 1) % waypoints.Length;
    }

    public void Eating()
    {
        if (isEating && !isCoolDown)
        {
            StartCoroutine(WidowEatCooldown());
            isCoolDown = true;
        }
    }

    public void GoPatrolling()
    {
        this.currentState = State.Patrolling;
    }

    public void GoEating()
    {
        isEating = true;
        this.currentState = State.Eating;
    }

    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitWidow playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitWidow>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnWidowHit(other, this);
        }
    }

    //private void Patrol()
    //{
    //    if (Vector3.Distance(waypoints[currentTarget].transform.position, gameObject.transform.position) <= 0)
    //    {
    //        currentTarget++;

    //        if (currentTarget >= waypoints.Length)
    //        {
    //            currentWP = waypoints[0];
    //        }
    //    }

    //    thisAgent.SetDestination(waypoints[currentTarget].transform.position);

    //    if (thisAgent.pathStatus == NavMeshPathStatus.PathComplete)
    //    {
    //        isMoving = false;
    //    }
    //}

    private IEnumerator WidowEatCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);

        isEating = false;
        isCoolDown = false;
        GoPatrolling();
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Ground") && (isOnFloor == false))
        {
            agent.enabled = false;
            agent.enabled = true;
            isOnFloor = true;
        }
    }
}
