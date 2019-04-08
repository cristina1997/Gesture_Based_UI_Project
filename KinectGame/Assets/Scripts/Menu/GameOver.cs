using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject gameOverUI;

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
