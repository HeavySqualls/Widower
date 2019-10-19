using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHookCannon : MonoBehaviour
{


    private GameObject projectilePrefab;

    public float speed = 105f;

    private GameObject currentProjectile;

    private GameObject player;

    public float reachingTime = 0.5f;//for player to reach the ball

    public bool attachToTargetWhenReached;

    public bool isPlayerOne;

    public KeyCode grapplingHookKey;

    private void Awake()
    {
        //assign my prefab from the assets folder
        //projectilePrefab =(GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/GrapplingHook/Projectile.prefab");
        projectilePrefab = Resources.Load<GameObject>("Resources/Projectile");

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
        // Shoot projectile when button pressed down
        if (Input.GetKeyDown(grapplingHookKey))
        {
            currentProjectile = Instantiate(projectilePrefab, transform);
            currentProjectile.GetComponent<Projectile>().player = player;
        }

        // go to projectile when 
        if (Input.GetKeyUp(grapplingHookKey) && currentProjectile.GetComponent<Projectile>().isCollided)
        {
            ReachTargetInTime reachTargetInTime = player.AddComponent<ReachTargetInTime>();
            reachTargetInTime.SetInitialValue(currentProjectile.transform, reachingTime, attachToTargetWhenReached);
        }
    }
}
