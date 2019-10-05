using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTargetInTime : MonoBehaviour
{
    public Transform target;                //This is the target to reach.
    public bool attachToTargetWhenReached;  //This says if we need to attach or not to the target once reached.

    bool isReaching = false;                        //This tels the script if it is in progress reaching the target.
    [SerializeField]
    float reachingTime;                     //This is the time in which we must reach the target.

    float initialTime;                      //This is the time when the script is activated.
    Vector3 initialPosition;                //This is the position when the script is activated.
    Quaternion initialRotation;             //This is the rotation when the script is activated.

    Transform targetToAttach;

    private void Start()
    {
        if (!isReaching)
        {
            if (target)
            {
                SetInitialValue(target, reachingTime, attachToTargetWhenReached, target);
            }
        }
    }

    void Update()
    {
        Move();      
    }

    public void SetInitialValue(Transform pTarget, float pReachingTime, bool pAttachToTargetWhenReached, Transform pTargetToAttach = null)
    {      
        target = pTarget;                                       //We set the target.
        reachingTime = pReachingTime;                           //We set the reachingTime.

        if (pAttachToTargetWhenReached)
            if (pTargetToAttach)
                targetToAttach = pTargetToAttach;
            else
                targetToAttach = pTarget;

        initialTime = Time.time;                                //We set the initialTime.
        initialPosition = transform.position;                   //We set the initialPosition.
        initialRotation = transform.rotation;                   //We set the initialRotation.
        attachToTargetWhenReached = pAttachToTargetWhenReached; //We set if we must attach to the target once reached or not.

        isReaching = true;
    }

    private void Move()
    {
        if (isReaching)
        {
            float ratio = (Time.time - initialTime) / reachingTime;

            if (target)
            {
                transform.position = Vector3.Lerp(initialPosition, target.position, ratio);
                transform.rotation = Quaternion.Slerp(initialRotation, target.rotation, ratio);
            }

            if (ratio > 1)
            {
                ratio = 1;

                if (attachToTargetWhenReached)
                {
                    transform.parent = targetToAttach;
                }

                Destroy(this);
            }
        }      
    }
}
