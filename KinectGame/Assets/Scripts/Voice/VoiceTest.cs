using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;


public class VoiceTest : MonoBehaviour
{
    // Waits for string
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public static bool GameIsPaused = false;
    public GameObject pausemenuUI;
    public GameObject mainMenuUI;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    void Start()
    {
        actions.Add("stop", Pause);
        actions.Add("pause", Pause);
        actions.Add("pause game", Pause);

        actions.Add("resume", Resume);
        actions.Add("continue", Resume);

        actions.Add("back", Back);
        actions.Add("go back", Back);

        //checks Array of strings
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void OnApplicationQuit()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
            keywordRecognizer.Stop();
        }
    }

    //Recognize what was said
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    // Start Pause
    private void Pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Resume to game
    private void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Back()
    {
        //Starts time
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
