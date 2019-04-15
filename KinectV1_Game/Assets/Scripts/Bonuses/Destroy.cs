using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //sets destroying time to public so user can change it , when health pick up is destroyed.
    public float destoryTime = .5f;
    //the roating speed of the health pick up 
    private float rotateSpeed = 300.0f;

    //kills method
    public void Kill()
    {   
        //destroy the game object (health pick up)
        Destroy(gameObject);
    }

    void Update()
    {   
        //rotates the object at the speed set above
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
