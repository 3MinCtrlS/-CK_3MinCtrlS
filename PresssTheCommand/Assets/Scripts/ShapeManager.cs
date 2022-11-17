using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject selectedObject;


    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        SelectObject();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //camera = GameObject.FindObjectOfType<Camera>();
        Debug.Log("ShapeManager :: Initalize - Done");
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ShapeManager :: Clicked");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (rayHit.collider != null)
            {
                Debug.Log(rayHit.collider.name);

                GameObject selectedBackground;

                // ������Ʈ�� ���� ���, ������ ������Ʈ�� ���� �� �־�� �Ѵ�.
                if (selectedObject != null)
                {
                    selectedBackground = selectedObject.transform.Find("SelectedBackground").gameObject;
                    selectedBackground.SetActive(false);
                }

                selectedObject = rayHit.collider.gameObject;

                // ���� ���. ���õǾ��� �� ������� ������Ʈ�� �޾��ش�.
                selectedBackground = selectedObject.transform.Find("SelectedBackground").gameObject;
                selectedBackground.SetActive(true);
            }
        }
    }

    public string GetSelectedObjectName() 
    {
        return selectedObject.name;
    }
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
}
