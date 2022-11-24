using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �ۼ��� : ������
// �ۼ��� : 2022.11.24
// ��� ��� : GameManager�� ������Ʈ���� ���� �� �־� ����.
// ���� ���� : UI ��ü ��Ʈ��.
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject popupTitle; //PopupTopbarTitleText
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private GameObject soundButton; //ButtonSound
    [SerializeField] private Slider soundSlider; //Slider

    [SerializeField] private GameObject fullscreenButton; //ButtonF
    [SerializeField] private GameObject windowdedButton; //ButtonW

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject[] popupPannels;

    [SerializeField] private POPUP_TYPE currentPopup;
    const string prefsVolume = "musicVolume";
    //creators.txt

    enum POPUP_TYPE
    {
        POPUP_TYPE_SETTING,
        POPUP_TYPE_CREATORS,
        POPUP_TYPE_EXIT,
    }

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        //popupTitle = GameObject.Find("PopupTopbarTitleText"); //PopupTopbarTitleText
        //soundSlider = GameObject.Find("Slider").GetComponent<Slider>(); //Slider
        //soundButton = GameObject.Find("ButtonSound"); //ButtonSound

        if (!PlayerPrefs.HasKey(prefsVolume))
        {
            PlayerPrefs.SetFloat(prefsVolume, 1);
            Load();
        }
        else
        {
            Load();
        }

        //fullscreenButton = GameObject.Find("ButtonF"); //ButtonF
        //windowdedButton = GameObject.Find("ButtonW"); //ButtonW

        //popup = GameObject.Find("Popup");
        //popupPannels = new GameObject[3];
        //popupPannels[(int)POPUP_TYPE.POPUP_TYPE_SETTING] = GameObject.Find("SettingPannel");
        //popupPannels[(int)POPUP_TYPE.POPUP_TYPE_CREATORS] = GameObject.Find("CreatorsPannel");
        //popupPannels[(int)POPUP_TYPE.POPUP_TYPE_EXIT] = GameObject.FindGame("ExitPannel");

        Debug.Log("UI :: Initalize - Done");
    }


    public void OpenPopup(int popupType = 0)
    {
        if (popup) popup.SetActive(true);

        currentPopup = (POPUP_TYPE)popupType;
        popupPannels[popupType].SetActive(true);

        switch (popupType)
        {
            case (int)POPUP_TYPE.POPUP_TYPE_SETTING:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("����");
                break;
            case (int)POPUP_TYPE.POPUP_TYPE_CREATORS:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("������ ");
                break;
            case (int)POPUP_TYPE.POPUP_TYPE_EXIT:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("�ȳ�");
                break;
            default:
                break;
        }
    }

    public void ClosePopup()
    {
        if (popup) popup.SetActive(false);
        popupPannels[(int)currentPopup].SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundOnOff()
    {
    }

    public void SoundSlider()
    {
        AudioListener.volume = soundSlider.value;
        Save();
    }

    private void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat(prefsVolume);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(prefsVolume, soundSlider.value);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SelectModeUI");
    }
}
