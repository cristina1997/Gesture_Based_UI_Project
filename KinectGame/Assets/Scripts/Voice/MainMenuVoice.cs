using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class MainMenuVoice : MonoBehaviour
{
    // Waits for string
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public GameObject menu;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;


    void Start()
    {
        actions.Add("play", PlayGame);
        actions.Add("start", PlayGame);
        actions.Add("new game", PlayGame);

        actions.Add("quit", QuitGame);
        actions.Add("stop game", QuitGame);
        actions.Add("stop playing", QuitGame);
        actions.Add("finish", QuitGame);

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

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
