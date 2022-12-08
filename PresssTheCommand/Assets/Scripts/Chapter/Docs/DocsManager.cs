using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ۼ��� : ������
// �ۼ��� : 2022.12.09
// ��� ��� : instance�� ���Ͽ� ����
// ���� ���� : �⺻���� ���� é�� ��Ʈ��
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
            //instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //DontDestroyOnLoad(this.gameObject);
            Debug.Log("DocsManager :: Make instance - Done");
        }
        else
        {
            //instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� MinigameManager)�� ����
            Destroy(this.gameObject);
        }
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ.
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

    // �ʱ� ȯ�� ����
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
                timeText = string.Format("�������� {0}�� {1:F2}��!", minute, second);
            }
            else 
            {
                timeText = string.Format("�������� {0:F2}��!", m_timer);
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
