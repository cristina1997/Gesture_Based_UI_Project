using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckLT : MonoBehaviour
{
    // sets duck manager to null and available to drop in hierarchy
    [HideInInspector]
    public DuckManager mDuckManager = null;
    //creates private duckmanager object 
    private GameObject duckManagerObject;
    private Vector3 mMovementDir = Vector3.zero;    // randomized movement direction
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        duckManagerObject = GameObject.Find("DuckManager");
        mDuckManager = (DuckManager)duckManagerObject.GetComponent(typeof(DuckManager));
        mCurrentChanger = StartCoroutine(MoveRight(5.0f, 0.5f));
    }

    private void OnBecameInvisible()
    {
        // turn off the game object when no longer seen by the camera
        //gameObject.SetActive(false);
        // moving the duck back to the screen before disabling it
        transform.position = mDuckManager.GetPlanePositionLeft();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the ducks
        transform.position += mMovementDir * Time.deltaTime * 5f;
    }

    public IEnumerator DestroyDucks()
    {
        //destroyes the duck 
        StopCoroutine(mCurrentChanger);
        yield return new WaitForSeconds(0.1f);
        //gets position and moves it to the right
        transform.position = mDuckManager.GetPlanePositionLeft();
        mCurrentChanger = StartCoroutine(MoveRight(5.0f, 0.5f));
    }

    private IEnumerator MoveRight(float moveAmount, float waitTime)
    {
        // the while loop runs while the game object is active
        // Move left forever, could just as easily check for a certain bound like:
        // while (transform.position.x < -10.0f) {

        // moves the object to right in while loop
        while (gameObject.activeSelf)
        {
            mMovementDir = Vector2.right;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
