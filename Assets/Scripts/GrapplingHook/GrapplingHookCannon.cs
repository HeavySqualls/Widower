using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHookCannon : MonoBehaviour
{

    
    private GameObject projectilePrefab;
    //private Rigidbody rb;
    public float speed = 105f;


    private GameObject player;

    private GameObject currentProjectile;

    public float reachingTime = 0.5f;//for player to reach the ball

    public bool attachToTargetWhenReached;
    
    // Start is called before the first frame update
    private void Awake()
    {
        
        //assign my prefab from the assets folder
        projectilePrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/GrapplingHook/Projectile.prefab");


        player = GameObject.Find("PlayerPrefab");
        //player.AddComponent<ReachTargetInTimeSpawner>();




    }

    
    
        
        
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            currentProjectile = Instantiate(projectilePrefab, transform);

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

        if (Input.GetKeyUp(KeyCode.M) && currentProjectile.GetComponent<Projectile>().isCollided)
        {
            ReachTargetInTime reachTargetInTime = player.AddComponent<ReachTargetInTime>();
            reachTargetInTime.SetInitialValue(currentProjectile.transform, reachingTime, attachToTargetWhenReached);
            
            
        }





        if (Input.GetKeyDown(KeyCode.R))// reload scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
}
