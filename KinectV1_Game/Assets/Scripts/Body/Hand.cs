using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform mHandMesh;


    public static bool isDuckDestroyed;
    public int scoreValue;
    private DuckManager duckManager;


    private void Start()
    {
         GameObject duckManagerObject = GameObject.Find("DuckManager");
        if (duckManagerObject != null)
        {          
            duckManager = duckManagerObject.GetComponent <DuckManager>();
            scoreValue = 1;
        }
        if (duckManager == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // we set he hand mesh posittion to follow the joint
        mHandMesh.position = Vector3.Lerp(mHandMesh.position, transform.position, Time.deltaTime * 15.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DuckLT duckLT = collision.gameObject.GetComponent<DuckLT>();
        DuckRT duckRT = collision.gameObject.GetComponent<DuckRT>();

        // if the object is collected then give the boolean a value of true
        if ( collision.gameObject.CompareTag("DuckLT") || collision.gameObject.CompareTag("DuckRT"))
        {
            duckManager.AddScore(scoreValue);
            isDuckDestroyed = true;

            if (collision.gameObject.CompareTag("DuckLT"))
            {
                Debug.Log("COLLISION LEFT HAND");
                StartCoroutine(duckLT.DestroyDucks());
            }
            if (collision.gameObject.CompareTag("DuckRT"))
            {
                Debug.Log("COLLISION RIGHT HAND");
                StartCoroutine(duckRT.DestroyDucks());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDuckDestroyed = false;
    }

}
