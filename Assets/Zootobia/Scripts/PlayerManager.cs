﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

   // public AnimationCurve curve;
    public float t = 0;
    Vector3 srcPosition;
    public Vector3 dstPosition;
    public int _CurrentValue = 0;
    public bool isPlaying = false;
    Rigidbody _PlayerRigidBody;
    public float movementPower = 30f;
    public float rotationPower = 10f;
    bool canMove = true;

    /// <summary>
    /// ///////////////////////////////////
    /// </summary>

    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {Destroy(this.gameObject);}

        else{ _instance = this;}
    }


    public enum BlockTypes
    {
        None,
        Forward,
        TurnLeft,
        TurnRight,
        AbsoluteForward,
    }

   public BlockTypes _CurrentBlockType=BlockTypes.None;
    private void Start()
    {
        srcPosition = transform.position;
        dstPosition.y = transform.position.y;
        dstPosition.x = transform.position.x;
        _PlayerRigidBody = GetComponent<Rigidbody>();

    }
    void Update()
    {

        if (isPlaying)
        {
            switch (_CurrentBlockType)
            {
                case BlockTypes.Forward:
                    {
                        
                            Forward(_CurrentValue);//+(int)srcPosition.z
                        
                        break;
                    }
                case BlockTypes.TurnLeft:
                    {
                       
                            TurnLeft(_CurrentValue);
                        
                        break;
                    }
                case BlockTypes.TurnRight:
                    {
                     
                            TurnRight(_CurrentValue);
                        
                        break;
                    }
                case BlockTypes.AbsoluteForward:
                    {
                        AbsoluteForward();
                        break;
                    }
            }
        }
    }
    public void Forward(int steps)
    {
        print("Forward");
        //        dstPosition.z = steps;
        if (_PlayerRigidBody.velocity == Vector3.zero)
        {
            _PlayerRigidBody.velocity = transform.forward * movementPower * Time.deltaTime;
        }
        if (Check_Forward_Constrains(steps))
        {
            _PlayerRigidBody.velocity = Vector3.zero;
            srcPosition = transform.position;
            SetCurrentBlockType();
            t = 0;
        }
        #region Deprecated 
        //// this.transform.localPosition = Vector3.MoveTowards(transform.position, dstPosition,0.02f);
        //// this.transform.position = Vector3.Lerp(srcPosition, dstPosition, t);
        ////    t += Time.deltaTime * 0.3f; // will do the lerp over two seconds

        // if (Mathf.Abs(Mathf.Abs( transform.position.z)- Mathf.Abs(srcPosition.z) )>=  steps  ||  Mathf.Abs( Mathf.Abs(transform.position.x)- Mathf.Abs(srcPosition.x))>=steps)
        // {
        //     //print("ZZZ   " + Mathf.Abs(Mathf.Abs(transform.position.z) - Mathf.Abs(srcPosition.z)));
        //     //print("XXX   " + Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(srcPosition.x)));
        //     _PlayerRigidBody.velocity = Vector3.zero;
        //     srcPosition = transform.position;
        //     SetCurrentBlockType();
        //     t = 0;
        // }
        #endregion

    }



    public bool Check_Forward_Constrains(int steps)
    {
        int x = (int)Mathf.Abs(transform.position.x - srcPosition.x);
        int z = (int)Mathf.Abs(transform.position.z - srcPosition.z);
        int res =(int) Mathf.Abs(Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)));
        if (res == steps) { return true; }
        return false;       
    }

    public void AbsoluteForward()
    {
        _PlayerRigidBody.velocity = transform.forward * movementPower * Time.deltaTime;
       // SetCurrentBlockType();
    }

    public void TurnRight(int angle)
    {
        print("Right");
        transform.localRotation = Quaternion.RotateTowards(transform.rotation,
       Quaternion.Euler(transform.rotation.x, transform.rotation.y + angle, transform.rotation.z), rotationPower * Time.deltaTime);
        if (Mathf.Abs(this.transform.eulerAngles.y) >= Mathf.Abs((Mathf.Abs(transform.rotation.y) + angle)))
        {
            SetCurrentBlockType();
        }

    }


    public void Check_Right_Constrains(int angle)
    {

    }

    public void TurnLeft(int angle)
    {
        print("Left");
        // transform.eulerAngles = new Vector3(transform.localRotation.x, -angle, transform.localRotation.z);
        transform.localRotation = Quaternion.RotateTowards(transform.rotation,
       Quaternion.Euler(transform.rotation.x, transform.rotation.y - angle, transform.rotation.z), rotationPower * Time.deltaTime);
        if (this.transform.eulerAngles.y <= transform.rotation.y - angle)
        {
            SetCurrentBlockType();
        }


    }
    ////////////////////////////////Setter
    public void SetCurentToForward()
    {
        _CurrentBlockType = BlockTypes.Forward;
    }
    public void SetCurentToTurnLeft()
    {
        _CurrentBlockType = BlockTypes.TurnLeft;
    }
    public void SetCurentToTurnRigh()
    {
        _CurrentBlockType = BlockTypes.TurnRight ;
    }
    public void SetCurentToAbsoluteForward()
    {
        _CurrentBlockType = BlockTypes.AbsoluteForward;
    }
    public void SetPlaying(bool what)
    {
        isPlaying = what;
    }
    public void ResetCurrentType()
    {
        _CurrentBlockType = BlockTypes.None;

    }
    /////////////////////Time To Play
    public void SetCurrentBlockType()
    {
        if (GameManager.Instance.IsBlocksEmpty())
        {
            ResetCurrentType();
            return;
        }
        else
        {
            GameManager.Instance.ImportDataFromManager();
        }
    }
}
