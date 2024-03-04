using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    float timeLeft = 60f; 

    bool isFlickering = false;
    bool hasStunFlickered = false;

    public GameObject winscreen;
    
    public GameObject smoke;

    public int vile = 0;
    public int vialsRemaining = 5;
    public GameObject objectiveTextUI;
    public GameObject objectiveTextNumberUI;
    
    TextMeshProUGUI objectiveText;
    TextMeshProUGUI objectiveTextNumber;

    public GameObject gameOverScreen, pauseScreen;

    //string formattedString = string.Format("Objective: Collect {0} vials", vialsRemaining);
    //string vialsLeftText = $"Objective: Collect {vialsRemaining} vials";
    string vialsLeftText;

    public string escapeText = "Escape to the entrance";

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
        on = false;
        audio = GetComponent<AudioSource>();
        objectiveTextNumber = objectiveTextNumberUI.GetComponent<TextMeshProUGUI>();
        objectiveText = objectiveTextUI.GetComponent<TextMeshProUGUI>();
        string vialsLeftText = $"Objective: Collect {vialsRemaining} vials";
    }

    void Update()
    { 
        if (vialsRemaining > 1)
        {
            vialsLeftText = $"Objective: Collect {vialsRemaining} vials";
            objectiveText.text = vialsLeftText;
        }
        else if (vialsRemaining == 1)
        {
            vialsLeftText = $"Objective: Collect {vialsRemaining} vial";
            objectiveText.text = vialsLeftText;
        }
        else if (vialsRemaining <= 0)
        {
            //objectiveTextNumberUI.SetActive(false);
            objectiveText.text = escapeText;
        }

        RaycastHit hit1;
        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray1, out hit1, rayDistancePickUp))
        {
            if ((hit1.collider.tag == "Battery" && (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("PickUp")) && batterylife.fillAmount <= 0.75f))
            {
                Destroy(hit1.collider.gameObject);
                timeLeft += 30f; 
                audio.clip = batterysound;
                audio.Play();
                UpdateBatteryLifeUI();
            }
            else if ((hit1.collider.tag == "Vile" && (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("PickUp"))))
            {
                vile = vile + 1;
                Debug.Log(vile);
                Destroy(hit1.collider.gameObject);
                
                audio.clip = vilesound;
                audio.Play();
                vialsRemaining -= 1;
            }
        }


        if ((Input.GetKeyUp(KeyCode.F) || Input.GetButtonDown("ToggleSwitch") && timeLeft > 0) && gameOverScreen.activeSelf == false && pauseScreen.activeSelf == false)
        {
            ToggleFlashlight();
        }

        if (on && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

          
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
                        Destroy(hit.collider.gameObject);
                        GameManager.enemyPresent = false;
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
        timeLeft -= 30; 
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

     private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Win" && vile == 5)
        {
            winscreen.SetActive(true);
            Debug.Log("WINNER");

        }
     }
}