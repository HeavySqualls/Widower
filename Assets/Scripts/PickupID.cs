using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupID : MonoBehaviour
{
    public bool pickupGrey = false;
    public bool pickupOrange = false;
    public bool pickupBlue = false;

    public float pickUpValue;

    void Start()
    {
        if (pickupGrey)
        {
            pickUpValue = Toolbox.GetInstance().GetPlayerManager().greyPickUps;
        }
        else if (pickupOrange)
        {
            pickUpValue = Toolbox.GetInstance().GetPlayerManager().orangePickUps;
        }
        else if (pickupBlue)
        {
            pickUpValue = Toolbox.GetInstance().GetPlayerManager().bluePickUps;
        }
    }

    public void AddPlayerStats()
    {
        Debug.Log("Add player stats");
        pickUpValue += 1;
    }
}
