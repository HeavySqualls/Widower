using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOWRespawn : MonoBehaviour
{
    public Vector3 respawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            other.transform.position = respawnPosition;
        }
    }
}
