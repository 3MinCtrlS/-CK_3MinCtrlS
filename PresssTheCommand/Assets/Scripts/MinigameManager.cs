using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    private static MinigameManager instance = null;

    [SerializeField] private GameObject   m_minigamePopup;
    [SerializeField] private GameObject[] m_minigame;
    [SerializeField] private GameObject   m_minigameTitleText;
    [SerializeField] private bool m_minigameClear;

    private string[] m_minigameTitleString;
    private const string m_minigamePopupName = "MinigameCanvas";
    private const string m_minigamePanelName = "MinigamePanel";
    [SerializeField] private MINIGAME_TYPE m_currentMinigame;

    Animator m_minigamePopupAnimator = null;

    [SerializeField] private GameObject m_resultUI;
    [SerializeField] private GameObject m_restartButton;



    void Awake()
    {
        if (null == instance)
        {
            //instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("MinigameManager :: Make instance - Done");
        }
        else
        {
            //instance에 인스턴스가 존재한다면 자신(새로운 씬의 MinigameManager)을 삭제
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티.
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

    // 초기 환경 셋팅
    private void Initalize()
    {
        m_minigameClear = false;
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
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO] = "펜토미노";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE] = "선 자르기";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PAINT] = "알맞게 색깔 채워 넣기";
        m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_BRUSH] = "색깔놀이";

        m_minigamePopup = GameObject.Find(m_minigamePopupName);

        m_resultUI = GameObject.Find("MinigameResultUI");
        m_restartButton = GameObject.Find("MinigameRestartButton");
        if (m_resultUI) m_resultUI.SetActive(false);

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

    public void RestartMinigame()
    {
        if (m_resultUI) m_resultUI.SetActive(false);
        if (m_restartButton) m_restartButton.SetActive(false);

        StartMinigame((int)m_currentMinigame);
    }

        public void StopMinigame()
    {
        if (m_minigame[(int)m_currentMinigame]) 
        {
            m_minigame[(int)m_currentMinigame].SetActive(false);
            if (m_resultUI) m_resultUI.SetActive(false);
            if (m_restartButton) m_resultUI.SetActive(false);
        }
        m_minigamePopup.SetActive(false);
    }

    private void PlayPentamino()
    {
        Debug.Log("MinigameManager :: PlayPentamino");
        if (m_minigame[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO]) 
        {
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO].SetActive(true);
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO]
                .GetComponent<MinigamePentamino>().RestartMinigame();
        }
        m_minigameTitleText.GetComponent< TMPro.TextMeshProUGUI>()
            .SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PENTAMINO]);
    }

    private void PlayCuttingline()
    {
        Debug.Log("MinigameManager :: PlayCuttingline");
        if (m_minigame[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE])
        {
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE].SetActive(true);
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE]
                .GetComponent<MinigameCuttingLine>().RestartMinigame();
        }
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>()
            .SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_CUTTINGLINE]);

    }

    private void PlayPaint()
    {
        Debug.Log("MinigameManager :: PlayPaint");
        if (m_minigame[(int)MINIGAME_TYPE.MINIGAME_PAINT]) 
        {
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_PAINT].SetActive(true);
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_PAINT]
                .GetComponent<MinigamePaint>().RestartMinigame();
        }
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>()
            .SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_PAINT]);
    }

    private void PlayBrush()
    {
        Debug.Log("MinigameManager :: PlayBrush");
        if (m_minigame[(int)MINIGAME_TYPE.MINIGAME_BRUSH]) 
        {
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_BRUSH].SetActive(true);
            m_minigame[(int)MINIGAME_TYPE.MINIGAME_BRUSH]
                .GetComponent<MinigameBrush>().RestartMinigame();
        }
        m_minigameTitleText.GetComponent<TMPro.TextMeshProUGUI>()
            .SetText(m_minigameTitleString[(int)MINIGAME_TYPE.MINIGAME_BRUSH]);
    }

    public void SetResult(string titleText, string infoText, Color pannelBGColor , bool buttonActivate) 
    {
        if (!m_resultUI) return;
        m_resultUI.SetActive(true);

        GameObject GameResultTitle = GameObject.Find("GameResultTitle"); ;
        GameObject GameResultInformation = GameObject.Find("GameResultInformation");

        m_resultUI.GetComponent<Image>().color = pannelBGColor;
        if (GameResultTitle) GameResultTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(titleText);
        if (GameResultInformation) GameResultInformation.GetComponent<TMPro.TextMeshProUGUI>().SetText(infoText);
        if (m_restartButton) m_restartButton.SetActive(buttonActivate);
    }
}
