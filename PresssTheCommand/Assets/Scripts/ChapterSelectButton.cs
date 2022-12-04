using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject chapterImage;
    [SerializeField] private GameObject chapterName;
    [SerializeField] private GameObject chapterInfo;

    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // 초기 환경 셋팅
    private void Initalize()
    {
        //chapterImage = GetChildWithName("ChapterImage");
        chapterName = GetChildWithName("ChapterName");
        chapterInfo = GetChildWithName("ChapterInfo");
        
        SetImage();
        SetChapterName("Chapter");
        SetChapterInfo("배우는 단축키 설명");

        //Debug.Log("ChapterSelectButton :: Initalize - Done");
    }
    GameObject GetChildWithName(string name)
    {
        Transform trans = this.transform;
        Transform childTrans = trans.Find(name);
 
        if (childTrans != null) { return childTrans.gameObject; }
        else { return null; }
    }

    public void SetImage(string imgName = "none")
    {
        if (!chapterImage) return;

        chapterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/" + imgName);
    }
    public void SetChapterName(string name = "Chapter")
    {
        if (!chapterName) return;

        chapterName.GetComponent<TMPro.TextMeshProUGUI>().SetText(name);
    }
    public void SetChapterInfo(string info = "배우는 단축키 설명")
    {
        if (!chapterInfo) return;

        chapterInfo.GetComponent<TMPro.TextMeshProUGUI>().SetText(info);
    }
}
