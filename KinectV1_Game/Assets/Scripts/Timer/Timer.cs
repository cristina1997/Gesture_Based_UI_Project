using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is responsible for setting timer to increase difficulty.
public class Timer : MonoBehaviour
{
    //Declare variables
    Text TimeCounter; //References to the time counter UI Text.
    float startTime; // The time when the user says play.
    float ellapsedTime; // The ellapsed time after the user pick play option.
    bool startCounter; // Flag to start the counter.
    int minutes; // Amount of minutes.
    int seconds; // Amount of seconds.

    // Use this for initialization
    void Start()
    {
        //Sets strat time.
        startTime = 0;
        startCounter = true;
        // Gets the Text UI component from this gameObcject
        TimeCounter = GetComponent<Text>();
    }//End of Start method.

    //Method to start the time counter.
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }// End StartTimeCounter method.

    //Funtion to stop time counter
    public void StopTimeCounter()
    {
        startCounter = false;
    }// End StopTimeCounter method.

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            //Computes the ellapsed time
            ellapsedTime += Time.deltaTime;
            //Gets the minutes
            minutes = (int)ellapsedTime / 60;
            //Gets the seconds
            seconds = (int)ellapsedTime % 60;

            //Updates the counter UI Text
            TimeCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }//End of Update method

}// End of Timer class.
