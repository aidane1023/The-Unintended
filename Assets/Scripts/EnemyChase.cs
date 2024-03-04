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
    private float footstepDelay = 0.8f; 


    public float moveSpeed;
    private bool inRange = false;
    private bool isAttacking = false;

    void Start()
    {
        creature.speed = 0f;
        player = GameObject.Find("Player");
        
        audio = GetComponent<AudioSource>();

        animator.SetFloat("Speed", creature.speed);

        StartCoroutine(StallMovement());
    }


    void Update()
    {
        Vector3 currentPosition = player.transform.position;
        creature.SetDestination(currentPosition);

        if(isAttacking)
        {
            inRange = true;
            creature.speed = 0f;
        }
        else
        {
            inRange = false;
            creature.speed = moveSpeed;

            if (Vector3.Distance(currentPosition, transform.position) <= 2.5f)
            {
                StartCoroutine(Attack());
            }
        }

        animator.SetBool("inRange", inRange);
    }

    void FixedUpdate()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f && audio.clip == step)
        {
            audio.Play();
            footstepTimer = footstepDelay;
        }
    }

    IEnumerator StallMovement()
    {
        yield return new WaitForSeconds(2f);
        creature.speed = moveSpeed;
        audio.clip = step;
        animator.SetFloat("Speed", moveSpeed);
    }

    IEnumerator Attack()
    {   
        Debug.Log("Distance is: "+Vector3.Distance(player.transform.position, transform.position));
        creature.SetDestination(player.transform.position);
        isAttacking = true;
        if(audio.clip == step)
        {
            audio.Stop();
            audio.clip = attack;
            audio.time = 0f;
        } 
        
        collider.enabled = true;

        yield return new WaitForSeconds(.84f);

        audio.Play();

        yield return new WaitForSeconds(1.827f);

        collider.enabled = false;

        if (Vector3.Distance(player.transform.position, transform.position) > 2.5f)
        {
            isAttacking = false;
            inRange = false;
            audio.clip = step;
        }
        else
        {
            StartCoroutine(Attack());
        }
        
    }
}
