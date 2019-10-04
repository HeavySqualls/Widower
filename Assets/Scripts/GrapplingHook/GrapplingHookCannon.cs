using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHookCannon : MonoBehaviour
{

    
    private GameObject projectilePrefab;

    public float speed = 105f;

    private GameObject currentProjectilePlayer;

    private GameObject player;

    public float reachingTime = 0.5f;//for player to reach the ball

    public bool attachToTargetWhenReached;

    public bool isPlayerOne;

    public KeyCode grapplingHookKey;
    
    // Start is called before the first frame update
    private void Awake()
    {

        //assign my prefab from the assets folder
        projectilePrefab =
            (GameObject) AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/GrapplingHook/Projectile.prefab");

        player = transform.parent.parent.gameObject;

        if (isPlayerOne)
        {
            grapplingHookKey = KeyCode.M;
        }
        else
        {
            grapplingHookKey = KeyCode.N;
        }



    }







    // Update is called once per frame
    void Update()
    {
        

        GrapplingHookFunction();
        
        
        if (Input.GetKeyDown(KeyCode.R))// reload scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }


    void GrapplingHookFunction()
    {
            if (Input.GetKeyDown(grapplingHookKey))
            {
                currentProjectilePlayer = Instantiate(projectilePrefab, transform);
                currentProjectilePlayer.GetComponent<Projectile>().player = player;
            }

            if (Input.GetKeyUp(grapplingHookKey) && currentProjectilePlayer.GetComponent<Projectile>().isCollided)
            {
                ReachTargetInTime reachTargetInTime = player.AddComponent<ReachTargetInTime>();
                reachTargetInTime.SetInitialValue(currentProjectilePlayer.transform, reachingTime, attachToTargetWhenReached);
             
            }  
        }
      
           
      

        
        
   
}
