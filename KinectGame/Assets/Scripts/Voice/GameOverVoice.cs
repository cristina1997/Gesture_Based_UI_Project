using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class GameOverVoice : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public static bool GameIsPaused = false;
    public GameObject gameOverUI;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    void Start()
    {
        actions.Add("play again", Restart);
       
        actions.Add("quit", QuitGame);

        actions.Add("main menu", Back);
        actions.Add("menu", Back);

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
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        Time.timeScale = 0;
    }

    private void Back()
    {
        //Starts time
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

}
