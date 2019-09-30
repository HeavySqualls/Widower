using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitWidow : WidowInteractable
{
    public override void OnWidowHit(Collision hit, WidowController widow)
    {
        if (hit.gameObject.GetComponent<Player_1_Controller>() && !widow.isEating)// || hit.gameObject.GetComponent<Player_2_Controller>() )
        {
            if (hit.gameObject.GetComponent<Player_1_Controller>().p1_Manager.p1_points >= widow.scoreToBeat)
            {
                print("Player 1 wins!");
                Toolbox.GetInstance().GetGameManager().EndGame();
            }
            else
            {
                print("Widow hit player 1");
                Toolbox.GetInstance().GetPlayer_1_Manager().DisplayScore();
                widow.GoEating();
            }
        }      
    }
}
