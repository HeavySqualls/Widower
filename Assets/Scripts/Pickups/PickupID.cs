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

    private GameManager gMan;
    private PickupController puCon;

    void Start()
    {
        puCon = GetComponentInParent<PickupController>();
        gMan = Toolbox.GetInstance().GetGameManager();

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
        puCon.pCon.playerManager.pointPickups += scoreValPoints;
        if (gMan.currentLevel != 2)
        {
            puCon.pCon.pUI.UpdateNextStats();
        }

    }    

    public void AddOrangeStats()
    {
        Debug.Log("Add grey player stats");
        puCon.pCon.playerManager.eatPickups += scoreValEatSpeed;
        if (gMan.currentLevel != 2)
        {
            puCon.pCon.pUI.UpdateNextStats();
        }
    }

    public void AddBlueStats()
    {
        Debug.Log("Add grey player stats");
        puCon.pCon.playerManager.movePickups += scoreValMoveSpeed;
        if (gMan.currentLevel != 2)
        {
            puCon.pCon.pUI.UpdateNextStats();
        }
    }
}
