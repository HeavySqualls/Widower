using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupID : MonoBehaviour
{
    public bool pickupPoints = false;
    public float healthPoints = 400f;
    public int scoreValPoints = 1;

    public bool pickupEatSpeed = false;
    public float healthEatSpeed = 400f;
    public int scoreValEatSpeed = 1;

    public bool pickupMoveSpeed = false;
    public float healthMoveSpeed = 50f;
    public int scoreValMoveSpeed = 1;

    public float pickUpValue;

    public float hValue;

    void Start()
    {
        if (pickupPoints)
        {
            hValue = healthPoints;
        }
        else if (pickupEatSpeed)
        {
            hValue = healthEatSpeed;
        }
        else if (pickupMoveSpeed)
        {
            hValue = healthMoveSpeed;
        }
    }

    public void IncrementStats()
    {
        if (pickupPoints)
        {
            AddGreyStats();
        }
        else if (pickupEatSpeed)
        {
            AddOrangeStats();
        }
        else if (pickupMoveSpeed)
        {
            AddBlueStats();
        }
    }

    public void AddGreyStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_pointPickups += scoreValPoints;
    }

    public void AddOrangeStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_eatPickups += scoreValEatSpeed;
    }

    public void AddBlueStats()
    {
        Debug.Log("Add grey player stats");
        Toolbox.GetInstance().GetPlayer_1_Manager().p1_movePickups += scoreValMoveSpeed;
    }
}
