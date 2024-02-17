using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 playerLocation;

    void Update()
    {
        playerLocation = player.transform.position;

        agent.destination = playerLocation;

        
    }
}
