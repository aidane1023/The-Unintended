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
        distance = Vector3.Distance(creature.transform.position, player.transform.position);
        
        
        //Circuit(); 
        if(distance <= 5f)
        {
            this.GetComponent<EnemyPath>().enabled = false;
            //this.GetComponent("EnemyPath").enabled = false;
            creature.SetDestination(player.transform.position);
            creature.speed = 4f; 
        }
        else 
        {
            this.GetComponent<EnemyPath>().enabled = true;
            //this.GetComponent("EnemyPath").enabled = true;
            creature.speed = 2f;
        }
    }
}
