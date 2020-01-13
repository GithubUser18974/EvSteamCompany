using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MotorCategoryPanel;
    public GameObject _MianBoard;
    public GameObject _MainCanvas;
    public GameObject _ScoreCanvas;
    public Text _ScoreText;
    public int Score = 0;
    bool isEmpty = true;
    private static GameManager _instance;
    public enum BlockType
    {
        Forward,
        TurnLeft,
        TurnRight,
        AbsoluteForward,
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
    ///////////////////////////////////////////////PLAYer Settings
    public void ImportDataFromManager()
    {
        if (BlockSequentialType == null||BlockSequentialValue==null)
        {
            print("Fuck");
            PlayerManager.Instance.ResetCurrentType();

            return;
        }
        if (BlockSequentialValue.Count > 0)
        {
            PlayerManager.Instance._CurrentValue = BlockSequentialValue.Dequeue();
        }
        switch (BlockSequentialType.Dequeue())
        {
            case BlockType.Forward:
                {
                    PlayerManager.Instance.SetCurentToForward();
                    break;
                }
            case BlockType.TurnLeft:
                {

                    PlayerManager.Instance.SetCurentToTurnLeft();

                    break;
                }
            case BlockType.TurnRight:
                {

                    PlayerManager.Instance.SetCurentToTurnRigh();
                    break;
                }
            case BlockType.AbsoluteForward:
                {

                    PlayerManager.Instance.SetCurentToAbsoluteForward();
                    break;
                }
        }
    }
    ///////////////////////////////////////////////PLAY Button
    public void BtnPlay()
    {
        print(_MianBoard.transform.childCount);
        _MainCanvas.SetActive(false);
        _ScoreCanvas.SetActive(true);
        GetBlockFromUI();
        PlayerManager.Instance.SetPlaying(true);
        PlayerManager.Instance.SetCurrentBlockType();
       
    }  

    public void AddBlock()
    {

    }
    ///////////////////////////////////////////////UI Interaction

    public void GetBlockFromUI()
    {
        for (int i = 0; i < _MianBoard.transform.childCount; i++)
        {
            if (_MianBoard.transform.GetChild(i).GetComponentInChildren<InputField>() != null)
            {
                string val = _MianBoard.transform.GetChild(i).GetComponentInChildren<InputField>().text;
                print(val);
                int value = 0;
                if (val != "")
                {
                    value = int.Parse(val);
                }
                BlockSequentialValue.Enqueue(value);

            }

            // BlockType typpe = _MianBoard.GetComponent<TypeOfBlock>().blocktype;
            //  BlockSequentialType.Enqueue(typpe);
            switch (_MianBoard.transform.GetChild(i).tag)
            {
                case "Forward":
                    {
                     
                        BlockSequentialType.Enqueue(BlockType.Forward);
                        break;
                    }
                case "TurnRight":
                    {
                        BlockSequentialType.Enqueue(BlockType.TurnRight);

                        break;
                    }
                case "TurnLeft":
                    {
                        BlockSequentialType.Enqueue(BlockType.TurnLeft);

                        break;
                    }
                case "AbsoluteForward":
                    {
                        BlockSequentialType.Enqueue(BlockType.AbsoluteForward);
                        break;
                    }
            }
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
    public bool IsBlocksEmpty()
    {
        if (BlockSequentialType.Count==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void IncrementScore()
    {
        Score++;
        _ScoreText.text = "Score : " + Score;
    }
  public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
