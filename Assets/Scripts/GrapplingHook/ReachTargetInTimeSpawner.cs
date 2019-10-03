using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTargetInTimeSpawner : MonoBehaviour
{
    public Transform target;
    public bool attachToTargetWhenReached;
    public float reachingTime;

 

    // Update is called once per frame
    void Update()
    {
//        if (Input.GetKeyUp(KeyCode.M))
//        {
//            ReachTargetInTime reachTargetInTime = gameObject.AddComponent<ReachTargetInTime>(); // Added the script (component) called ReachTargetInTime
//            reachTargetInTime.SetInitialValue(target, reachingTime, attachToTargetWhenReached);
//
//        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ReachTargetInTime reachTargetInTime = gameObject.AddComponent<ReachTargetInTime>();
            reachTargetInTime.SetInitialValue(target, reachingTime, attachToTargetWhenReached);

        }
    }
}
