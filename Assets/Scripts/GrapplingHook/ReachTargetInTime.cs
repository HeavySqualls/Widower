using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTargetInTime : MonoBehaviour
{
    /*The goal of that script is to reach a target is a specific time.
     * Is has the option to attach (becoming a child object) to the target transform after reaching it.
     * That script is a single use only, it will auto destroy when the task in completed.
     * No object should have it as component when the game start.
     * It should always be added by script.
     * Here is an example how to activate it:
     * ---ReachTargetInTime reachTargetInTime = gameObject.AddComponent<ReachTargetInTime>();
     * ---reachTargetInTime.SetInitialValue(target, 0.8f, true);  //("the actual target", "the wanted time to reach it", "true/false if we need to attach to the target once reached")
     */
     
    public Transform target;                //This is the target to reach.
    public bool attachToTargetWhenReached;  //This says if we need to attach or not to the target once reached.
        
    bool isReaching;                        //This tels the script if it is in progress reaching the target.
    [SerializeField]
    float reachingTime;                     //This is the time in which we must reach the target.

    float initialTime;                      //This is the time when the script is activated.
    Vector3 initialPosition;                //This is the position when the script is activated.
    Quaternion initialRotation;             //This is the rotation when the script is activated.

    Transform targetToAttach;

    //Awake is called when the script is created. Awake is called before Start, it's a good place to initialize values.
    private void Awake()
    {
        //We set isReaching to false, it will be set to true only when SetInitialValue will be called.
        isReaching = false;        
    }  

    public void SetInitialValue(Transform pTarget, float pReachingTime, bool pAttachToTargetWhenReached, Transform pTargetToAttach = null)
    {
        //This is the avtivation fucntion. Once everytnhing is setted, Updae will be able to complete the task.
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

        isReaching = true;    //We set isReaching to true so that Update can do its job.
    }

    private void Start()
    {           
        if(!isReaching)
        {
            if (target)
            {     
                SetInitialValue(target, reachingTime, attachToTargetWhenReached, target);
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {     
        //If is reaching is not set to true, Update must not be executed, so we have to safely exit the fucntion using "return"
        if (!isReaching)
            return;

        //We compute the completition ratio using our timer values.
        float ratio = (Time.time - initialTime) / reachingTime;

        //Since we don't want to overshoot, we must make sure that ratio is not greater than 1
        if(ratio > 1)
        {
            //If it is greater than one, we set it back to 1
            ratio = 1;
            //Then we set isReaching to false since there is no more need for Update to be executed.
            isReaching = false;
        }        

        //Using ratio, we set the new position between initialPosition and target.position using the ratio and the Vector3.Lerp(..) helper function.
        transform.position = Vector3.Lerp(initialPosition, target.position, ratio);
        
        //Using ratio, we set the new rotation between initialRotation and target.rotation using the ratio and the Quaternion.Slerp(..) helper function.
        transform.rotation = Quaternion.Slerp(initialRotation, target.rotation, ratio);

        //If reaching has been set to false
        if (!isReaching)
        {
            //We check of attachToTargetWhenReached feature is activated or not.
            if (attachToTargetWhenReached)
            {
                //If it is, we set targetToAttach as being our gameObject's parent.
                transform.parent = targetToAttach;
            }
            //Job done !! Now time to auto-destroy the script.
            Destroy(this);
        }
    }
}
