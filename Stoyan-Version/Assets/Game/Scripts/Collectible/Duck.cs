using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{

    public Sprite mDuckSprite;

    [HideInInspector]
    public DuckManager mDuckManager = null;

    private GameObject duckManagerObject;
    private Vector3 mMovementDir = Vector3.zero;    // randomized movement direction
    private SpriteRenderer mSpriteRenderer = null;
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        duckManagerObject = GameObject.Find("DuckManager");
        mDuckManager = (DuckManager)duckManagerObject.GetComponent(typeof(DuckManager));

        mCurrentChanger = StartCoroutine(DirectionChanger());
    }

    private void OnBecameInvisible()
    {
        // turn off the game object when no longer seen by the camera
        //gameObject.SetActive(false);

        // moving the bubble back to the screen before disabling it
        transform.position = mDuckManager.GetPlanePosition();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the bubbles
        transform.position += mMovementDir * Time.deltaTime * 0.5f;

    }

    public IEnumerator DestroyDucks()
    {       

        StopCoroutine(mCurrentChanger);
        mMovementDir = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        transform.position = mDuckManager.GetPlanePosition();

        mCurrentChanger = StartCoroutine(DirectionChanger());
    }

    private IEnumerator DirectionChanger()
    {
        // the while loop runs while the game object is active
        while (gameObject.activeSelf)
        {
            // it lets the duck go th the left or right
            mMovementDir = new Vector2(Random.Range(-100, 100) * 0.01f, Random.Range(0, 100) * 0.01f);

            // generated every 5 seconds
            yield return new WaitForSeconds(3.0f);
        }
    }

}
