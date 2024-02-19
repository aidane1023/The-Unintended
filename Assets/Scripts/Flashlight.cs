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

    float rayDistance = 10f;
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = !light.enabled;
        on = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            light.enabled = !light.enabled;
            on = true;
        }

        else if (on == true && Input.GetMouseButtonUp(1))
        {
            light.intensity = 4;
            light.spotAngle = 50;
           
            StartCoroutine(FlickerLight(totalDuration));
            
            
        }
    }

    private IEnumerator FlickerLight(float duration)
    {
        float endTime = Time.time + duration;
        while(Time.time < endTime){
        light.enabled = !light.enabled;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.tag == "Creature")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            yield return new WaitForSeconds(flickerDuration);}
        

        light.enabled = false;
        light.intensity = 2;
        light.spotAngle = 40;
        on = false;
    }
}
