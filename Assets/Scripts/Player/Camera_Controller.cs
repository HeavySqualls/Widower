using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private float rotationSpeed = 1;
    private float xRot, yRot;

    public bool isCameraMovement = true;
    private float currentCameraRotX = 0f;
    private float cameraRotLimit = 32f;
    private Camera cam;

    private Player_Controller playerController;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        playerController = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        CamControl();
    }

    void CamControl()
    {
        if (isCameraMovement)
        {
            // Calculate player rotation 
            if (playerController.isGamePad)
            {
                xRot = Input.GetAxis(playerController.controlProfile.X_Gamepad);
                yRot = Input.GetAxis(playerController.controlProfile.Y_Gamepad);
            }
            else
            {
                xRot = Input.GetAxis(playerController.controlProfile.Mouse_X);
                yRot = Input.GetAxis(playerController.controlProfile.Mouse_Y);
            }


            Vector3 _rotation = new Vector3(0f, xRot, 0f) * rotationSpeed;

            // Calculate camera rotation
            float _cameraRotationX = -yRot * rotationSpeed;

            rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));

            if (cam != null)
            {
                // set rotation and clamp it
                currentCameraRotX -= _cameraRotationX;

                currentCameraRotX = Mathf.Clamp(currentCameraRotX, -cameraRotLimit, cameraRotLimit);

                // apply rotation to the camera
                cam.transform.localEulerAngles = new Vector3(currentCameraRotX, 0, 0);
            }
        }
    }
}
