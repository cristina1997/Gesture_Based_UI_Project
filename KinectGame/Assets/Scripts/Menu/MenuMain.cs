using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
  //  public GameObject Camera;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      //  DontDestroyOnLoad(this.Camera);
        SceneManager.LoadScene(1);

    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
