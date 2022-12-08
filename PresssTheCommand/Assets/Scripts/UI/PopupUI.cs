using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 작성자 : 전시은
// 작성일 : 2022.11.24
// 사용 방법 : GameManager에 오브젝트들을 전달 해 주어 동작.
// 제작 사유 : UI 객체 컨트롤.
public class PopupUI : MonoBehaviour
{
    [SerializeField] private GameObject m_popupTitle; //PopupTopbarTitleText
    [SerializeField] private GameObject[] m_popupPannels;

    [SerializeField] private POPUP_TYPE m_currentPopupType;
    [SerializeField] private const int m_popupCount = 10;

    [SerializeField] private GameObject m_helpImage;
    [SerializeField] private const int m_helpCount = 14;
    [SerializeField] private int m_currentHelpCount = 1;
    [SerializeField] private Sprite[] m_helpImages;


    private string[] m_popupTitleText = null;
    //creators.txt

    enum POPUP_TYPE
    {
        POPUP_TYPE_NONE = 0,
        POPUP_TYPE_SETTING,
        POPUP_TYPE_CREATORS,
        POPUP_TYPE_EXIT,
        POPUP_TYPE_HELP,
    }

    // Start is called before the first frame update
    private void Start() { Initalize(); }


    // 초기 환경 셋팅
    private void Initalize()
    {
        m_popupTitle = GameObject.Find("PopupTopbarTitleText"); //PopupTopbarTitleText

        m_popupPannels = new GameObject[m_popupCount];
        //m_popupPannels = GameObject.FindGameObjectsWithTag("PopupContents");
        m_popupPannels[(int)POPUP_TYPE.POPUP_TYPE_SETTING] = FindPopupUIObjects(this.gameObject, "SettingPannel");
        m_popupPannels[(int)POPUP_TYPE.POPUP_TYPE_CREATORS] = FindPopupUIObjects(this.gameObject, "CreatorsPannel");
        m_popupPannels[(int)POPUP_TYPE.POPUP_TYPE_EXIT] = FindPopupUIObjects(this.gameObject, "GameExitPannel");
        m_popupPannels[(int)POPUP_TYPE.POPUP_TYPE_HELP] = FindPopupUIObjects(this.gameObject, "GameHelpPannel");

        m_popupTitleText = new string[m_popupCount];
        m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_SETTING] = "설정";
        m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_CREATORS] = "제작자";
        m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_EXIT] = "안내";
        m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_HELP] = "도움말";

        ClosePopup();
        //GameManager.Instance.PopupUIOff();
        //Debug.Log("UI :: Initalize - Done");
    }

    public GameObject FindPopupUIObjects(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public void SetPopupType(string typeName) 
    {
        switch (typeName) 
        {
            case "POPUP_TYPE_SETTING":
                m_currentPopupType = POPUP_TYPE.POPUP_TYPE_SETTING;
                break;
            case "POPUP_TYPE_CREATORS":
                m_currentPopupType = POPUP_TYPE.POPUP_TYPE_CREATORS;
                break;
            case "POPUP_TYPE_EXIT":
                m_currentPopupType = POPUP_TYPE.POPUP_TYPE_EXIT;
                break;
            case "POPUP_TYPE_HELP":
                m_currentPopupType = POPUP_TYPE.POPUP_TYPE_HELP;
                break;
            default:
                m_currentPopupType = POPUP_TYPE.POPUP_TYPE_NONE;
                break;
        }
    }

    public void OpenPopup()
    {
        this.gameObject.SetActive(true);
        if (m_popupPannels[(int)m_currentPopupType])
            m_popupPannels[(int)m_currentPopupType].SetActive(true);

        switch (m_currentPopupType)
        {
            case POPUP_TYPE.POPUP_TYPE_SETTING:
                m_popupTitle.GetComponent<TMPro.TextMeshProUGUI>()
                    .SetText(m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_SETTING]);
                break;
            case POPUP_TYPE.POPUP_TYPE_CREATORS:
                m_popupTitle.GetComponent<TMPro.TextMeshProUGUI>()
                    .SetText(m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_CREATORS]);
                break;
            case POPUP_TYPE.POPUP_TYPE_EXIT:
                m_popupTitle.GetComponent<TMPro.TextMeshProUGUI>()
                    .SetText(m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_EXIT]);
                break;
            case POPUP_TYPE.POPUP_TYPE_HELP:
                m_popupTitle.GetComponent<TMPro.TextMeshProUGUI>()
                    .SetText(m_popupTitleText[(int)POPUP_TYPE.POPUP_TYPE_HELP]);
                break;
        }
    }

    public void ClosePopup()
    {
        if(m_popupPannels[(int)m_currentPopupType])
            m_popupPannels[(int)m_currentPopupType].SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NextHint() 
    {
        if (m_helpImage) 
        {
            m_currentHelpCount++;
            m_helpImage.GetComponent<Image>().sprite = m_helpImages[m_currentHelpCount];
        }
    }
    public void PrevHint()
    {
        if (m_helpImage)
        {
            m_currentHelpCount--;
            m_helpImage.GetComponent<Image>().sprite = m_helpImages[m_currentHelpCount];
        }

    }
}
