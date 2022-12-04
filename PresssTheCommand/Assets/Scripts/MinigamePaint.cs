using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePaint : MonoBehaviour
{
    [SerializeField] private GameObject[] m_userObjects;
    [SerializeField] private GameObject[] m_questObjects;
    [SerializeField] private bool[] m_userDatas;
    [SerializeField] private bool[] m_questDatas;
    [SerializeField] private int m_currentSelect;
    private const int m_countMax = 5;
    private int m_currentCount;
    private bool isPainting;

    // Start is called before the first frame update
    private void Start() { Initalize(); }
    private void Update()
    {
        DetectKey();
    }

    // 초기 환경 셋팅
    private void Initalize()
    {
        isPainting = false;
        m_currentSelect = 0;
        m_currentCount = 0;
        m_userObjects = new GameObject[m_countMax];
        m_questObjects = new GameObject[m_countMax];
        m_userDatas = new bool[m_countMax];
        m_questDatas = new bool[m_countMax];

        Color initColor;
        for (int count = 0; count < m_countMax; count++)
        {
            m_userDatas[count] = false;
            m_userObjects[count] = GameObject.Find(string.Format("UserCircle{0}", count));
            m_questObjects[count] = GameObject.Find(string.Format("QuestCircle{0}", count));

            if (m_userObjects[count]) m_userObjects[count].GetComponent<Button>().interactable = false;

            initColor = new Color32(255, 255, 255, 255);
            SetDeselected(count);
            SetUserColor(count, initColor);
        }
        SetQuest();
        SetSelected(m_currentSelect);
    }

    public void RestartMinigame()
    {
        Initalize();
    }

    private void SetQuest()
    {
        for (int count = 0; count < m_countMax; count++)
        {
            Color color;
            int random = GameManager.Instance.Random(1, 10);

            if (random % 2 == 0)
            {
                m_questDatas[count] = false;
                color = new Color32(255, 255, 255, 255);
            }
            else
            {
                m_questDatas[count] = true;
                color = new Color32(0, 0, 0, 255);
            }
            if (m_questObjects[count]) m_questObjects[count].GetComponent<Image>().color = color;
        }
    }

    private void DetectKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PassNext();
        }
    }
    public void ButtonActivate()
    {
        isPainting = true;
        for (int count = 0; count < m_countMax; count++)
        {
            if (m_userObjects[count]) m_userObjects[count].GetComponent<Button>().interactable = true;
        }
    }

    public void ButtonClicked()
    {
        if (isPainting) 
        {
            isPainting = false;
            for (int count = 0; count < m_countMax; count++)
            {
                if (m_userObjects[count]) m_userObjects[count].GetComponent<Button>().interactable = false;
            }
        }
    }
    private void PassNext()
    {
        SetDeselected(m_currentSelect);

        if (m_currentSelect == m_countMax - 1)
        {
            if (CheckFinished())
            {
                PaintSuccess();
            }
            else 
            {
                PaintFail();
            }
            return;
        }
        else
        {
            m_currentSelect++;
        }

        SetSelected(m_currentSelect);
    }

    private void SetSelected(int count) 
    {
        if (m_userObjects[count]) 
        {
            Color activatedColor = new Color32(255, 0, 0, 255);
            m_userObjects[count].GetComponent<Image>().color = activatedColor;
        }
    }
    private void SetDeselected(int count)
    {
        if (m_userObjects[count])
        {
            Color inactivatedColor = new Color32(255, 255, 255, 100);
            m_userObjects[count].GetComponent<Image>().color = inactivatedColor;
        }
    }

    public void SetUserData()
    {
        if (!m_userDatas[m_currentSelect]) 
        {
            m_userDatas[m_currentSelect] = true;

            if (m_userObjects[m_currentSelect])
            {
                Color color = new Color32(0, 0, 0, 255);
                SetUserColor(m_currentSelect, color);
            }
        }
    }

    private void SetUserColor(int count, Color color) 
    {
        GameObject childGameObject = m_userObjects[count].transform.Find("MiniCircle").gameObject;
        if (childGameObject) childGameObject.GetComponent<Image>().color = color;
    }

    private bool CheckFinished() 
    {
        bool result = true;

        for (int count = 0; count < m_countMax; count++) 
        {
            if (m_userDatas[count] != m_questDatas[count])
            {
                result = false;
                break;
            }
        }
        return result;
    }

    private void PaintSuccess()
    {
        string titleText = "Game Clear!";
        string infoText = "종료 단축키를 이용해서 게임을 종료하세요.";
        Color successColor = new Color32(151, 210, 88, 200);

        MinigameManager.Instance.SetResult(titleText, infoText, successColor, false);
    }
    private void PaintFail()
    {
        string titleText = "Game Over!";
        string infoText = "우측 하단의 재도전 버튼을 누르세요!";
        Color failColor = new Color32(255, 13, 13, 200);

        MinigameManager.Instance.SetResult(titleText, infoText, failColor, true);
    }
}
