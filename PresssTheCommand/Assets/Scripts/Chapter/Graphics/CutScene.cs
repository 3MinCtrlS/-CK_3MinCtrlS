using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 작성자 : 전시은
// 작성일 : 2022.12.11
// 사용 방법 : 최적화를 뭐 줬지만 일단 완성하는 것에 집중하기로...
// 제작 사유 : 
public class CutScene : MonoBehaviour
{
    [SerializeField] private GameObject m_popup;
    [SerializeField] private GameObject[] m_cutSceneImages;
    [SerializeField] private int m_cutsceneCount = 5;
    [SerializeField] private int m_currentSceneCount;
    [SerializeField] private bool m_isMoving;

    enum SCENE_TYPE 
    {
        SCENE_START_1 = 0,
        SCENE_START_2,
        SCENE_START_3,
        SCENE_START_4,
        SCENE_END_1,
    }

    // Start is called before the first frame update
    private void Start() { Initalize(); }

    private void Update() 
    {
        MovingScene();
        CheckClick();
    }

    // 초기 환경 셋팅
    private void Initalize()
    {
        m_popup.SetActive(true);
        m_currentSceneCount = 0;

        for (int i = 0; i < m_cutsceneCount; i++) 
        {
            m_cutSceneImages[i].SetActive(false);
        }

        // 처음 화면만 활성화
        m_cutSceneImages[0].SetActive(true);

        if (m_isMoving) 
        {
            StartCoroutine(MovingScene());
        }
    }

    private IEnumerator MovingScene() 
    {
        Vector3 pos = m_cutSceneImages[m_currentSceneCount].GetComponent<RectTransform>().position;

        if (pos.y <= 4.3)
        {
            pos.y += Time.deltaTime / 5;
            m_cutSceneImages[m_currentSceneCount].GetComponent<RectTransform>().position = pos;

            yield return new WaitForSeconds(0.01f);
            StartCoroutine("MovingScene");
        }
        else 
        {
            StopCoroutine("MovingScene");
        }
    }

    private void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("ShapeManager :: Clicked");

            if (m_currentSceneCount < (int)SCENE_TYPE.SCENE_START_4)
            {
                m_cutSceneImages[m_currentSceneCount].SetActive(false);
                m_cutSceneImages[m_currentSceneCount + 1].SetActive(true);
                m_currentSceneCount++;

                if (m_isMoving)
                {
                    StartCoroutine(MovingScene());
                }
            }
            else if (m_currentSceneCount >= (int)SCENE_TYPE.SCENE_START_4) 
            {
                m_cutSceneImages[m_currentSceneCount].SetActive(false);
                m_popup.SetActive(false);
            }
        }
    }

    public void ClearScene() 
    {
        m_currentSceneCount = (int)SCENE_TYPE.SCENE_END_1;
        m_cutSceneImages[m_currentSceneCount].SetActive(true);
    }
}
