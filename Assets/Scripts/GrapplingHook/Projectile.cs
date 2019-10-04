﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class Projectile : MonoBehaviour
{
   
    //TODO no clues 
    
    public Transform cannonTransform;
    public float speed = 15f;

    private Rigidbody rb;
    private bool hasBall;
    private bool receiveBall;

    public bool isCollided; 
   // public float targetDistance;
   // public float distanceBetweenObjects;

    public GameObject player; //TODO fix this code to destroy the ball

    // Start is called before the first frame update
    void Awake()
    {

        cannonTransform = transform.parent;
        
        rb = GetComponent<Rigidbody>();
        hasBall = true;

        isCollided = false;
        //player = transform
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBall == true)
        {
            transform.parent = null;
            hasBall = false;
            receiveBall = false;

            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(cannonTransform.forward * speed, ForceMode.Impulse); // change mass for power
      
        }



        if (hasBall) // This solution works but it nullifies the sequence of the ball coming towards you, maybe I can find a way to get the game to come back slowly.
        {
            Vector3 myPos = cannonTransform.position;

            gameObject.transform.position = myPos;
        }

        if (receiveBall)
        {
            if (Vector3.Distance(transform.position, cannonTransform.transform.position) < 0.25f)
            {
                hasBall = true;
                receiveBall = false;
            }
        }

       // float currentDistance = Vector3.Distance(cannonTransform.position, transform.position);
        
        if ( player.transform.position == transform.position)
        {
            
            print("destroyball: ");
            
            print("targetDistance: ");
            Destroy(gameObject);
            
            
        }

        


    }

    private void OnCollisionEnter(Collision other)
    {
        if (player.name == other.gameObject.name)
        {
            print("#####################");
            Destroy(gameObject);
        }


        //stick the ball to the wall
        rb.isKinematic = true;
        rb.useGravity = false;

        isCollided = true;

        //calculate the ball 

        //distanceBetweenObjects = Vector3.Distance(cannonTransform.position, transform.position);

        //targetDistance = (distanceBetweenObjects * 90) / 100;


    }
}
