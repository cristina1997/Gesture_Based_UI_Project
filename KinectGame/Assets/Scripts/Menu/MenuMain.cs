using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public GameObject TimeCounter;//References to the time counter game object

    void PlayGame()
    {
        //TimeCounter.GetComponent<Timer>().StartTimeCounter();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
