using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible to detect collision between user foot and enemy.
public class Foot : MonoBehaviour
{
    // Declare variables
    public Transform mFootMesh; // The Transform attached to this GameObject leg.
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
    } // End of Start method.

    // Update is called once per frame
    void Update()
    {
        // We set he hand mesh posittion to follow the joint.
        mFootMesh.position = Vector3.Lerp(mFootMesh.position, transform.position, Time.deltaTime * 15.0f);
    }

    // Checks collision between foot and enemies.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets component of left enemy (snake which moving left direction).
        EnemyLT enemyLT = collision.gameObject.GetComponent<EnemyLT>();
        // Gets component of right enemy (snake which moving right direction).
        EnemyRT enemyRT = collision.gameObject.GetComponent<EnemyRT>();

        // if the object is collected then give the boolean a value of true
        // if the object is collected then give the boolean a value of true
        if (collision.gameObject.CompareTag("EnemyRT") || collision.gameObject.CompareTag("EnemyLT"))
        {
            isEnemyDestroyed = true;

            // If collision is detected between foot and left snake then destroy the enemy.
            if (collision.gameObject.CompareTag("EnemyLT"))
            {
                StartCoroutine(enemyLT.DestroyEnemies());
            }
            // If collision is detected between foot and right snake then destroy the enemy.
            if (collision.gameObject.CompareTag("EnemyRT"))
            {
                StartCoroutine(enemyRT.DestroyEnemies());
            }
        }
        else
            return;
    } // End of OnTriggerEnter2D

    // Don't destroy enemy.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyDestroyed = false;
    }// End of OnTriggerExit2D method.

}// End of Foot class.
