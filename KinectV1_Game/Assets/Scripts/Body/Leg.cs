using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible to detect collision between user leg and enemy.
public class Leg : MonoBehaviour
{
    //Declare variables
    public Transform mLegMesh; // The Transform attached to this GameObject leg.
    public static bool isEnemyDestroyed; // Checks if enemy gets destroy.
    public int scoreValue; // Socre value.
    private EnemyManager enemyManager; // Enemy manager game object.
    HealthBar healthBar; // Helth bar to track player damage.


    private void Start()
    {
        // Finds enemy manager game object using tag.
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        if (enemyManagerObject != null)
        {
            // If the enemy manager object it's not null take the component of it.
            enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
        }

        // If the enemy manager object it's null display in console log.
        if (enemyManager == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }// End of Start method.

    // Update is called once per frame
    void Update()
    {
        // We set he hand mesh posittion to follow the joint.
        mLegMesh.position = Vector3.Lerp(mLegMesh.position, transform.position, Time.deltaTime * 15.0f);
    }// End of Update method.

    // Checks collision between leg and enemies.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets component of left enemy (snake which moving left direction).
        EnemyLT enemyLT = collision.gameObject.GetComponent<EnemyLT>();
        // Gets component of right enemy (snake which moving right direction).
        EnemyRT enemyRT = collision.gameObject.GetComponent<EnemyRT>();
        //healthBar = collision.gameObject.GetComponent<HealthBar>();
        healthBar = FindObjectOfType<HealthBar>();

        // if the object is collected then give the boolean a value of true
        // if the object is collected then give the boolean a value of true
        if (collision.gameObject.CompareTag("EnemyRT") || collision.gameObject.CompareTag("EnemyLT"))
        {
            isEnemyDestroyed = true;

            // When collides with left enemy (snake which moving left direction) then take away five points from health bar.
            if (collision.gameObject.CompareTag("EnemyLT"))
            {
                //healthBar.showHealth();
                healthBar.DealDamage(5);
                StartCoroutine(enemyLT.DestroyEnemies());
            }

            // When collides with right enemy (snake which moving right direction) then take away five points from health bar.
            if (collision.gameObject.CompareTag("EnemyRT"))
            {
                healthBar.DealDamage(5);
                StartCoroutine(enemyRT.DestroyEnemies());
            }
        }
        else
            return;
    }// End of method OnTriggerEnter2D

    // Don't destroy enemy.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyDestroyed = false;
    }

}// End of Leg class.