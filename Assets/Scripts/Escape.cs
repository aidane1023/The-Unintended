using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject pauseMenu, gameOverScreen;
    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            //Application.Quit();
            pauseMenu.SetActive(true);
        }

        if (pauseMenu.activeSelf == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
