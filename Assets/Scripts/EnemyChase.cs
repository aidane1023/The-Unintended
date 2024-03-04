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

    private AudioSource audio;
    public AudioClip step;
    public AudioClip attack;

    private float footstepTimer = 0f;
    private float footstepDelay = 0.8f; 


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
        if (footstepTimer <= 0f)
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
        audio.Stop();
        audio.clip = attack;
        
        collider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        audio.Play();
        yield return new WaitForSeconds(1.867f);

        creature.isStopped = false;
        moveSpeed = stashedSpeed;
        collider.enabled = false;

        audio.clip = step;

        
    }

    void OnDestroy()
    {
        Instantiate(smoke, transform.position, transform.rotation);
        Destroy(this);
    }
}
