using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    public UnityEngine.AI.NavMeshAgent player;

    private float distance;

    void Update()
    {
        creature.SetDestination(player.transform.position);
        distance = Vector3.Distance(creature.transform.position, player.transform.position);
        //Circuit(); 
        if(distance <= 10f)
        {
            creature.speed = 5f; 
        }
        else 
        {
            creature.speed = 2f;
        }
    }
}
