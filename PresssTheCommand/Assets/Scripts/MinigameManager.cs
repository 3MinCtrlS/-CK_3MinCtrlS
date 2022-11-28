using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private static MinigameManager instance = null;

    [SerializeField] private GameObject   m_minigamePopup;
    [SerializeField] private GameObject[] m_minigame;
    [SerializeField] private GameObject   m_minigameTitleText;

    private string[] m_minigameTitleString;
    private const string m_minigamePopupName = "MinigameCanvas";
    private const string m_minigamePanelName = "MinigamePanel";
    [SerializeField] private MINIGAME_TYPE m_currentMinigame;

    Animator m_minigamePopupAnimator = null;



    void Awake()
    {
        if (null == instance)
        {
            //instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("MinigameManager :: Make instance - Done");
        }
        else
        {
            //instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� MinigameManager)�� ����
            Destroy(this.gameObject);
        }
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ.
    public static MinigameManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

    enum MINIGAME_TYPE
    {
        MINIGAME_NONE = 0,
        MINIGAME_PENTAMINO,
        MINIGAME_CUTTINGLINE,
        MINIGAME_PAINT, // PAINTING
        MINIGAME_BRUSH, // COLORING 
    }


    private void Start() { Initalize(); }

    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        m_currentMinigame = MINIGAME_TYPE.MINIGAME_NONE;

        m_minigame = new GameObject[5];
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO]  = GameObject.Find("MinigamePentamino");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE] = GameObject.Find("MinigameCuttingLine");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_PAINT]           = GameObject.Find("MinigamePaint");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_BRUSH]          = GameObject.Find("MinigameBrush");

        GameObject[] minigameList = GameObject.FindGameObjectsWithTag("MinigamePannel");

        foreach (GameObject minigame in minigameList)
        {
            minigame.SetActive(false);
        }

        m_minigameTitleText = GameObject.Find("MinigameNameText");
        m_minigameTitleString = new string[5];
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO] = "����̳�";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE] = "�� �ڸ���";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PAINT] = "�˸°� ���� ä�� �ֱ�";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_BRUSH] = "�������";

        m_minigamePopup = GameObject.Find(m_minigamePopupName);
        GameManager.Instance.PopupUIOff();

        //m_minigamePopupAnimator = GameObject.Find(m_minigamePanelName).GetComponent<Animator>();
        //if (m_minigamePopupAnimator) m_minigamePopupAnimator.SetTrigger("MinigameStart");
        //Debug.Log("MinigameManager :: Initalize - Done");
    }

    public void StartMinigame(int minigameType) 
    {
        m_minigamePopup.SetActive(true);
        m_currentMinigame = (MINIGAME_TYPE)minigameType;

        switch ((MINIGAME_TYPE)minigameType) 
        {
            case MINIGAME_TYPE.MINIGAME_PENTAMINO:
                PlayPentamino();
                break;
            case MINIGAME_TYPE.MINIGAME_CUTTINGLINE:
                PlayCuttingline();
                break;
            case MINIGAME_TYPE.MINIGAME_PAINT:
                PlayPaint();
                break;
            case MINIGAME_TYPE.MINIGAME_BRUSH:
                PlayBrush();
                break;
        }
    }

    public void StopMinigame() 
    {
        m_minigame[(int)m_currentMinigame].SetActive(false);
        m_minigamePopup.SetActive(false);
    }

    private void PlayPentamino() 
    {
        Debug.Log("MinigameManager :: PlayPentamino");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO].SetActive(true);
        m_minigameTitleText.GetComponent< TMPro.TextMeshProUGUI>().SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO]);


    }

    private void PlayCuttingline()
    {
        Debug.Log("MinigameManager :: PlayCuttingline");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE].SetActive(true);
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>().SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE]);

    }

    private void PlayPaint()
    {
        Debug.Log("MinigameManager :: PlayPaint");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_PAINT].SetActive(true);
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>().SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PAINT]);
    }

    private void PlayBrush()
    {
        Debug.Log("MinigameManager :: PlayBrush");
        m_minigame[(int)MINIGAME_TYPE.MINIGAME_BRUSH].SetActive(true);
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>().SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_BRUSH]);
    }

}
