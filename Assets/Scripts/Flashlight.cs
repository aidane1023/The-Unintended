using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    bool on;
    public Camera cam;

    public float flickerDuration = 0.1f; // How long each flicker lasts for the stunning effect
    public float totalDuration = 1f; // Total duration of the stunning flicker effect

    float rayDistance = 10f;
    float timeLeft = 40f; // Doubled battery life

    bool isFlickering = false;
    bool hasStunFlickered = false;

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false; // Ensure the light is initially off
        on = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (timeLeft > 0)
            {
                light.enabled = !light.enabled;
                on = light.enabled;
            }
        }

        if (on && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            // Adjusted conditions for the doubled battery life
            if ((timeLeft < 10f && light.spotAngle != 25) ||
                (timeLeft < 20f && timeLeft >= 10f && light.spotAngle != 30) ||
                (timeLeft < 30f && timeLeft >= 20f && light.spotAngle != 35))
            {
                if (!isFlickering)
                {
                    StartCoroutine(BatteryFlicker(timeLeft));
                }
            }

            if (Input.GetMouseButtonUp(1) && !hasStunFlickered)
            {
                StartCoroutine(FlickerLight(totalDuration));
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    if (hit.collider.tag == "Creature")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
                hasStunFlickered = true; // Ensures the flicker effect is intended
            }
            else
            {
                hasStunFlickered = false; // Allows for future flickers
            }
        }
        else if (timeLeft <= 0 && light.enabled)
        {
            light.enabled = false; // Turn off the light when the battery is depleted
            on = false;
        }
    }

    IEnumerator BatteryFlicker(float timeLeft)
    {
        isFlickering = true; // Prevent overlapping flickers

        int flickerCount = Random.Range(2, 4); // Choose randomly between 2 or 3 flickers
        for (int i = 0; i < flickerCount; i++)
        {
            light.enabled = false;
            yield return new WaitForSeconds(0.05f); // Short off duration
            light.enabled = true;
            if (i < flickerCount - 1)
            {
                yield return new WaitForSeconds(0.05f); // Short on duration
            }
        }

        // Adjust the spot angle based on the current battery level
        if (timeLeft < 10f)
        {
            light.spotAngle = 25;
        }
        else if (timeLeft < 20f)
        {
            light.spotAngle = 30;
        }
        else if (timeLeft < 30f)
        {
            light.spotAngle = 35;
        }

        isFlickering = false; // Allow flickers to occur again
    }

    IEnumerator FlickerLight(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            light.enabled = !light.enabled;
            yield return new WaitForSeconds(flickerDuration);
        }
        light.enabled = true; // Ensure the light is on after flickering
    }
}
