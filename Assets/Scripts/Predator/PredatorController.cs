using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorController : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitPredator playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitPredator>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnPredatorHit(other, this);
        }
    }
}
