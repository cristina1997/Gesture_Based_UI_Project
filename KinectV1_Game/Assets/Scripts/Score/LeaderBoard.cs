using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible to get player hogh score.
public class LeaderBoard : MonoBehaviour
{
    public int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //use this value in whatever shows the leaderboard.
    }
}// End LeaderBoard class.
