using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

// This class is responsible to detect voice command using unity speech on pasue menu.
public class VoiceTest : MonoBehaviour
{
    // Declare variables
    private KeywordRecognizer keywordRecognizer; // keywordRecognizer waits for strings.
    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); // Dectionary as array of words.
    public static bool GameIsPaused = false; // Check if the game is paused
    public GameObject pausemenuUI; // Holder for pausemenu.
    public ConfidenceLevel confidence = ConfidenceLevel.Medium; // Speech confidence level.
    public float speed = 1; //Speech speed value.

    void Start()
    {
        // Adds set of strings to pasue game.
        actions.Add("pause", Pause);
        actions.Add("pause game", Pause);
        actions.Add("stop game", Pause);
        actions.Add("stop", Pause);
        // Adds set of strings to resume game.
        actions.Add("resume", Resume);
        actions.Add("continue", Resume);
        // Adds set of string to back to main menu.
        actions.Add("back", Back);
        actions.Add("go back", Back);

        //Checks Array of strings.
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

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
    }

    // Starts Pause menu.
    private void Pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Resume to game.
    private void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Back to main menu from pause.
    private void Back()
    {
        //Starts time
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

}// End of VoiceTest.
