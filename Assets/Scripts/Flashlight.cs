using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    bool on;

    public float flickerDuration = 0.1f;

    public float totalDuration = 1f;
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
            light.intensity = 10;
            StartCoroutine(FlickerLight(totalDuration));
        }
    }

    private IEnumerator FlickerLight(float duration)
    {
        float endTime = Time.time + duration;
        while(Time.time < endTime){
        light.enabled = !light.enabled;
        yield return new WaitForSeconds(flickerDuration);}

        light.enabled = false;
        light.intensity = 1;
        on = false;
    }
}
