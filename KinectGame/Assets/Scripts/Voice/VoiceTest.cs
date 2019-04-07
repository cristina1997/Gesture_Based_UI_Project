using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;



public class VoiceTest : MonoBehaviour
{
    // Waits for string
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public static bool GameIsPaused = false;
    public GameObject pausemenuUI;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    void Start()
    {
        actions.Add("stop", Pause);
        actions.Add("pause", Pause);
        actions.Add("stop game", Pause);
        actions.Add("pause game", Pause);

        actions.Add("play", Resume);
        actions.Add("play again", Resume);
        actions.Add("back", Resume);
        actions.Add("go back", Resume);
        actions.Add("resume", Resume);
        actions.Add("start", Resume);
        actions.Add("start again", Resume);

        actions.Add("quit", QuitGame);
        actions.Add("finish", QuitGame);

        //checks Array of strings
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),confidence);
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
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    // Start Pause
    private void Pause() {
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

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
