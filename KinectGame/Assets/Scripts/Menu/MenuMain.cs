using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public GameObject TimeCounter;//References to the time counter game object
    public GameObject menu;//Refer to main menu panel display
    public GameObject highScore;//Refer to main menu panel display
    public GameObject instructionsMenu;
    public Text high;
    public GameObject duckmg;

    void Start()
    {
        duckmg = GameObject.Find("DuckManager");
        SaveScore();
    }

    void PlayGame()
    {
        //TimeCounter.GetComponent<Timer>().StartTimeCounter();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    //On LeaderBoardButton click
    public void HighScoreButton()
    {
        //Hide Main Menu 
        GameObject.Find("MainMenu").SetActive(false);
        //Display High Scores
        highScore.SetActive(true);

    }

    public void Instructions()
    {
        GameObject.Find("MainMenu").SetActive(false);
        instructionsMenu.SetActive(true);

    }

    
    //On Back Button click in leaderboard panel
    public void BackButton()
    {
        //Display Main Menu 
        menu.SetActive(true);
        //Hide High Scores
        highScore.SetActive(false);
        instructionsMenu.SetActive(false);
    }


    void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    void SaveScore()
    {
        high.text = PlayerPrefs.GetInt("HighScore").ToString();
    } 
}
