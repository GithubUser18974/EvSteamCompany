using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed;
    private float mouseX=10;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.eulerAngles.y > 170 && transform.eulerAngles.y < 200)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X");
            transform.position = playerTransform.position;
            transform.rotation *= Quaternion.Euler(0, mouseX * rotationSpeed * Time.deltaTime, 0);
        }

    }
}
