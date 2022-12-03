using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCuttingLine : MonoBehaviour
{
    [SerializeField] private GameObject m_cuttingObject;
    [SerializeField] private GameObject m_currentWidthText;
    [SerializeField] private GameObject m_questText;
    [SerializeField] private GameObject m_questTipText;


    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // 초기 환경 셋팅
    private void Initalize()
    {
        m_cuttingObject       = GameObject.Find("CuttingObject");
        m_currentWidthText   = GameObject.Find("CurrentWidthText");
        m_questText             = GameObject.Find("QuestText");
        m_questTipText         = GameObject.Find("QuestTipText");

        m_cuttingObject.SetActive(false);
        m_currentWidthText.SetActive(false);
        m_questText.SetActive(false);
        m_questTipText.SetActive(false);
    }
}
