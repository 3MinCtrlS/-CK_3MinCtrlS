using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ۼ��� : ������
// �ۼ��� : 2022.11.17
// ��� ��� : �÷��̾��� �Է¿��� ����Ű�� �����ϰ�, �з� �� �����Ѵ�.
// ���� ���� : ����Ű ����, ����
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


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        isControl = false;
        isAlt = false;
        isShift = false;

        GameObject plaidUI = GameObject.Find("PlaidUI");
        plaidUI.SetActive(false);
        //Debug.Log("HotKey :: Initalize - Done");
    }

    // ���� �����丵�� �ʿ��ϴ�
    // ���� : https://unity-programmer.tistory.com/9
    private void DetectKey() 
    {
        // ���� ����Ű
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.S)) { HotKeyCtrlS(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.Z)) { HotKeyCtrlZ(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.Q)) { HotKeyCtrlQ(); }

        // ����̳�
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.T)) { HotKeyCtrlT(); }
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.J)) { HotKeyCtrlJ(); }

        // �˸°� ���� ä�� �ֱ�
        if (Input.GetKeyDown(KeyCode.G)) { HotKeyG(); }

        // �� �ڸ���
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.R)) { HotKeyCtrlR(); }
        if (DetectAltKey() && Input.GetKeyDown(KeyCode.I) && Input.GetKeyDown(KeyCode.P)) { HotKeyAltIP(); }

        // �������
        if (Input.GetKeyDown(KeyCode.LeftBracket)) { HotKeyLeftBracket(); }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { HotKeyRightBracket(); }
    }

    /// Ctrl, Alt, Shift Ű ���� =====

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

    /// ���� Ű =====

    // ���� : ����Ű
    private void HotKeyCtrlS()
    {
        // ���߿� ��罺ũ�� ������ Ƣ����� ���� �����ϱ� ����.
        Debug.Log("Ctrl+S key was pressed.");
    }

    // ���� : �ڷΰ���, �̴ϰԤ��� ���߿� ���� �۾����� ���ư��� ���� ��� ���.
    private void HotKeyCtrlZ()
    {
        Debug.Log("Ctrl+Z key was pressed.");
    }

    // ���� : ����Ű, �̴ϰ��� Ŭ���� �� �̴ϰ��� ���� �뵵�� ���.
    private void HotKeyCtrlQ()
    {
        Debug.Log("Ctrl+Q key was pressed.");
        GameManager.Instance.ResumeGame();
        MinigameManager.Instance.StopMinigame();
    }

    /// ��Ÿ�̳� =====

    // ��Ÿ�̳� : ���̾� �����ϱ�(��Ʈ��+J)
    private void HotKeyCtrlJ() 
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Manager�� ����Ͽ� ����� �� �ְԲ�
        Instantiate(selectedObject, selectedObject.transform.parent, true);
    }

    private void HotKeyCtrlJOld()
    {
        Debug.Log("Ctrl+J key was pressed.");

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Manager�� ����Ͽ� ����� �� �ְԲ�
        Instantiate(selectedObject, mousePos2D, Quaternion.identity);
    }

    // ��Ÿ�̳� :��������(��Ʈ��+T)
    private void HotKeyCtrlT()
    { 
    }

    /// �� �ڸ��� =====

    // �� �ڸ��� : ǥ���� ���̱�/�����(��Ʈ��+R)
    private void HotKeyCtrlR()
    { 
    }

    // �� �ڸ��� : ������ ���� �߶󳻱�(ALT+I+P)
    private void HotKeyAltIP()
    {
    }

    /// �˸°� ���� ä�� �ֱ� =====

    // �˸°� ���� ä�� �ֱ� : ����Ʈ ��(G+Ŭ��)

    private void HotKeyG()
    {
        GameObject plaidUI = GameObject.Find("PlaidUI");
        plaidUI.SetActive(true);
    }

    /// ������� =====

    // ������� : �귯�� ũ�� ����([=�۰�,]=ũ��)

    private void HotKeyLeftBracket()
    {
    }
    private void HotKeyRightBracket()
    {
    }
}
