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
    public GameObject pauseMenu;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        v.profile.TryGetSettings(out g);
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //Application.Quit();
            pauseMenu.SetActive(true);
        }

        if (pauseMenu.activeSelf == true)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") *  sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") *  sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -15f, 30f);
        }

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = GameObject.Find("Mutant(Clone)").transform.position;
        
        if (Vector3.Distance(currentPosition, transform.position) <= 10f)
        {
            if (g.intensity.value < 1f)
            {
                g.intensity.value = g.intensity.value + 0.2f;
            }
        }
        else
        {
            if (g.intensity.value > 0f)
            {
                g.intensity.value = g.intensity.value - 0.2f;
            }
        }
    }
}
