using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.smokeSpawned = true;
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(3f);
        GameManager.smokeSpawned = false;
        Destroy(this.gameObject);
    }
}
