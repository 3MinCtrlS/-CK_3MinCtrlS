using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private static MinigameManager instance = null;

    [SerializeField] private GameObject m_minigamePopup;
    Animator m_minigamePopupAnimator = null;
    private const string m_minigamePopupName = "MinigameCanvas";
    private const string m_minigamePanelName = "MinigamePanel";

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
        MINIGAME_PENTAMINO,
        MINIGAME_CUTTINGLINE,
        MINIGAME_PAINT, // PAINTING
        MINIGAME_BRUSH, // COLORING 
    }


    private void Start() { Initalize(); }

    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        m_minigamePopup = GameObject.Find(m_minigamePopupName);
        GameManager.Instance.PopupUIOff();

        m_minigamePopupAnimator = GameObject.Find(m_minigamePanelName).GetComponent<Animator>();
        m_minigamePopupAnimator.SetTrigger("MinigameStart");
        //Debug.Log("MinigameManager :: Initalize - Done");
    }

    public void StartMinigame(int minigameType) 
    {
        m_minigamePopup.SetActive(true);

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

    private void PlayPentamino() 
    {
        Debug.Log("MinigameManager :: PlayPentamino");
    }

    private void PlayCuttingline()
    {
        Debug.Log("MinigameManager :: PlayCuttingline");
    }

    private void PlayPaint()
    {
        Debug.Log("MinigameManager :: PlayPaint");
    }

    private void PlayBrush()
    {
        Debug.Log("MinigameManager :: PlayBrush");
    }

}
