using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Transform mLegMesh;
    public static bool isEnemyDestroyed;
    public int scoreValue;
    private EnemyManager enemyManager;
    HealthBar healthBar;


    private void Start()
    {
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        if (enemyManagerObject != null)
        {
            enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
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
        EnemyLT enemyLT = collision.gameObject.GetComponent<EnemyLT>();
        EnemyRT enemyRT = collision.gameObject.GetComponent<EnemyRT>();
        //healthBar = collision.gameObject.GetComponent<HealthBar>();
        healthBar = FindObjectOfType<HealthBar>();

        // if the object is collected then give the boolean a value of true
        // if the object is collected then give the boolean a value of true
        if (collision.gameObject.CompareTag("EnemyRT") || collision.gameObject.CompareTag("EnemyLT"))
        {
            isEnemyDestroyed = true;

            if (collision.gameObject.CompareTag("EnemyLT"))
            {
                //healthBar.showHealth();
                healthBar.DealDamage(5);
                StartCoroutine(enemyLT.DestroyEnemies());
            }
            if (collision.gameObject.CompareTag("EnemyRT"))
            {
                healthBar.DealDamage(5);
                StartCoroutine(enemyRT.DestroyEnemies());
            }
        }
        else
            return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyDestroyed = false;
    }

}