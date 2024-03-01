using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    public UnityEngine.AI.NavMeshAgent player;

    private float distance;
    private RaycastHit hit;
    public LayerMask mask;


    void Update()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            this.GetComponent<EnemyPath>().enabled = false;
            creature.SetDestination(player.transform.position);
            creature.speed = 4f;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            this.GetComponent<EnemyPath>().enabled = true;
            creature.speed = 2f;
        }
    }

    void Attack()
    {
        // If really close to player && Raycast hits
        // Stop Moving
        // Face player
        // Swing arm
        // If *Swinging* arm hits player, death.
    }
}
