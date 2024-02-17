using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    public UnityEngine.AI.NavMeshAgent player;

    void Update()
    {

        creature.SetDestination(player.transform.position);
    }
}
