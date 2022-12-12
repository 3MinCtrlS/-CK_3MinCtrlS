using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameCuttingLine : MonoBehaviour
{
    [SerializeField] private GameObject m_cuttingObject;
    [SerializeField] private GameObject m_currentWidthText;
    [SerializeField] private GameObject m_questText;
    [SerializeField] private GameObject m_questTipText;
    [SerializeField] private GameObject m_plaidUI;

    [SerializeField] private Slider m_slider;
    [SerializeField] private GameObject m_silderFill;

    [SerializeField] private float m_value;
    [SerializeField] private float m_successValue;
    private const float m_successArrange = 0.25f;

    [SerializeField] private bool m_altIP;              // alt I P 사용 여부
    [SerializeField] private bool m_altIPFinished;   // 클릭 종료 여부

    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // 초기 환경 셋팅
    private void Initalize()
    {
        m_altIP = false;
        m_altIPFinished = false;

        m_cuttingObject = GameObject.Find("CuttingObjectSlider");
        m_currentWidthText = GameObject.Find("CurrentWidthText");
        m_questText = GameObject.Find("QuestText");
        m_questTipText = GameObject.Find("QuestTipText");

        //m_plaidUI = GameObject.Find("PlaidUI");
        if (m_plaidUI) m_plaidUI.SetActive(false);

        m_value = 0.6f;
        m_slider = m_cuttingObject.GetComponent<Slider>();
        m_silderFill = GameObject.Find("CuttingObjectSliderFill");

        SliderActivation();
        SetValue(m_value);
        SetQuest();
    }

    public void PlaidUIActivation() 
    {
        if (m_plaidUI) 
        {
            if (m_plaidUI.activeSelf) m_plaidUI.SetActive(false);
            else m_plaidUI.SetActive(true);
        }
    }
    private void SliderActivation()
    {
        if (!m_slider) 
        {
            Debug.Log("MinigameCuttingLine :: m_slider is null");
            return;
        }
        if (!m_silderFill) 
        {
            Debug.Log("MinigameCuttingLine :: m_silderFill is null");
            return;
        }

        bool currentState = m_altIP && !m_altIPFinished;
        m_slider.interactable = currentState;

        if (currentState)
        {
            Color activatedColor = new Color32(255, 255, 255, 255);
            m_silderFill.GetComponent<Image>().color = activatedColor;
        }
        else 
        {
            Color inactivatedColor = new Color32(205, 205, 205, 255);
            m_silderFill.GetComponent<Image>().color = inactivatedColor;
        }
    }

    public void SetAltIP(bool isAltIP) 
    {
        m_altIP = isAltIP;
        SliderActivation();
    }


    // 값 변경 일괄 셋팅 
    public void SetValue(float value)
    {
        m_value = value;

        if (m_slider) 
        {
            m_slider.value = m_value;
        }

        if (m_currentWidthText) 
        {
            string currentWidthText = string.Format("현재 길이 : {0:F2}cm", m_slider.value * 10);
            m_currentWidthText.GetComponent<TMPro.TextMeshProUGUI>().SetText(currentWidthText);
        }
    }

    // 퀘스트 세팅
    private void SetQuest()
    {
        m_successValue = GameManager.Instance.Random(25, 45) / 10f;

        string questText = string.Format("{0}cm 길이를 남기세요.", m_successValue);
        string questTipText = string.Format("성공 길이 : {0:F2}cm~{1:F2}cm", m_successValue - m_successArrange, m_successValue + m_successArrange);

        if (m_questText) m_questText.GetComponent<TMPro.TextMeshProUGUI>().SetText(questText);
        if (m_questTipText) m_questTipText.GetComponent<TMPro.TextMeshProUGUI>().SetText(questTipText);
    }


    public void SliderMouseLeave() 
    {
        if (!m_slider) return;

        if (m_altIP && !m_altIPFinished) 
        {
            m_altIPFinished = true;
            GetComponent<AudioUnits>().PlayAudio();
            SetValue(m_slider.value);
            SliderActivation();
            CheckQuest();
        }
    }

    // 목적 달성 여부 확인
    private void CheckQuest() 
    {
        if (m_slider.value*10 <= m_successValue + m_successArrange
            && m_slider.value * 10 >= m_successValue - m_successArrange)
        {
            CuttingLineSuccess();
        }
        else 
        {
            CuttingLineFail();
        }
    }

    private void CuttingLineSuccess() 
    {
        string titleText = "Game Clear!";
        string infoText = "종료 단축키를 이용해서 게임을 종료하세요.";
        Color successColor = new Color32(151, 210, 88, 200);

        MinigameManager.Instance.SetMinigameClear(true);
        MinigameManager.Instance.SetResult(titleText, infoText, successColor, false);
    }

    private void CuttingLineFail()
    {
        string titleText = "Game Over!";
        string infoText = "우측 하단의 재도전 버튼을 누르세요!";
        Color failColor = new Color32(255, 13, 13, 200);

        MinigameManager.Instance.SetResult(titleText, infoText, failColor, true);
    }

    public void RestartMinigame() 
    {
        Initalize();
    }
}
