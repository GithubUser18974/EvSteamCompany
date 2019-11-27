using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {Destroy(this.gameObject);}

        else{ _instance = this;}
    }


    public AnimationCurve curve;
    public float t = 0;
     Vector3 srcPosition;
    public Vector3 dstPosition;
    bool forward = false;
    private void Start()
    {
        srcPosition = transform.position;
        dstPosition.y = transform.position.y;
        dstPosition.x = transform.position.x;
    }
    void Update()
    {
      

    }
    public void Forward(int steps)
    {
        dstPosition.z = steps;
        this.transform.position = Vector3.Lerp(srcPosition, dstPosition, t);
        t += Time.deltaTime * 0.5f; // will do the lerp over two seconds

    }
    public void TurnRight(int angle)
    {

    }
    public void TurnLeft(int angle)
    {

    }
}
