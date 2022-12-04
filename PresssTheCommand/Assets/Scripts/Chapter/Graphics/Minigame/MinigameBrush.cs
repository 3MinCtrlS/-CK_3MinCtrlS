using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBrush : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // 초기 환경 셋팅
    private void Initalize()
    {

    }

    public void RestartMinigame()
    {
        Initalize();
    }
}
