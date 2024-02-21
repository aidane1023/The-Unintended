using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject creaturePrefab;
    public static bool enemyPresent = false;
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
        index = Random.Range(0, spawnPoints.Count);
        Vector3 spawnLocation = (spawnPoints[index].position);

        Instantiate(creaturePrefab, spawnLocation, Quaternion.identity);
    }
}
