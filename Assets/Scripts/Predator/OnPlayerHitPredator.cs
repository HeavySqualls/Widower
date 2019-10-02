using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitPredator : PredatorInteractable
{
    public override void OnPredatorHit(Collision hit, PredatorController predator)
    {
        // respawn player, remove aquired stats without adding them 
        if (hit.gameObject.GetComponent<Player_Controller>())// || hit.gameObject.GetComponent<Player_2_Controller>() )
        {
            //print("Predator hit " + +hit.gameObject.GetComponent<Player_Controller>().playerManager);
            hit.gameObject.GetComponent<Player_Controller>().predatorKilledPlayer = true;
            hit.gameObject.GetComponent<Player_Controller>().playerManager.DisplayScore();
        }
    }
}
