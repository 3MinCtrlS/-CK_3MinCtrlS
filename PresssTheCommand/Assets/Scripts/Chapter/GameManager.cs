using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �ۼ��� : ������
// �ۼ��� : 2022.11.27
// ��� ��� : GameManager.Instance.���ϴ±��();
// ���� ���� : �̱��� ������ Ȱ���Ͽ� �������� ���� ���� ���� �� ���� ��� ó��
public class GameManager : MonoBehaviour
{
    // �̱��� ���� ����
    private static GameManager instance = null;

    [SerializeField] private GameObject m_popupUI;
    private const string m_prefsVolume = "musicVolume";


    void Awake()
    {
        if (null == instance)
        {
            //instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //DontDestroyOnLoad(this.gameObject);
            Debug.Log("GameManager :: Make instance - Done");
        }
        else
        {
            //instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameManager)�� ����
            Destroy(this.gameObject);
        }
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ.
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

    // �ʱ� ȯ�� ����
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


    //�����ڷ� : https://gigong.tistory.com/65
    public int Random(int min, int max)
    {
        if (max < min)
            throw new ArgumentOutOfRangeException();

        var rand = System.Security.Cryptography.RandomNumberGenerator.Create();

        byte[] data = new byte[4];

        // ������ ���� ����
        rand.GetBytes(data);

        return Math.Abs(BitConverter.ToInt32(data, 0)) % (max - min) + min;
    }
}
