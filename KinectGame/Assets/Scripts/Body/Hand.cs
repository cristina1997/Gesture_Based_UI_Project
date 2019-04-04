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
        // if the object is collected then give the boolean a value of true
        if (!collision.gameObject.CompareTag("Duck"))
            return;
        else if (collision.gameObject.CompareTag("Duck"))
        {
            duckManager.AddScore(scoreValue);
            isDuckDestroyed = true;
        }

        Duck duck = collision.gameObject.GetComponent<Duck>();
        StartCoroutine(duck.DestroyDucks());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDuckDestroyed = false;
    }

}
