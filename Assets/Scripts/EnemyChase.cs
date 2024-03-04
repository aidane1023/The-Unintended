using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent creature;
    public Collider collider;
    private GameObject player;
    public Animator animator;

    private AudioSource audio;
    public AudioClip step;
    public AudioClip attack;

    private float footstepTimer = 0f;
    private float footstepDelay = 1f; 
    private bool isAttacking;


    public float moveSpeed;
    private float stashedSpeed;
    public bool inRange = false;

    void Start()
    {
        
        stashedSpeed = moveSpeed;
        creature.speed = moveSpeed;
        player = GameObject.Find("Player");
        
        audio = GetComponent<AudioSource>();

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

    void FixedUpdate()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f && !isAttacking)
        {
            audio.Play();
            footstepTimer = footstepDelay;
        }
    }

    IEnumerator StallMovement()
    {
        yield return new WaitForSeconds(2f);
        audio.clip = step;
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.45f);
            audio.clip = attack;
            audio.time = 0f;
            audio.Play();
            collider.enabled = true;
            yield return new WaitForSeconds(2.436f);
            audio.clip = step;
            creature.isStopped = false;
            moveSpeed = stashedSpeed;
            collider.enabled = false;
        }

        isAttacking = false; 
    }
}
