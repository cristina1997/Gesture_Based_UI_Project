using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for detecting collision between user body and bonuses.
public class FullBody : MonoBehaviour
{
    //Declare variables
    HealthBar health; // Helth bar game object
    public int bonusAmount = 10; // Value for bonuses.

    // Checks collision between body and bonuses object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy destroy = collision.gameObject.GetComponent<Destroy>();       
        health = FindObjectOfType<HealthBar>();

        // If bonus was picked then increase health bar.
        if (collision.gameObject.CompareTag("Bonus"))
            {
            //Debug.Log("Picked Up");

            destroy.GetComponent<Destroy>().Kill();
            //health.showHealth();
            health.AddHealth(10);
        }

    }// End of OnTriggerEnter2D method

}// End of FullBody class.
