using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerlight : MonoBehaviour
{
public Light flickerLight;
    private float minTimeOn = 5.0f;
    private float maxTimeOn = 20.0f;
    public float minFlickerDuration = 0.05f;
    public float maxFlickerDuration = 0.2f;

    
    void Start()
    {
        if (flickerLight == null)
        {
            flickerLight = GetComponent<Light>();
        }
        StartCoroutine(FlickerRoutine());
    }

    
    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeOn, maxTimeOn));
            flickerLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
            flickerLight.enabled = true;
        }
    }
}