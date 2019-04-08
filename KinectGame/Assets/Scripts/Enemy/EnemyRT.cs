using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRT : MonoBehaviour
{
    //public Sprite mDuckSprite;

    [HideInInspector]
    public EnemyManager mEnemyManager = null;

    private GameObject enemyManagerObject;
    private Vector3 mMovementDir = Vector2.left;    // randomized movement direction
    //private SpriteRenderer mSpriteRenderer = null;
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {
        //mSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyManagerObject = GameObject.Find("EnemyManager");
        mEnemyManager = (EnemyManager)enemyManagerObject.GetComponent(typeof(EnemyManager));

        mCurrentChanger = StartCoroutine(MoveLeft(5.0f, 0.5f));
    }

    private void OnBecameInvisible()
    {
        // turn off the game object when no longer seen by the camera
        //gameObject.SetActive(false);

        // moving the duck back to the screen before disabling it
        transform.position = mEnemyManager.GetPlanePositionRight();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the bubbles
        transform.position += mMovementDir * Time.deltaTime * 5f;


    }

    public IEnumerator DestroyEnemies()
    {
        StopCoroutine(mCurrentChanger);
        yield return new WaitForSeconds(0.1f);
        transform.position = mEnemyManager.GetPlanePositionRight();
        mCurrentChanger = StartCoroutine(MoveLeft(5.0f, 0.5f));
    }

    private IEnumerator MoveLeft(float moveAmount, float waitTime)
    {
        // the while loop runs while the game object is active
        // Move left forever, could just as easily check for a certain bound like:
        // while (transform.position.x < -10.0f) {
        while (gameObject.activeSelf)
        {
            mMovementDir = Vector2.left;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
