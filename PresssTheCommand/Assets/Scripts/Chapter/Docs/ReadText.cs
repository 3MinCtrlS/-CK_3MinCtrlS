using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 작성자 : 전시은
// 작성일 : 2022.12.12
// 사용 방법 : ReadTexT
// 제작 사유 : 문서 내용 읽기
public class ReadText : MonoBehaviour
{
    [SerializeField] private GameObject m_textUI;
    [SerializeField] public string m_docsString;
    [SerializeField] public string[] m_hintString;
    [SerializeField] private string m_filePath;

    private void Start()
    {
        m_filePath = Path.Combine("Assets/Scripts/Chapter/Docs/AutoDocs.txt");
        m_docsString = ReadTextFile();

        m_hintString = new string[20];
        m_filePath = Path.Combine("Assets/Scripts/Chapter/Docs/HintText.txt");
        m_hintString[0] = ReadTextFile();
        m_hintString = ReadTextFileWithEnter(m_hintString[0]);

        StartCoroutine(ShowText());
    }

    private string ReadTextFile()
    {
        FileInfo fileInfo = new FileInfo(m_filePath);
        string resultString;

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(m_filePath);
            resultString = reader.ReadToEnd();
            reader.Close();
        }
        else 
        {
            resultString = "파일이 없습니다.";
        }

        return resultString;
    }

    private string[] ReadTextFileWithEnter(string original)
    {
        string[] resultString;
        resultString = original.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);

        //for (int i = 0; i < original.Length; i++)
        //{
        //    if (original.Contains("\n"))
        //    {
        //    }
        //}

        return resultString;
    }

    public float delay = 0.1f;
    private string currentText = "";

    IEnumerator ShowText()
    {
        for (int i = 0; i < m_docsString.Length; i++)
        {
            currentText = m_docsString.Substring(0, i);
            m_textUI.GetComponent<TMPro.TextMeshProUGUI>().SetText(currentText);
            yield return new WaitForSeconds(delay);
        }
    }
}
