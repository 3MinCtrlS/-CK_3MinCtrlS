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


    // 초기 환경 셋팅
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

                // 오브젝트가 있을 경우, 기존의 오브젝트를 해제 해 주어야 한다.
                if (selectedObject != null)
                {
                    selectedBackground = selectedObject.transform.Find("SelectedBackground").gameObject;
                    selectedBackground.SetActive(false);
                }

                selectedObject = rayHit.collider.gameObject;

                // 임의 기능. 선택되었을 때 배경으로 오브젝트를 달아준다.
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
