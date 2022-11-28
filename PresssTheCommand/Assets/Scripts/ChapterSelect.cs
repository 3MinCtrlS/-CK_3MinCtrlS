using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �ۼ��� : ������
// �ۼ��� : 2022.11.24
// ��� ��� : Pannel�� script�� �����ϰ�, ������Ʈ���� ���� �� �־� ����.
// ���� ���� : ChapterSelect ��ư ���� �����͸� ����
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

    // �ʱ� ȯ�� ����
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
