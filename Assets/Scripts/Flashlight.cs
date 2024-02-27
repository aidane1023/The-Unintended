 using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    Light light;
    bool on;
    public Camera cam;

    public Image batterylife;

    public float flickerDuration = 0.1f; // How long each flicker lasts for the stunning effect
    public float totalDuration = 1f; // Total duration of the stunning flicker effect

    float rayDistance = 10f;
    float rayDistancePickUp = 3f;
    float timeLeft = 40f; // Doubled battery life

    bool isFlickering = false;
    bool hasStunFlickered = false;

    int vile = 0;

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false; // Ensure the light is initially off
        on = false;
    }

    void Update()
    {
        
            RaycastHit hit1;
            Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray1, out hit1, rayDistancePickUp))
            {
                if (hit1.collider.tag == "Battery" && Input.GetKeyUp(KeyCode.E) && batterylife.fillAmount <= 0.75f)
                {
                    Destroy(hit1.collider.gameObject);
                    timeLeft += 20f;
                    UpdateBatteryLifeUI();
                }
            else if (hit1.collider.tag == "Vile" && Input.GetKeyUp(KeyCode.E)){
                Destroy(hit1.collider.gameObject);
                vile += 1;
            }
            }
       

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
            if ((timeLeft < 10f && light.spotAngle != 45) ||
                (timeLeft < 20f && timeLeft >= 10f && light.spotAngle != 50) ||
                (timeLeft < 30f && timeLeft >= 20f && light.spotAngle != 55))
            {
                if (!isFlickering)
                {
                    StartCoroutine(BatteryFlicker(timeLeft));
                }
            }

           if (Input.GetMouseButtonUp(1) && !hasStunFlickered && batterylife.fillAmount >= 0.5f)
            {
                StartCoroutine(FlickerLight(totalDuration));
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    if (hit.collider.tag == "Creature")
                    {
                        GameManager.enemyPresent = false;
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
            batterylife.fillAmount = 0f;
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
            light.spotAngle = 45;
            batterylife.fillAmount = 0.25f;
        }
        else if (timeLeft < 20f)
        {
            light.spotAngle = 50;
            batterylife.fillAmount = 0.50f;
        }
        else if (timeLeft < 30f)
        {
            light.spotAngle = 55;
            batterylife.fillAmount = 0.75f;
        }

        isFlickering = false; // Allow flickers to occur again
    }

    IEnumerator FlickerLight(float duration)
{
    timeLeft -= 20; // Subtract 20 from time left when the flicker effect is used
    UpdateBatteryLifeUI(); // Call a method to update the UI based on the new time left

    float endTime = Time.time + duration;
    while (Time.time < endTime)
    {
        light.enabled = !light.enabled;
        yield return new WaitForSeconds(flickerDuration);
    }
    light.enabled = true; // Ensure the light is on after flickering
}
void UpdateBatteryLifeUI()
{
    if (timeLeft < 10f)
        {
            light.spotAngle = 45;
            batterylife.fillAmount = 0.25f;
        }
        else if (timeLeft < 20f)
        {
            light.spotAngle = 50;
            batterylife.fillAmount = 0.50f;
        }
        else if (timeLeft < 30f)
        {
            light.spotAngle = 55;
            batterylife.fillAmount = 0.75f;
        }
        else if (timeLeft < 40f)
        {
            light.spotAngle = 60;
            batterylife.fillAmount = 1f;
        }
        else if (timeLeft >= 40f)
        {
            timeLeft = 40f;
            light.spotAngle = 60;
            batterylife.fillAmount = 1f;
        }

    }

}