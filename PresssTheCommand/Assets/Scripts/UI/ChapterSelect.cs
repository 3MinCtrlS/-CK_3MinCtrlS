using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 작성자 : 전시은
// 작성일 : 2022.11.24
// 사용 방법 : Pannel에 script를 적용하고, 오브젝트들을 전달 해 주어 동작.
// 제작 사유 : ChapterSelect 버튼 내의 데이터를 관리
public class ChapterSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] chapterPannels;
    [SerializeField] private ChapterSelectButton[] chapterPannelData;

    enum CHAPTER_TYPE
    {
        CHAPTER_TYPE_DOCS,
        CHAPTER_TYPE_GRAPHICS,
        CHAPTER_TYPE_IDE,
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initalize();
    }

    // 초기 환경 셋팅
    private void Initalize()
    {
        chapterPannelData = new ChapterSelectButton[3];
        chapterPannels = new GameObject[3];
        //Debug.Log("ChapterSelect :: Initalize - Done");
    }


    public void SetChapter(int chapterType)
    {
        chapterPannels[(int)CHAPTER_TYPE.CHAPTER_TYPE_DOCS].GetComponent<ChapterSelectButton>();
        chapterPannels[(int)CHAPTER_TYPE.CHAPTER_TYPE_GRAPHICS].GetComponent<ChapterSelectButton>();
        chapterPannels[(int)CHAPTER_TYPE.CHAPTER_TYPE_IDE].GetComponent<ChapterSelectButton>();
    }
    public void SelectChapter(int chapterType) 
    {
        switch (chapterType)
        {
            case (int)CHAPTER_TYPE.CHAPTER_TYPE_DOCS:
                SceneManager.LoadScene("ChapterDocs");
                break;
            case (int)CHAPTER_TYPE.CHAPTER_TYPE_GRAPHICS:
                SceneManager.LoadScene("ChapterGraphics");
                break;
            case (int)CHAPTER_TYPE.CHAPTER_TYPE_IDE:
                SceneManager.LoadScene("ChapterIDE");
                break;
            default:
                break;
        }
    }
}
