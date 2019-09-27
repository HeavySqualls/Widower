using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitPredator : PredatorInteractable
{
    public override void OnPredatorHit(Collision hit, PredatorController predator)
    {
        // respawn player, remove aquired stats without adding them 
        if (hit.gameObject.GetComponent<Player_1_Controller>())// || hit.gameObject.GetComponent<Player_2_Controller>() )
        {
            print("Predator hit player 1");
            hit.gameObject.GetComponent<Player_1_Controller>().predatorKilledPlayer = true;
            Toolbox.GetInstance().GetPlayer_1_Manager().DisplayScore();

        }
    }
}
