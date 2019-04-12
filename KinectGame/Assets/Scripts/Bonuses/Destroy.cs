using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float destoryTime = .5f;
    private float rotateSpeed = 300.0f;

    void Start()
    {
        Destroy(gameObject, destoryTime);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
