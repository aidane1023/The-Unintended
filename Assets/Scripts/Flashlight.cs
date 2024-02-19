using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    bool on;
    public Camera cam;

    public float flickerDuration = 0.1f;

    public float totalDuration = 1f;

    public float slightFlicker = 0.3f;

    float rayDistance = 10f;

    float timeLeft = 20f;

    bool hasFlickered;
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = !light.enabled;
        on = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && timeLeft > 0)
        {
            light.enabled = !light.enabled;
            if (light.enabled == true)
            {
                on = true;
            }
            else on = false;
        }

        if (on == true && Input.GetMouseButtonUp(1))
        {
            light.intensity = 4;
            light.spotAngle = 50;
            light.enabled = false;
            light.intensity = 2;
            light.spotAngle = 40;
            on = false;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Creature")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            StartCoroutine(FlickerLight(totalDuration));
            


        }

        else if (on == true && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);
            if (timeLeft <= 0)
            {
                light.enabled = false;
                on = false;
                timeLeft = 0;
                light.spotAngle = 40;
            }
            else if (timeLeft < 5f)
            {
                light.spotAngle = 25;
            }
            else if (timeLeft < 10f)
            {
                light.spotAngle = 30;
            }
            else if (timeLeft < 15f) 
            {
                light.spotAngle = 35;
            }
            
            
            
        }
        
    }

    private IEnumerator FlickerLight(float duration)
    {
        float endTime = Time.time + duration;
        while(Time.time < endTime){
        light.enabled = !light.enabled;
            
        yield return new WaitForSeconds(flickerDuration);}
        

       
    }
}
