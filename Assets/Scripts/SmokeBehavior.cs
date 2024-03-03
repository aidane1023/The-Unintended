using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this);
    }
}
