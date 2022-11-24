using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string prefsVolume = "musicVolume";
    // Start is called before the first frame update
    private void Start()
    {
        Initalize();
    }

    // 초기 환경 셋팅
    private void Initalize()
    {
        if (!PlayerPrefs.HasKey(prefsVolume))
        {
            PlayerPrefs.SetFloat(prefsVolume, 1);
            LoadSetting();
        }
        else
        {
            LoadSetting();
        }

        Debug.Log("GameManager :: Initalize - Done");
    }


    private void LoadSetting()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(prefsVolume);
    }
}
