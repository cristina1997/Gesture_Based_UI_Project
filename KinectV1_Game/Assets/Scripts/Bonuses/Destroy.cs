using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float destoryTime = .5f;
    private float rotateSpeed = 300.0f;

    public void Kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
