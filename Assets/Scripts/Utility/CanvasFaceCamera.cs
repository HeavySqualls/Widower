using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera currentCamera;
    private PickupController pickupCon;

    void Start()
    {
        pickupCon = GetComponentInParent<PickupController>();
    }

    void Update()
    {
        if (pickupCon.interactObjInRange)
        {
            AssignCamera();

            transform.LookAt(
                transform.position +
                currentCamera.transform.rotation * Vector3.forward,
                currentCamera.transform.rotation * Vector3.up
                );
        }      
    }

    void AssignCamera()
    {
        if (pickupCon.cameraTarget != null)
        {
            currentCamera = pickupCon.cameraTarget;
        }
    }
}
