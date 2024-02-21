using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordinglight : MonoBehaviour
{
  public GameObject flashingObject;
    public float flashDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        while (true) 
        {
            flashingObject.SetActive(!flashingObject.activeSelf); 
            yield return new WaitForSeconds(flashDuration); 
        }
    }
}
