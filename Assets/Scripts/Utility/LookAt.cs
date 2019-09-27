using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public bool useLateUpdate;

    void Update()
    {
        if (!useLateUpdate)
        {
            NormalUpdate();
        }
    }

    void NormalUpdate()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(target.position - gameObject.transform.position, Vector3.up);
    }

    private void LateUpdate()
    {
        if (useLateUpdate)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(target.position - gameObject.transform.position, Vector3.up);
        }
    }
}
