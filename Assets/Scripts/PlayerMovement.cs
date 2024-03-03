using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    //public Animator animator;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    AudioSource audio;

    public AudioClip walk;

    Vector3 moveDirection;
    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    float footstepTimer = 0f;
    float footstepDelay = 0.4f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        audio = GetComponent<AudioSource>();
        audio.clip = walk;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        //animator.SetFloat("Speed", moveSpeed * Mathf.Abs(horizontalInput + verticalInput));
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

       
       if ((Mathf.Abs(verticalInput) > 0 || Mathf.Abs(horizontalInput) > 0) && grounded)
{
    footstepTimer -= Time.deltaTime;
    if (footstepTimer <= 0f)
    {
        audio.Play();
        footstepTimer = footstepDelay;
    }
}

        else
        {
            footstepTimer = 0f; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something");

        if (other.tag == "Creature")
        {
            //End Game
            Debug.Log("Game End");
            Destroy(this.gameObject);
        }
    }
}
