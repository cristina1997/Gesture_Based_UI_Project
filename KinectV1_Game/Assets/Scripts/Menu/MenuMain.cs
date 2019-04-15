using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class is responsible for showing a main menu.
public class MenuMain : MonoBehaviour
{
    // declare variables
    public GameObject TimeCounter;//References to the time counter game object.
    public GameObject menu;//Refer to main menu panel display.
    public GameObject highScore;//Refer to main menu panel display.
    public GameObject instructionsMenu; //Holder for instruction panel.
    public Text high; // Gets high score.
    public GameObject duckmg; // Game object for duck manager.

    void Start()
    {
        // Finds duck manager game object.
        duckmg = GameObject.Find("DuckManager");
        SaveScore();
    }//End of Start method.

    // Starts game.
    void PlayGame()
    {
        //TimeCounter.GetComponent<Timer>().StartTimeCounter();
        //Loads scene with given index using scene manager.
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
    }//End of HighScoreButton method.

    // Displays instructions.
    public void Instructions()
    {
        GameObject.Find("MainMenu").SetActive(false);
        instructionsMenu.SetActive(true);
    }//End of Instructions method.

    //On Back Button click in leaderboard panel
    public void BackButton()
    {
        //Display Main Menu 
        menu.SetActive(true);
        //Hide High Scores
        highScore.SetActive(false);
        instructionsMenu.SetActive(false);
    }// End of BackButton method.

    // Quits game.
    void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    //Shows the highest score.
    void SaveScore()
    {
        high.text = PlayerPrefs.GetInt("HighScore").ToString();
    } 
}// End of Menumain class.
