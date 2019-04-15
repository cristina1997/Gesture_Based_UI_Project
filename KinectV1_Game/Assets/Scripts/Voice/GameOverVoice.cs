using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

// This class is responsible to detect voice command using unity speech on game over menu.
public class GameOverVoice : MonoBehaviour
{
    // Declare variables
    private KeywordRecognizer keywordRecognizer; // keywordRecognizer waits for strings.
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();// Dectionary as array of words.
    public static bool GameIsPaused = false; // Check if the game is paused.
    public GameObject gameOverUI; // Holder for game over.
    public ConfidenceLevel confidence = ConfidenceLevel.Medium; // Speech confidence level.
    public float speed = 1; //Speech speed value.

    void Start()
    {
        // Add string play again to restart the game.
        actions.Add("play again", Restart);
        // Add string quit again to quit the game.
        actions.Add("quit", QuitGame);
        // Add string main menu and menu to navigate back to main menu.
        actions.Add("main menu", Back);
        actions.Add("menu", Back);

        //Checks Array of strings
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }// End of Start method

    // Stops keywordRecognizer when quits application.
    private void OnApplicationQuit()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
            keywordRecognizer.Stop();
        }
    }//End of OnApplicationQuit method

    //Recognize what was said.
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    // Restarts game from game over screen.
    public void Restart()
    {
        Time.timeScale = 1f;
        //Loads scene with given index using scene manager.
        SceneManager.LoadScene(1);
    }
    // Quits game from game over screen.
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        Time.timeScale = 0;
    }
    // Back to main menu from game over screen.
    private void Back()
    {
        //Starts time
        Time.timeScale = 0;
        //Loads scene with given index using scene manager.
        SceneManager.LoadScene(0);
    }

}// End of GameOverVoice class.
