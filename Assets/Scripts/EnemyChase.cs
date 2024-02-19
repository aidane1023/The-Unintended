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

    private Vector3 direction = Vector3.forward;
    private Vector3 axis = Vector3.up;
    Vector3 rotation1;
    Vector3 rotation2;



    void Start()
    {
        Quaternion axisRotation = Quaternion.AngleAxis(15f, axis);
        rotation1 = axisRotation * direction;

        axisRotation = Quaternion.AngleAxis(-15f, axis);
        rotation2 = axisRotation * direction;
    }


    void Update()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);

            this.GetComponent<EnemyPath>().enabled = false;
            creature.SetDestination(player.transform.position);
            creature.speed = 4f;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(rotation1), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(rotation1) * hit.distance, Color.yellow);

            this.GetComponent<EnemyPath>().enabled = false;
            creature.SetDestination(player.transform.position);
            creature.speed = 4f;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(rotation2), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(rotation2) * hit.distance, Color.yellow);

            this.GetComponent<EnemyPath>().enabled = false;
            creature.SetDestination(player.transform.position);
            creature.speed = 4f;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(rotation1) * 1000, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(rotation2) * 1000, Color.white);

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
