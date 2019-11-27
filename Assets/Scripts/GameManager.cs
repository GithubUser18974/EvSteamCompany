using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MotorCategoryPanel;
    public GameObject _MianBoard;
    private static GameManager _instance;
    public enum BlockType
    {
        Forward,
        TurnLeft,
        TurnRight
    }
    public static GameManager Instance { get { return _instance; } }
    public Queue<BlockType>BlockSequentialType;
    public Queue<int> BlockSequentialValue;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    void Start()
    {
        BlockSequentialType = new Queue<BlockType>();
        BlockSequentialValue = new Queue<int>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///////////////////////////////////////////////PLAY Button
    public void BtnPlay()
    {
        print(_MianBoard.transform.childCount);
        GetBlockFromUI();
       
    }  

    public void AddBlock()
    {

    }
    public void GetBlockFromUI()
    {
        for (int i = 0; i < _MianBoard.transform.childCount; i++)
        {
            string val = _MianBoard.transform.GetChild(i).GetComponentInChildren<InputField>().text;
            print(val);
            int value = 0;
            if (val != "") {
                 value = int.Parse(val);
            }
          // BlockType typpe = _MianBoard.GetComponent<TypeOfBlock>().blocktype;
          //  BlockSequentialType.Enqueue(typpe);
            BlockSequentialValue.Enqueue(value);
        }

        for (int i = 0; i < BlockSequentialType.Count; i++)
        {
          //  print(BlockSequentialType.Dequeue());
            print(BlockSequentialValue.Dequeue());

        }

    }

    ///////////////////////////////////////////////UI
    public void ShowMotorCategoryCanvas()
    {
        DeActivateMainBoard();
        MotorCategoryPanel.SetActive(true);
    }
    public void HideMotorCategoryCanvas()
    {
        MotorCategoryPanel.SetActive(false);

    }
    public void BtnMotorCategory()
    {
        if (MotorCategoryPanel.activeSelf == true)
        {
            MotorCategoryPanel.SetActive(false);
            ActivateMainBoard();
        }
        else
        {
            DeActivateMainBoard();
            MotorCategoryPanel.SetActive(true);
        }
    }
    public void ActivateMainBoardBlockRayCaster()
    {
        _MianBoard.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void DisActivateMainBoardBlockRayCaster()
    {
        _MianBoard.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
    public void DeActivateMainBoard()
    {
        _MianBoard.SetActive(false);
    }
    public void ActivateMainBoard()
    {
        _MianBoard.SetActive(true);
    }
  
}
