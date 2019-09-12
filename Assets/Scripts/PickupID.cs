using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupID : MonoBehaviour
{
    public bool pickupGrey = false;
    public bool pickupOrange = false;
    public bool pickupBlue = false;

    public float pickUpValue;

    public void IncrementStats()
    {
        if (pickupGrey)
        {
            AddGreyStats();
        }
        else if (pickupOrange)
        {
            AddOrangeStats();
        }
        else if (pickupBlue)
        {
            AddBlueStats();
        }
    }

    public void AddGreyStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayerManager().greyPickUps += 1;
    }

    public void AddOrangeStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayerManager().orangePickUps += 1;
    }

    public void AddBlueStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayerManager().bluePickUps += 1;
    }
}
