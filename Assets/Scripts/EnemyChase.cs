using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(StallMovement());
    }


    void Update()
    {
        Vector3 currentPosition = player.transform.position;
        creature.SetDestination(currentPosition);
    }


    IEnumerator StallMovement()
    {
        yield return new WaitForSeconds(2f);
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
