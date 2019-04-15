using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is responsible to show a game over canvas.
public class GameOver : MonoBehaviour
{
    // Declare variables
    public static bool GameIsPaused = false; //Sets pause panel to false.
    public GameObject gameOverUI; // Holder for game over panel.

    // Start a game.
    public void PlayAgain()
    {
        //Loads scane with given index using scene manager.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }// End of PlayAgain method.

    // Shows Game over.
    public void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }//End EndGame method.
}//End GameOver class.
