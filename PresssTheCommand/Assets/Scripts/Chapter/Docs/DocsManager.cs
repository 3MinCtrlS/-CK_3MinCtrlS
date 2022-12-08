using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 작성자 : 전시은
// 작성일 : 2022.12.09
// 사용 방법 : instance를 통하여 접근
// 제작 사유 : 기본적인 문서 챕터 컨트롤
public class DocsManager : MonoBehaviour
{
    private static DocsManager instance = null;

    [SerializeField] private int m_playerHp;
    [SerializeField] private const int m_playerMaxHp = 3;
    [SerializeField] private GameObject[] m_hearts;
    [SerializeField] private Sprite[] m_heartImage;

    [SerializeField] private float m_timer;
    [SerializeField] private GameObject m_timerText;



    void Awake()
    {
        if (null == instance)
        {
            //instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //DontDestroyOnLoad(this.gameObject);
            Debug.Log("DocsManager :: Make instance - Done");
        }
        else
        {
            //instance에 인스턴스가 존재한다면 자신(새로운 씬의 MinigameManager)을 삭제
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티.
    public static DocsManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // 초기 환경 셋팅
    private void Initalize()
    {
        m_playerHp = 3;
        SetHP();

        m_timer = 105.0f;
        SetTimerUI();
        StartCoroutine(Timer());
    }

    public void RestartMinigame()
    {
        Initalize();
    }

    public void SetHP() 
    {
        for (int i = 0; i < m_playerMaxHp; i++) 
        {
            if (i < m_playerHp)
            {
                m_hearts[i].GetComponent<Image>().sprite = m_heartImage[1];
            }
            else 
            {
                m_hearts[i].GetComponent<Image>().sprite = m_heartImage[0];
            }
        }
    }

    private void SetTimerUI() 
    {
        if (m_timerText) 
        {
            string timeText;

            if (m_timer > 60)
            {
                int minute = (int) m_timer / 60;
                float second = m_timer - (minute * 60);
                timeText = string.Format("마감까지 {0}분 {1:F2}초!", minute, second);
            }
            else 
            {
                timeText = string.Format("마감까지 {0:F2}초!", m_timer);
            }
            m_timerText.GetComponent<TMPro.TextMeshProUGUI>().text = timeText;
        }
    }

    IEnumerator Timer()
    {
        m_timer -= Time.deltaTime * 10;
        SetTimerUI();

        if (m_timer <= 0)
        {
            GameOver();
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Timer");

        //while (true)
        //{
        //    m_timerText;
        //    Debug.Log(System.DateTime.Now);
        //    yield return new WaitForSeconds(1.0f);
        //}
    }


    private void GameClear()
    {
        Debug.Log("DocsManager :: GameClear");
    }

    private void GameOver() 
    {
        Debug.Log("DocsManager :: GameOver");
    }
}
