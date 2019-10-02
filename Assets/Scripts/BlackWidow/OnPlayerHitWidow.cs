using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHitWidow : WidowInteractable
{
    public override void OnWidowHit(Collision hit, WidowController widow)
    {
        if (hit.gameObject.GetComponent<Player_Controller>() && !widow.isEating)// || hit.gameObject.GetComponent<Player_2_Controller>() )
        {
            //print("Widow hit " + hit.gameObject.GetComponent<Player_Controller>().playerManager);
            hit.gameObject.GetComponent<Player_Controller>().playerManager.DisplayScore();
            widow.GoEating();
        }      
    }
}
