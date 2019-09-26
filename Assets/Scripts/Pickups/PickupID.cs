using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupID : MonoBehaviour
{
    public bool pickupGrey = false;
    public float hValueGrey = 400f;

    public bool pickupOrange = false;
    public float hValueOrange = 400f;

    public bool pickupBlue = false;
    public float hValueBlue = 50f;

    public float pickUpValue;

    public float hValue;

    void Start()
    {
        if (pickupGrey)
        {
            hValue = hValueGrey;
        }
        else if (pickupOrange)
        {
            hValue = hValueOrange;
        }
        else if (pickupBlue)
        {
            hValue = hValueBlue;
        }
    }

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
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_pointPickups += 1;
    }

    public void AddOrangeStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_eatPickups += 1;
    }

    public void AddBlueStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_movePickups += 1;
    }
}
