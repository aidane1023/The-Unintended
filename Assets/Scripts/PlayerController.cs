using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate ()
    {
        m_Movement.x = Input.GetAxis("Horizontal");
        m_Movement.z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(m_Movement.x, 0, m_Movement.z) * Time.deltaTime * 3f);
    }
    
}
