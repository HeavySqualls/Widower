using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class Projectile : MonoBehaviour
{
    public Transform cannonTransform;
    public float speed = 15f;

    private Rigidbody rb;
    private bool hasBall;
    private bool receiveBall;

    public bool isCollided;

    public GameObject player; //TODO fix this code to destroy the ball


    void Awake()
    {
        cannonTransform = transform.parent;

        rb = GetComponent<Rigidbody>();
        hasBall = true;

        isCollided = false;
    }

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

        if (player.transform.position == transform.position)
        {
            print("destroyball: ");

            print("targetDistance: ");
            Destroy(gameObject);
        }

        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (player.name == other.gameObject.name)
        {
            Destroy(gameObject);
        }

        //stick the ball to the wall
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.rotation = Quaternion.identity;
        isCollided = true;
    }
}
