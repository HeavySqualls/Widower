using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHookCannon : MonoBehaviour
{

    private GameObject projectile;
    //private Rigidbody rb;
    public float speed = 105f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        
        //assign my prefab from the assets folder
        projectile = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/GrapplingHook/Projectile.prefab");
        //rb = projectile.GetComponent<Rigidbody>();

    }

    
    
        
        
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            Instantiate(projectile, transform);

//            GameObject newProjectile = Instantiate(projectile, transform);
//            newProjectile.transform.parent = null;
//            
//            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
//          
//            rb.isKinematic = false;
//            rb.useGravity = true;
//            rb.AddForce(transform.forward * speed, ForceMode.Impulse);

            //newProjectile.GetComponent<Projectile>().On

          

        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            
            
            
            
        }





        if (Input.GetKeyDown(KeyCode.R))// reload scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
}
