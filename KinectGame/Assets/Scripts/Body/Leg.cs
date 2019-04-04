using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Transform mLegMesh;
    public static bool isEnemyDestroyed;
    public int scoreValue;
    private EnemyManager enemyManager;


    private void Start()
    {
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        if (enemyManagerObject != null)
        {
            enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
            scoreValue = 1;
        }
        if (enemyManager == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // we set he hand mesh posittion to follow the joint
        mLegMesh.position = Vector3.Lerp(mLegMesh.position, transform.position, Time.deltaTime * 15.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the object is collected then give the boolean a value of true
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("LEG COLLISION");
            isEnemyDestroyed = true;
        }
        else
            return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        StartCoroutine(enemy.DestroyEnemies());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyDestroyed = false;
    }

}

