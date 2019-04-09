using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Resolution[] resolutions = Screen.resolutions;

        //// Print the resolutions
        //foreach (var res in resolutions)
        //{
        //    Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
        //}
        Debug.Log(Screen.currentResolution);
    }
}
