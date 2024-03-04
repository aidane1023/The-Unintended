using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject creaturePrefab;
    public Transform player;
    public static bool enemyPresent = true;
    public static bool smokeSpawned = false;
    private int index = 0;
    

    void Start()
    {
        enemyPresent = true;
        SpawnEnemy();
        
    }

    void Update()
    {
        if (enemyPresent == false && smokeSpawned)
        {
            enemyPresent = true;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 playerLocation = (player.position);
        Debug.Log("Player at: "+player.position);

        index = Random.Range(0, spawnPoints.Count);
        Debug.Log("index #"+index);
        Vector3 spawnLocation = (spawnPoints[index].position);
        Debug.Log("Creature Spawn at: "+spawnLocation);

        Debug.Log("Distance: "+ Vector3.Distance(spawnLocation, playerLocation));

        if (Vector3.Distance(spawnLocation, playerLocation) >= 20f)
        {
            Instantiate(creaturePrefab, spawnLocation, Quaternion.identity);
        }
        else
        {
            SpawnEnemy();
        }
    }
}
