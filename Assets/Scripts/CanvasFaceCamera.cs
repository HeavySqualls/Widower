using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera currentCamera;
    public PickupController pickupController;

    void Start()
    {
    }

    void Update()
    {
        AssignCamera();

        transform.LookAt(
            transform.position + 
            currentCamera.transform.rotation * Vector3.forward, 
            currentCamera.transform.rotation * Vector3.up
            );        
    }

    void AssignCamera()
    {
        currentCamera = pickupController.cameraTarget;
    }
}
