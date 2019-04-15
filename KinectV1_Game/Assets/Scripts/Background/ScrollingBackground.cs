using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is responsible to controll moving background.
public class ScrollingBackground : MonoBehaviour
{
    //Declare variables 
    public float bgSpeed; //Background speed
    public Renderer bgRend; //Bacground renderer

    // Update is called once per frame
    void Update()
    {
        // Gets component from object which script is assigned to, assigne offset to object texture
        bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
    }// End of Update 
}// End of ScrollingBackground class
