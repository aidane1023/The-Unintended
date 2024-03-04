using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    Light light;
    bool on;
    public Camera cam;

    public Image batterylife;

    public AudioClip vilesound;
    public AudioClip batterysound;

    public AudioClip surge;

    public AudioClip onoff;

    AudioSource audio;

    public float flickerDuration = 0.1f;
    public float totalDuration = 1f;

    float rayDistance = 10f;
    float rayDistancePickUp = 3f;
    float timeLeft = 60f; // Adjusted to start from 60 seconds

    bool isFlickering = false;
    bool hasStunFlickered = false;

    public GameObject smoke;

    int vile = 0;

    public GameObject gameOverScreen, pauseScreen;

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
        on = false;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit1;
        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray1, out hit1, rayDistancePickUp))
        {
            if ((hit1.collider.tag == "Battery" && (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("PickUp")) && batterylife.fillAmount <= 0.75f))
            {
                Destroy(hit1.collider.gameObject);
                timeLeft += 30f; // Scaled to maintain ratio with new total time
                audio.clip = batterysound;
                audio.Play();
                UpdateBatteryLifeUI();
            }
            else if ((hit1.collider.tag == "Vile" && (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("PickUp"))))
            {
                Destroy(hit1.collider.gameObject);
                vile += 1;
                audio.clip = vilesound;
                audio.Play();
            }
        }


        if ((Input.GetKeyUp(KeyCode.F) || Input.GetButtonDown("ToggleSwitch") && timeLeft > 0) && gameOverScreen.activeSelf == false && pauseScreen.activeSelf == false)
        {
            ToggleFlashlight();
        }

        if (on && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            // Adjusted thresholds for the new total time
            if ((timeLeft < 15f && light.spotAngle != 45) ||
                (timeLeft < 30f && timeLeft >= 15f && light.spotAngle != 50) ||
                (timeLeft < 45f && timeLeft >= 30f && light.spotAngle != 55))
            {
                if (!isFlickering)
                {
                    StartCoroutine(BatteryFlicker(timeLeft));
                }
            }

            if ((Input.GetMouseButtonUp(1) || Input.GetButtonDown("Stun")) && !hasStunFlickered && batterylife.fillAmount >= 0.5f && gameOverScreen.activeSelf == false && pauseScreen.activeSelf == false)
            {
                StartCoroutine(FlickerLight(totalDuration));
                audio.clip = surge;
                audio.Play();
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    Debug.Log("Hit");
                    if (hit.collider.tag == "Creature")
                    {
                        Instantiate(smoke, hit.collider.transform.position, hit.collider.transform.rotation);
                        GameManager.enemyPresent = false;
                        Destroy(hit.collider.gameObject);
                    }
                }
                hasStunFlickered = true;
            }
            else
            {
                hasStunFlickered = false;
            }
        }
        else if (timeLeft <= 0 && light.enabled)
        {
            batterylife.fillAmount = 0f;
            light.enabled = false;
            on = false;
        }
    }

    IEnumerator BatteryFlicker(float timeLeft)
    {
        isFlickering = true;

        int flickerCount = Random.Range(2, 4);
        for (int i = 0; i < flickerCount; i++)
        {
            light.enabled = false;
            yield return new WaitForSeconds(0.05f);
            light.enabled = true;
            if (i < flickerCount - 1)
            {
                yield return new WaitForSeconds(0.05f);
            }
        }

        // Adjusted thresholds for the new total time
        if (timeLeft < 15f)
        {
            light.spotAngle = 45;
            batterylife.fillAmount = 0.25f;
        }
        else if (timeLeft < 30f)
        {
            light.spotAngle = 50;
            batterylife.fillAmount = 0.50f;
        }
        else if (timeLeft < 45f)
        {
            light.spotAngle = 55;
            batterylife.fillAmount = 0.75f;
        }

        isFlickering = false;
    }

    IEnumerator FlickerLight(float duration)
    {
        timeLeft -= 30; // Scaled to maintain ratio with new total time
        UpdateBatteryLifeUI();

        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            light.enabled = !light.enabled;
            yield return new WaitForSeconds(flickerDuration);
        }
        light.enabled = true;
    }

    void UpdateBatteryLifeUI()
    {
        // Adjusted thresholds for the new total time
        if (timeLeft < 15f)
        {
            light.spotAngle = 45;
            batterylife.fillAmount = 0.25f;
        }
        else if (timeLeft < 30f)
        {
            light.spotAngle = 50;
            batterylife.fillAmount = 0.50f;
        }
        else if (timeLeft < 45f)
        {
            light.spotAngle = 55;
            batterylife.fillAmount = 0.75f;
        }
        else if (timeLeft < 60f)
        {
            light.spotAngle = 60;
            batterylife.fillAmount = 1f;
        }
        else if (timeLeft >= 60f)
        {
            timeLeft = 60f;
            light.spotAngle = 60;
            batterylife.fillAmount = 1f;
        }
    }

    void ToggleFlashlight()
    {
        light.enabled = !light.enabled;
        audio.clip = onoff;
        audio.Play();
        on = light.enabled;
    }
}
