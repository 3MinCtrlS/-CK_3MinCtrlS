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


    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        DetectKey();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        isControl = false;
        isShift = false;
        Debug.Log("HotKey :: Initalize - Done");
    }

    private void DetectKey() 
    {
        if (DetectControlKey() && Input.GetKeyDown(KeyCode.J))
        {
            HotKeyCtrlJ();
        }
    }

    private bool DetectControlKey() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Control key was pressed.");
            isControl = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("Control key left.");
            isControl = false;
        }

        return isControl;
    }

    [System.Obsolete]
    private void HotKeyCtrlJ() 
    {
        Debug.Log("Ctrl+J key was pressed.");

        //string selectedObjectName = this.GetComponent<ShapeManager>().GetSelectedObject();
        //GameObject selectedPrefab = PrefabUtility.GetPrefabObject();

        GameObject selectedObject = GetComponent<ShapeManager>().GetSelectedObject();
        //Object parentObject = EditorUtility.GetPrefabParent(selectedObject);
        ////Object parentObject = EditorUtility.GetPrefabParent(GetComponent<ShapeManager>().GetSelectedObject());
        //string path = AssetDatabase.GetAssetPath(parentObject);

        //Debug.Log("Prefabs/" + selectedObject.tag);
        //GameObject selectedPrefab = Resources.Load("Prefabs/" + selectedObject.tag) as GameObject;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        // Manager�� ����Ͽ� ����� �� �ְԲ�

        //Instantiate(selectedPrefab, mousePos2D, Quaternion.identity);
        Instantiate(selectedObject, mousePos2D, Quaternion.identity);
    }
}
