using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for showing a pause menu.
public class Pause : MonoBehaviour
{
    // Declare variables
    public static bool GameIsPaused = false;  //Sets pause panel to false at the start.
    public GameObject pausemenuUI; // Holder for pause menu.
    private float tempTimeScale; // Value for time scale when pause is on(stops the game)
    
    // Pause game using "p" key.
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                // Go back to game.
                Resume();
            }else
            {
                // Displays pause menu.
                PauseGame();
            }
        }
    }// End of Update method.

    // Resume game from pause menu.
    void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }// End of Resumemethod.

    // Display pause menu.
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
    }// End of PauseGame method.
}// End of Pause class.
