using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowPosition : MonoBehaviour
{

    public Transform target;

    public bool useDamping; // ON or OFF damping (damping off with instantly bring the object to the target
    public float damping;

    void Update()
    {
        if (!useDamping)
        {
            gameObject.transform.position = target.transform.position;
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, damping * Time.deltaTime);
        }

    }
}
