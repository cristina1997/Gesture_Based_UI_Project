using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckRT : MonoBehaviour
{
    //public duck manager and sets it to equal to null
    [HideInInspector]
    public DuckManager mDuckManager = null;

    // creates game object (duck manager)
    private GameObject duckManagerObject;
    private Vector3 mMovementDir = Vector3.zero;    // randomized movement direction
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //sets the duck game manager object equal to duckmanager by using .find
        duckManagerObject = GameObject.Find("DuckManager");
        //gets component of DuckManager
        mDuckManager = (DuckManager)duckManagerObject.GetComponent(typeof(DuckManager));
        //sets m current changer to move to the left 
        mCurrentChanger = StartCoroutine(MoveLeft(5.0f, 0.5f));
    }

    private void OnBecameInvisible()
    {
        // turn off the game object when no longer seen by the camera
        //gameObject.SetActive(false);

        // moving the duck back to the screen before disabling it
        transform.position = mDuckManager.GetPlanePositionRight();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the ducks
        transform.position += mMovementDir * Time.deltaTime * 5f;
    }

    public IEnumerator DestroyDucks()
    {
        // stops moving the ducks and destroys them and waits with specified time 0.1f
        StopCoroutine(mCurrentChanger);
        yield return new WaitForSeconds(0.1f);
        //gets the position (right)
        transform.position = mDuckManager.GetPlanePositionRight();
        //changes the position to left at specified points given
        mCurrentChanger = StartCoroutine(MoveLeft(5.0f, 0.5f));
    }

    private IEnumerator MoveLeft(float moveAmount, float waitTime)
    {
        // the while loop runs while the game object is active
        // Move left forever, could just as easily check for a certain bound like:
        // while (transform.position.x < -10.0f) {

        //while loop moving duck to left and sets the vector equal to left
        while (gameObject.activeSelf)
        {
            mMovementDir = Vector2.left;
            yield return new WaitForSeconds(waitTime);
        }
    }

   
}
