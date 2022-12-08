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
    [SerializeField] private bool isAltI;


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
        isShift = false;
        isAlt = false;
        isAltI = false; 
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
        if (DetectAltKey() && Input.GetKeyDown(KeyCode.Delete)) { HotKeyAltDelete(); }

        // 선 자르기
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.R)) { HotKeyCtrlR(); }
        if (DetectAltKey() && Input.GetKeyDown(KeyCode.I)) { HotKeyAltI(); }
        if (isAltI && Input.GetKeyDown(KeyCode.P)) { HotKeyAltIP(); }

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
    private bool DetectAltKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("Alt key was pressed.");
            isAlt = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            //Debug.Log("Alt key left.");
            isAlt = false;
        }

        return isAlt;
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

        // 미니게임 종료가 체크될 시에만 사용 가능.
        if (MinigameManager.Instance.GetMinigameClear()) 
        {
            GameManager.Instance.ResumeGame();
            MinigameManager.Instance.StopMinigame();
        }
    }

    /// 펜타미노 =====

    // 펜타미노 : 레이어 복제하기(컨트롤+J)
    private void HotKeyCtrlJ()
    {
        Debug.Log("Ctrl+J key was pressed.");


        GameObject pentaminoManager = GameObject.Find("MinigamePentamino");

        if (!pentaminoManager) return;
        ShapeManager shapeManager = pentaminoManager.GetComponent<ShapeManager>();

        if (!shapeManager) return;
        GameObject selectedObject = shapeManager.GetSelectedObject();

        Vector3 anchoredPos;
        Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Canvas minigamePlayerPannel = GameObject.Find("MinigamePlayerPannel").GetComponent<Canvas>();

        RectTransformUtility.ScreenPointToWorldPointInRectangle(selectedObject.transform.parent.GetComponent<RectTransform>(), new Vector3(Screen.width, Screen.height, 1.0f), minigamePlayerPannel.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPos);

        // Manager에 등록하여 사용할 수 있게끔
        Instantiate(selectedObject, anchoredPos, Quaternion.identity);
    }

    private void HotKeyCtrlJTest()
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject pentaminoManager = GameObject.Find("MinigamePentamino");

        if (!pentaminoManager) return;
        ShapeManager shapeManager = pentaminoManager.GetComponent<ShapeManager>();

        if (!shapeManager) return;
        GameObject selectedObject = shapeManager.GetSelectedObject();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Manager에 등록하여 사용할 수 있게끔
        Instantiate(selectedObject, selectedObject.transform.parent, true);
    }

    private void HotKeyCtrlJOld()
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();

        Camera raycastCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Vector3 offset = new Vector3(5, -5, 0);
        Vector3 pos = Input.mousePosition + offset;
        pos.z = 0;

        Instantiate(selectedObject, raycastCamera.ScreenToWorldPoint(pos), Quaternion.identity);
    }

    // 펜타미노 :자유변형(컨트롤+T)
    private void HotKeyCtrlT()
    {
    }

    /// 선 자르기 =====

    // 선 자르기 : 표시자 보이기/숨기기(컨트롤+R)
    private void HotKeyCtrlR()
    {
        GameObject cuttingLine = GameObject.Find("MinigameCuttingLine");
        if(cuttingLine) cuttingLine.GetComponent<MinigameCuttingLine>().PlaidUIActivation();
    }

    // 선 자르기 : 선택한 영역 잘라내기(ALT+I+P)
    private void HotKeyAltI()
    {
        if (isAlt) 
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Alt I key was pressed.");
                isAltI = true;
            }

            if (Input.GetKeyUp(KeyCode.I))
            {
                isAltI = false;
            }
        }
    }

    // 선 자르기 : 선택한 영역 잘라내기(ALT+I+P)
    private void HotKeyAltIP()
    {
        GameObject cuttingLine = GameObject.Find("MinigameCuttingLine");
        if (cuttingLine) cuttingLine.GetComponent<MinigameCuttingLine>().SetAltIP(true);
    }

    /// 알맞게 색깔 채워 넣기 =====

    // 알맞게 색깔 채워 넣기 : 페인트 통(G+클릭)

    private void HotKeyG()
    {
        GameObject paint = GameObject.Find("MinigamePaint");
        if (paint) paint.GetComponent<MinigamePaint>().ButtonActivate();
    }
    private void HotKeyAltDelete()
    {
        GameObject paint = GameObject.Find("MinigamePaint");
        if (paint) paint.GetComponent<MinigamePaint>().SetUserData();
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
