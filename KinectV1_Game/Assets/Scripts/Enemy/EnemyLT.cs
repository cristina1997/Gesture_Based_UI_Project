using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLT : MonoBehaviour
{
    
    //creates enemy manager and sets it to null
    [HideInInspector]
    public EnemyManager mEnemyManager = null;
    // game object enemy manager object
    private GameObject enemyManagerObject;
    private Vector3 mMovementDir = Vector2.right;    // randomized movement direction
    private Coroutine mCurrentChanger = null;       // changing the direction

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        //sets enemy manager object equal to .find which looks for enemy manager
        enemyManagerObject = GameObject.Find("EnemyManager");
        //sets m enemy manager equal to the script EnemyManager
        mEnemyManager = (EnemyManager)enemyManagerObject.GetComponent(typeof(EnemyManager));
        //moves the enemy to right at specified points
        mCurrentChanger = StartCoroutine(MoveRight(5.0f, 0.5f));
    }

    private void OnBecameInvisible()
    {

        // moving the enemy back to the screen before disabling it
        transform.position = mEnemyManager.GetPlanePositionLeft();
    }


    // Update is called once per frame
    void Update()
    {
        // changing the position of the enemies
        transform.position += mMovementDir * Time.deltaTime * 3f;
    }

    public IEnumerator DestroyEnemies()
    {
        // stops the move, destroys the enemies (left) within 0.1 seconds
        // changes poistion as specified in getplaneposition
        // and moves it to the right at specified points 5.0f, 0.5f
        StopCoroutine(mCurrentChanger);
        yield return new WaitForSeconds(0.1f);
        transform.position = mEnemyManager.GetPlanePositionLeft();
        mCurrentChanger = StartCoroutine(MoveRight(5.0f, 0.5f));
    }

    private IEnumerator MoveRight(float moveAmount, float waitTime)
    {
        // the while loop runs while the game object is active
        // Move left forever, could just as easily check for a certain bound like:
        // while (transform.position.x < -10.0f) {     
        while (gameObject.activeSelf)
        {
            mMovementDir = Vector2.right;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
