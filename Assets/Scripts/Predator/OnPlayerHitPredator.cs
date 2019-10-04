using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitPredator : PredatorInteractable
{
    public override void OnPredatorHit(Collision hit, PredatorController predator)
    {


        if (hit.gameObject.GetComponent<Player_Controller>())
        {
            Player_Controller playerController = hit.gameObject.GetComponent<Player_Controller>();

            if (playerController.isEating == true)
            {
                playerController.isEating = false;
            }

            playerController.predatorKilledPlayer = true;
            playerController.playerManager.DisplayScore();
        }
    }
}
