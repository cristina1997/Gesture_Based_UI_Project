using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

// This class is responsible to detect voice command using unity speech on main menu.
public class MainMenuVoice : MonoBehaviour
{
    // Declare variables
    private KeywordRecognizer keywordRecognizer; // keywordRecognizer waits for strings.
    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); // Dectionary as array of words.
    public GameObject menu; //Hoder for menu panel.
    public GameObject highScore;//Refer to main menu panel display
    public GameObject instructions; //Holder for instructions panel.
    //public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1; //Speech speed value.


    void Start()
    {
        // Add string play to starts game.
        actions.Add("play", PlayGame);
        // Add string quit to quit game.
        actions.Add("quit", QuitGame);
        // Add string score to show high score.
        actions.Add("score", Score);
        // Add string back to go back to main menu.
        actions.Add("back", Back);
        // Add string instructions to show instructions.
        actions.Add("instructions", Instructions);

        //Checks Array of strings
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }//End of start method.

    // Stops keywordRecognizer when quits application.
    private void OnApplicationQuit()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
            keywordRecognizer.Stop();
        }
    }// End of OnApplicationQuit method.

    //Recognize what was said.
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }//End of RecognizedSpeech method.

    // Starts game from main menu.
    public void PlayGame()
    {
        //Loads sceane with given index using scene manager.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    // Quit game from main menu.
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // Displays high score.
    public void Score()
    {
        //Display Main Menu 
        menu.SetActive(false);
        //Hide High Scores
        highScore.SetActive(true);
    }

    // Displays instructions for user.
    public void Instructions()
    {
        //Display Main Menu 
        menu.SetActive(false);
        //Hide High Scores
        instructions.SetActive(true);
    }

    // Back to main menu.
    private void Back()
    {
        //Display Main Menu 
        menu.SetActive(true);
        //Hide High Scores
        highScore.SetActive(false);
        instructions.SetActive(false);
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}//End of MainMenuVoice class.
