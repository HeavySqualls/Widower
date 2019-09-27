using UnityEngine;
using UnityEngine.AI;

public class PredatorController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitPredator playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitPredator>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnPredatorHit(other, this);
        }
    }
}
