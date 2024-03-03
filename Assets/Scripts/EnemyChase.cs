using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    public Collider collider;
    private GameObject player;
    public Animator animator;

    public GameObject smoke;

    public float moveSpeed;
    private float stashedSpeed;
    public bool inRange = false;

    void Start()
    {
        
        stashedSpeed = moveSpeed;
        creature.speed = moveSpeed;
        player = GameObject.Find("Player");

        StartCoroutine(StallMovement());
    }


    void Update()
    {
        Vector3 currentPosition = player.transform.position;
        creature.SetDestination(currentPosition);

        if (Vector3.Distance(currentPosition, transform.position) <= 3f)
        {
            creature.isStopped = true;
            moveSpeed = 0;
            inRange = true;
            StartCoroutine(Attack());
        }
        else
        {
            inRange = false;
        }

        animator.SetBool("inRange", inRange);
        animator.SetFloat("Speed", moveSpeed);
    }

    IEnumerator StallMovement()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Attack()
    {
        collider.enabled = true;

        yield return new WaitForSeconds(2.667f);

        creature.isStopped = false;
        moveSpeed = stashedSpeed;
        collider.enabled = false;
    }

    void OnDestroy()
    {
        Instantiate(smoke, transform.position, transform.rotation);
    }
}
