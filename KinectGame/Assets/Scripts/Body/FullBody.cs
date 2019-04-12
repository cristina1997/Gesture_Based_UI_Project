using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBody : MonoBehaviour
{
    HealthBar health;
    public int bonusAmount = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy destroy = collision.gameObject.GetComponent<Destroy>();       
        health = FindObjectOfType<HealthBar>();

        if (collision.gameObject.CompareTag("Bonus"))
            {
            //Debug.Log("Picked Up");

            destroy.GetComponent<Destroy>().Kill();
            //health.showHealth();
            health.AddHealth(10);
        }


    }

}
