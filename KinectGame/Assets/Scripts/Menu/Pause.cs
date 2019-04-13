using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausemenuUI;
    private float tempTimeScale;//Value for time scale when pause is on(stops the game)


    void Update()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }else
            {
                PauseGame();
            }
        }
    }

    void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        pausemenuUI.SetActive(!pausemenuUI.activeInHierarchy);//If active in hierrarhy deaactivate pause
        if (Time.timeScale != 0)//If it's not 0
        {
            Time.timeScale = 0;//Assigned 0 to the time scale
        }//End of if
        else
        {
            Time.timeScale = tempTimeScale;
        }//End of else
    }
}
