using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public PostProcessVolume v;
    private Grain g;
    private GameObject creature;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        v.profile.TryGetSettings(out g);
    }


    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 30f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = GameObject.Find("Mutant(Clone)").transform.position;
        
        if (Vector3.Distance(currentPosition, transform.position) <= 10f)
        {
            Debug.Log("Getting Closer...");
            if (g.intensity.value > 1f)
            {
                Debug.Log("Changing intensity ^");
                g.intensity.value = 1f;
            }
        }
        else
        {
            Debug.Log("Getting Further");
            if (g.intensity.value < 0f)
            {
                Debug.Log("Changing intensity -^");
                g.intensity.value = 0f;
            }
        }
    }
}
