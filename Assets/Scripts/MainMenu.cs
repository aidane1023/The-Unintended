using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public AudioSource buttonSound;
    bool pressed;
    bool exit;
    float pressedTimer;
    float pressedTime = 0.5f;

    //float loadTimer;
    //float loadTime = 5.0f;

    public int mainMenuScene;
    public int mainScene;
    //int level2Scene = 2;
    //int creditsScene = 3;

    int selectedLevel;
    private void Update()
    {
        if(pressed == true)
        {
            pressedTimer -= Time.deltaTime;
            if ((pressedTimer < 0) && (exit == true))
            {
                Application.Quit();
                print("Exit Success");
            }
            else if(pressedTimer < 0)
            {
                SceneManager.LoadSceneAsync(selectedLevel);
            }

            if((pressedTimer < 0) && (exit == true))
            {
                Application.Quit();
                print("Exit Success");
            }
        }
    }
    public void StartGame()
    {
        selectedLevel = mainScene;
        if (pressed != true)
        {
            //buttonSound.Play(); 
            pressedTimer = pressedTime;
            pressed = true;
        }
    }

    public void ExitGame()
    {
        if (pressed != true)
        {
            //buttonSound.Play(); 
            pressedTimer = pressedTime;
            exit = true;
            pressed = true;
        }
    }


}