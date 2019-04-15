using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRT : MonoBehaviour
{
    // creates enemy manager and sets it to null
    [HideInInspector]
    public EnemyManager mEnemyManager = null;
    // creates enemy manager object
    private GameObject enemyManagerObject;
    private Vector3 mMovementDir = Vector2.left;    // randomized movement direction
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        // sets enemy manager object equal to gameobject . find to look for Enemy Manager
        enemyManagerObject = GameObject.Find("EnemyManager");
        // sets mEnemyManager equal to the script EnemeyManager
        mEnemyManager = (EnemyManager)enemyManagerObject.GetComponent(typeof(EnemyManager));
        // sets mCurrentChanger equal to StartCoroutine to move it to left with points set below
        mCurrentChanger = StartCoroutine(MoveLeft(5.0f, 0.3f));
    }

    private void OnBecameInvisible()
    {
        // moving the enemy back to the screen before disabling it
        transform.position = mEnemyManager.GetPlanePositionRight();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the enemy
        transform.position += mMovementDir * Time.deltaTime * 3f;
    }

    public IEnumerator DestroyEnemies()
    {
        // destroys the enemy within second provided 0.1f
        //changes position to the .getplaneposition right and then moves it to left as specified 5.0f,0.5f
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

        //while loop moving to left with waitTime
        while (gameObject.activeSelf)
        {
            mMovementDir = Vector2.left;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
