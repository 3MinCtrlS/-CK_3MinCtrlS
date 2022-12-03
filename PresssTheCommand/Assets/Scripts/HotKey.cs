using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : 전시은
// 작성일 : 2022.11.17
// 사용 방법 : 플레이어의 입력에서 단축키를 감지하고, 분류 및 제어한다.
// 제작 사유 : 단축키 감지, 제어
public class HotKey : MonoBehaviour
{
    [SerializeField] private bool isControl;
    [SerializeField] private bool isShift;
    [SerializeField] private bool isAlt;


    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // Update is called once per frame
    private void Update()
    {
        DetectKey();
    }


    // 초기 환경 셋팅
    private void Initalize()
    {
        isControl = false;
        isAlt = false;
        isShift = false;

        GameObject plaidUI = GameObject.Find("PlaidUI");
        plaidUI.SetActive(false);
        //Debug.Log("HotKey :: Initalize - Done");
    }

    // 추후 리팩토링이 필요하다
    // 참고 : https://unity-programmer.tistory.com/9
    private void DetectKey() 
    {
        // 고정 단축키
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.S)) { HotKeyCtrlS(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.Z)) { HotKeyCtrlZ(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.Q)) { HotKeyCtrlQ(); }

        // 펜토미노
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.T)) { HotKeyCtrlT(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.J)) { HotKeyCtrlJ(); }

        // 알맞게 색깔 채워 넣기
        if (Input.GetKeyDown(KeyCode.G)) { HotKeyG(); }

        // 선 자르기
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.R)) { HotKeyCtrlR(); }
        if (DetectAltKey() && Input.GetKeyDown(KeyCode.I) && Input.GetKeyDown(KeyCode.P)) { HotKeyAltIP(); }

        // 색깔놀이
        if (Input.GetKeyDown(KeyCode.LeftBracket)) { HotKeyLeftBracket(); }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { HotKeyRightBracket(); }
    }

    /// Ctrl, Alt, Shift 키 감지 =====

    private bool DetectControlKey() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Debug.Log("Control key was pressed.");
            isControl = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //Debug.Log("Control key left.");
            isControl = false;
        }

        return isControl;
    }

    private bool DetectAltKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //Debug.Log("Alt key was pressed.");
            isAlt = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            //Debug.Log("Alt key left.");
            isAlt = false;
        }

        return isAlt;
    }

    private bool DetectShiftKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Debug.Log("Shift key was pressed.");
            isShift = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Debug.Log("Shift key left.");
            isShift = false;
        }

        return isShift;
    }

    /// 고정 키 =====

    // 고정 : 저장키
    private void HotKeyCtrlS()
    {
        // 도중에 블루스크린 오류가 튀어나오는 것을 방지하기 위함.
        Debug.Log("Ctrl+S key was pressed.");
    }

    // 고정 : 뒤로가기, 미니게ㄴ임 도중에 이전 작업으로 돌아가고 싶을 경우 사용.
    private void HotKeyCtrlZ()
    {
        Debug.Log("Ctrl+Z key was pressed.");
    }

    // 고정 : 종료키, 미니게임 클리어 시 미니게임 종료 용도로 사용.
    private void HotKeyCtrlQ()
    {
        Debug.Log("Ctrl+Q key was pressed.");
        GameManager.Instance.ResumeGame();
        MinigameManager.Instance.StopMinigame();
    }

    /// 펜타미노 =====

    // 펜타미노 : 레이어 복제하기(컨트롤+J)
    private void HotKeyCtrlJ() 
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Manager에 등록하여 사용할 수 있게끔
        Instantiate(selectedObject, selectedObject.transform.parent, true);
    }

    private void HotKeyCtrlJOld()
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Manager에 등록하여 사용할 수 있게끔
        Instantiate(selectedObject, mousePos2D, Quaternion.identity);
    }

    // 펜타미노 :자유변형(컨트롤+T)
    private void HotKeyCtrlT()
    { 
    }

    /// 선 자르기 =====

    // 선 자르기 : 표시자 보이기/숨기기(컨트롤+R)
    private void HotKeyCtrlR()
    { 
    }

    // 선 자르기 : 선택한 영역 잘라내기(ALT+I+P)
    private void HotKeyAltIP()
    {
    }

    /// 알맞게 색깔 채워 넣기 =====

    // 알맞게 색깔 채워 넣기 : 페인트 통(G+클릭)

    private void HotKeyG()
    {
        GameObject plaidUI = GameObject.Find("PlaidUI");
        plaidUI.SetActive(true);
    }

    /// 색깔놀이 =====

    // 색깔놀이 : 브러쉬 크기 조정([=작게,]=크게)

    private void HotKeyLeftBracket()
    {
    }
    private void HotKeyRightBracket()
    {
    }
}
