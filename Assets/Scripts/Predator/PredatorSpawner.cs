using System.Collections;
using UnityEngine;

public class PredatorSpawner : MonoBehaviour
{
    enum State { Spawn, SpawnCooldown}
    State currentState;

    [Space]
    [Header("Spawner States:")]
    public bool spawnStop = false;
    private bool isSpawning = false;

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

    private string spawnerName;
    private GameManager gM;

    private void Start()
    {
        gM = Toolbox.GetInstance().GetGameManager();
        this.currentState = State.SpawnCooldown;
        spawnerName = gameObject.name;
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
        // Check for spawners 
        if (spawnLocations.Length == 0)
        {
            print("No spawn points set");
            return;
        }

        if (!isSpawning)
        {
            int randomSpawnSpot = Random.Range(0, spawnLocations.Length);

            Transform target = spawnLocations[randomSpawnSpot];

            currentSpawned = Instantiate(
                predatorPrefab,
                spawnLocations[randomSpawnSpot].position,
                spawnLocations[randomSpawnSpot].rotation,
                target
                );

            currentSpawned.GetComponent<PredatorController>().thisSpawnerName = spawnerName;

            print("Spawn Predator");
            isSpawning = true;
        }
    }

    public void OnSpawmCoolDown()
    {
        this.currentState = State.SpawnCooldown;
    }

    public void SpawnCoolDown()
    {
        if (!spawnStop)
        {
            StartCoroutine(ISpawnCoolDown());

            spawnStop = true;
        }
    }

    private IEnumerator ISpawnCoolDown()
    {
        print("Spawn CoolDown");

        yield return new WaitForSeconds(spawnDelay);

        isSpawning = false;
        spawnStop = false;
        this.currentState = State.Spawn;
    }
}
