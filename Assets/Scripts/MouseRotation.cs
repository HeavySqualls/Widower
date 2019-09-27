using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private bool useX = false;
    [SerializeField] private bool useY = false;

    [Space]

    [SerializeField] private float speed = 3f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0f;

    [Space]

    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;

    private Vector3 lastPosition;
    private Vector3 direction;

    void Start()
    {
        lastPosition = Input.mousePosition;
    }

    void LateUpdate()
    {
        DeltaMouse();
        if (useX)
        {
            RotateX();
        }

        if (useY)
        {
            RotateY();
        }
    }

    void DeltaMouse()
    {
        if (lastPosition != Input.mousePosition)
        {
            direction = (Input.mousePosition - lastPosition).normalized;
        }
        else
        {
            direction = Vector3.zero;
        }

        lastPosition = Input.mousePosition;
    }

    void RotateX()
    {
        if (!invertX)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * speed);
        }
        else
        {
            transform.Rotate(Vector3.down * Input.GetAxis("Mouse X") * speed);
        }
    }

    void RotateY()
    {
        //new rotation
        if (!invertY)
        {
            transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * speed,Space.Self);
        }
        else
        {
            transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * speed, Space.Self);
        }

        //clamp rotation
        if (transform.rotation.eulerAngles.x < minY)
        {
            //transform.rotation = Quaternion.Euler(minY, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.localEulerAngles = new Vector3(minY, 0, 0);
        }
        else if (transform.rotation.eulerAngles.x > maxY)
        {
            //transform.rotation = Quaternion.Euler(maxY, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.localEulerAngles = new Vector3(maxY, 0, 0);
        }
    }
}
