using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 작성자 : 전시은
// 작성일 : 2022.11.27
// 사용 방법 : GameManager.Instance.원하는기능();
// 제작 사유 : 싱글톤 패턴을 활용하여 전반적인 게임 진행 관리 및 공용 기능 처리
public class GameManager : MonoBehaviour
{
    // 싱글턴 패턴 적용
    private static GameManager instance = null;

    [SerializeField] private GameObject m_popupUI;
    private const string m_prefsVolume = "musicVolume";


    void Awake()
    {
        if (null == instance)
        {
            //instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //DontDestroyOnLoad(this.gameObject);
            Debug.Log("GameManager :: Make instance - Done");
        }
        else
        {
            //instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameManager)을 삭제
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티.
    public static GameManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initalize();
    }

    // 초기 환경 셋팅
    private void Initalize()
    {
        //m_popupUI = GameObject.Find("PopupTopbarTitleText");
        //if (!m_popupUI) m_popupUI = GameObject.FindGameObjectWithTag("MainPopup");

        LoadSetting();

        //Debug.Log("GameManager :: Initalize - Done");
    }

    public void PopupUIOn(string typeName) 
    {
        if (!m_popupUI) return;

        m_popupUI.GetComponent<PopupUI>().SetPopupType(typeName);
        m_popupUI.GetComponent<PopupUI>().OpenPopup();
    }

    public void PopupUIOff()
    {
         GameObject[] uiObjectList = GameObject.FindGameObjectsWithTag("UI");

        foreach (GameObject uiObject in uiObjectList)
        {
            uiObject.SetActive(false);
        }

       uiObjectList = GameObject.FindGameObjectsWithTag("MainPopup");

        foreach (GameObject uiObject in uiObjectList)
        {
            uiObject.SetActive(false);
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0; //Stop object using deltatime
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; //start object using deltatime
    }

    public void RestartGame()
    {

    }
    public void MoveScene(string sceneName = "MainUI")
    {
        SceneManager.LoadScene(sceneName);
    }

    private void LoadSetting()
    {
        if (!PlayerPrefs.HasKey(m_prefsVolume))
        {
            PlayerPrefs.SetFloat(m_prefsVolume, 1);
            LoadSetting();
        }
        else 
        {
            AudioListener.volume = PlayerPrefs.GetFloat(m_prefsVolume);
        }
    }


    //참고자료 : https://gigong.tistory.com/65
    public int Random(int min, int max)
    {
        if (max < min)
            throw new ArgumentOutOfRangeException();

        var rand = System.Security.Cryptography.RandomNumberGenerator.Create();

        byte[] data = new byte[4];

        // 임의의 숫자 생성
        rand.GetBytes(data);

        return Math.Abs(BitConverter.ToInt32(data, 0)) % (max - min) + min;
    }
}
