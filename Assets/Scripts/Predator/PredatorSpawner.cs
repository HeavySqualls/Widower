using System.Collections;
using UnityEngine;

public class PredatorSpawner : MonoBehaviour
{
    enum State { Spawn, SpawnCooldown}
    State currentState;

    [Space]
    [Header("Spawner States:")]
    public bool predatorAlive = false;
    private bool isSpawnCoolDown = false;

    [Space]
    [Header("Spawner variables:")]

    [Tooltip("Time in between spawns.")]
    public float spawnDelay = 3f;

    [Space]
    [Header("Spawner References:")]

    [Tooltip("Set up as many spawn locations as needed. " +
            "Locations must have a second location as its child and tagged with 'PatrolEnd'.")]
    public Transform[] spawnLocations;

    [Tooltip("Drop in predator prefab type here.")]
    public GameObject predatorPrefab;

    private GameObject currentSpawned;

    private string spawnerTag;
    private GameManager gM;

    private void Start()
    {
        gM = Toolbox.GetInstance().GetGameManager();
        this.currentState = State.SpawnCooldown;
        spawnerTag = gameObject.tag;
    }

    private void Update()
    {
        if (gM.isGameStart)
        {
            switch (this.currentState)
            {
                case State.SpawnCooldown:
                    this.SpawnCoolDown();
                    break;
                case State.Spawn:
                    this.Spawn();
                    break;
            }
        }
    }

    private void Spawn()
    {
        if (spawnLocations.Length == 0)
        {
            print("No spawn points set");
            return;
        }

        int randomSpawnSpot = Random.Range(0, spawnLocations.Length);

        Transform target = spawnLocations[randomSpawnSpot];

        currentSpawned = Instantiate(
            predatorPrefab, 
            spawnLocations[randomSpawnSpot].position, 
            spawnLocations[randomSpawnSpot].rotation, 
            target
            );

        currentSpawned.GetComponent<PredatorController>().thisSpawner = spawnerTag;

        print("Spawn Predator");

        this.currentState = State.SpawnCooldown;     
    }

    private void SpawnCoolDown()
    {
        if (!predatorAlive && !isSpawnCoolDown)
        {
            StartCoroutine(ISpawnCoolDown());
            isSpawnCoolDown = true;
            predatorAlive = true;
        }
    }

    private IEnumerator ISpawnCoolDown()
    {
        print("Spawn CoolDown");

        yield return new WaitForSeconds(spawnDelay);

        isSpawnCoolDown = false;

        this.currentState = State.Spawn;
    }
}
