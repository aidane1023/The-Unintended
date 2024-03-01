using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPath : MonoBehaviour
{
    public List<Transform> waypoints;
    Transform currentTarget;
    private int index = 1;

    public NavMeshAgent creature;
    //private Animator animator

    private bool reverse = false;
    private bool atEnd = false;
    private bool moving = true;

    void OnEnable()
    {
        if (waypoints.Count > 0 && waypoints[0] != null)
        {
            currentTarget = waypoints[index];

            creature.SetDestination(currentTarget.position);
        }
    }

    void Update()
    {
        if (currentTarget != null)
        {
            if ((Vector3.Distance(transform.position, currentTarget.position) <= 2f) && moving)
            {
                moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
        }
    }

    void MoveToNextWaypoint()
    {
        index++;

        if (index < waypoints.Count)
        {
            currentTarget = waypoints[index];
        }
        else
        {
            index = 0;
            currentTarget = waypoints[index];
        }

        creature.SetDestination(currentTarget.position);
        moving = true;
    }
}
