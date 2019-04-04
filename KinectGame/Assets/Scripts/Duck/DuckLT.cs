using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckLT : MonoBehaviour
{
    //public Sprite mDuckSprite;

    [HideInInspector]
    public DuckManager mDuckManager = null;

    private GameObject duckManagerObject;
    private Vector3 mMovementDir = Vector3.right;    // randomized movement direction
    //private SpriteRenderer mSpriteRenderer = null;
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {
        //mSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        duckManagerObject = GameObject.Find("DuckManager");
        mDuckManager = (DuckManager)duckManagerObject.GetComponent(typeof(DuckManager));

        mCurrentChanger = StartCoroutine(MoveRight(9.0f, 0.03f));
    }

    private void OnBecameInvisible()
    {
        // turn off the game object when no longer seen by the camera
        //gameObject.SetActive(false);

        // moving the bubble back to the screen before disabling it
        transform.position = mDuckManager.GetPlanePositionLeft();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the ducks
        transform.position += mMovementDir * Time.deltaTime * 0.05f;
    }

    public IEnumerator DestroyDucks()
    {

        StopCoroutine(mCurrentChanger);

        yield return new WaitForSeconds(0.5f);

        transform.position = mDuckManager.GetPlanePositionLeft();

        mCurrentChanger = StartCoroutine(MoveRight(9.0f, 0.03f));
    }

    private IEnumerator MoveRight(float moveAmount, float waitTime)
    {
        // the while loop runs while the game object is active
        // Move left forever, could just as easily check for a certain bound like:
        // while (transform.position.x < -10.0f) {
        while (gameObject.activeSelf)
        {
            transform.position += mMovementDir * Time.deltaTime * moveAmount;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
