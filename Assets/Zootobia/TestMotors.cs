using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMotors : MonoBehaviour
{
    public float speed = 25;
    Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rg.AddForce(transform.forward * speed*Time.deltaTime);
    }
}
