using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitPredator : PredatorInteractable
{
    public override void OnPredatorHit(Collision hit, PredatorController predator)
    {


        if (hit.gameObject.GetComponent<Player_Controller>())
        {
            Player_Controller pCon = hit.gameObject.GetComponent<Player_Controller>();

            if (pCon.isEating == true)
            {
                pCon.isEating = false;
            }

            
            pCon.predatorKilledPlayer = true;
            pCon.playerManager.DisplayScore();
        }
    }
}
