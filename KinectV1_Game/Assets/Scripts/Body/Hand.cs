using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for detecting collision between user hand and ducks.
public class Hand : MonoBehaviour
{
    // Declare variables
    public Transform mHandMesh; // The Transform attached to this GameObject hand.
    public static bool isDuckDestroyed; // Checks if ducks gets destroy.
    public int scoreValue; // Socre value.
    private DuckManager duckManager;  // Duck manager game object.

    private void Start()
    {
        // Finds duck manager game object using tag.
        GameObject duckManagerObject = GameObject.Find("DuckManager");
        if (duckManagerObject != null)
        {
            // If the duck manager object it's not null take the component of it.
            duckManager = duckManagerObject.GetComponent <DuckManager>();
            // Set score value to 1.
            scoreValue = 1;
        }
        // If the enemy manager object it's null display in console log.
        if (duckManager == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }// End of Start method

    // Update is called once per frame
    void Update()
    {
        // We set he hand mesh posittion to follow the joint.
        mHandMesh.position = Vector3.Lerp(mHandMesh.position, transform.position, Time.deltaTime * 15.0f);
    }// End of Update method

    // Checks collision between user hand and ducks.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets component of left duck (duck which moving left direction).
        DuckLT duckLT = collision.gameObject.GetComponent<DuckLT>();
        // Gets component of right duck (duck which moving right direction).
        DuckRT duckRT = collision.gameObject.GetComponent<DuckRT>();

        // if the object is collected then give the boolean a value of true
        if ( collision.gameObject.CompareTag("DuckLT") || collision.gameObject.CompareTag("DuckRT"))
        {
            // Increase score when detecting collision between hand and duck.
            duckManager.AddScore(scoreValue);
            isDuckDestroyed = true;

            if (collision.gameObject.CompareTag("DuckLT"))
            {
                Debug.Log("COLLISION LEFT HAND");
                // Destroy left duck.
                StartCoroutine(duckLT.DestroyDucks());
            }
            if (collision.gameObject.CompareTag("DuckRT"))
            {
                Debug.Log("COLLISION RIGHT HAND");
                // Destroy right duck.
                StartCoroutine(duckRT.DestroyDucks());
            }
        }
    }// End of OnTriggerEnter2D method.

    //Don't destroy ducks.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isDuckDestroyed = false;
    }

}// End of Hand class.
