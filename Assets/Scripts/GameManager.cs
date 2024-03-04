using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject creaturePrefab;
    public Transform player;
    public static bool enemyPresent = true;
    private int index = 0;
    

    void Start()
    {
        enemyPresent = true;
        SpawnEnemy();
    }

    void Update()
    {
        if (enemyPresent == false)
        {
            enemyPresent = true;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 playerLocation = (player.position);

        index = Random.Range(0, spawnPoints.Count);
        Vector3 spawnLocation = (spawnPoints[index].position);

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
