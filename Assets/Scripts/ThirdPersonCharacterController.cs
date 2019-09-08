using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0, vert).normalized * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
