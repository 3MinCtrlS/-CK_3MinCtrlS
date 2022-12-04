using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private Camera m_raycastCamera;
    [SerializeField] private GameObject m_selectedObject;


    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // Update is called once per frame
    void Update()
    {
        SelectObject();
    }


    // 초기 환경 셋팅
    private void Initalize()
    {
        m_raycastCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //camera = GameObject.FindObjectOfType<Camera>();
        //Debug.Log("ShapeManager :: Initalize - Done");
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ShapeManager :: Clicked");

            UnityEngine.UI.GraphicRaycaster graphicRaycaster;
            PointerEventData pointerEventData;
            EventSystem eventSystem;

            
            graphicRaycaster = GameObject.Find("MinigameCanvas").GetComponent<UnityEngine.UI.GraphicRaycaster>();
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();


            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;


            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            GameObject selectedShape = m_selectedObject;
            GameObject selectedBackground = null;

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "BlockChild") 
                {
                    selectedShape = result.gameObject;
                    Debug.Log("ShapeManager :: Selected Object - " + result);
                }
            }


            Transform backgroundTransform = null;

            // 오브젝트가 있을 경우, 기존의 오브젝트를 해제 해 주어야 한다.
            if (m_selectedObject)
            {
                backgroundTransform = m_selectedObject.transform.Find("SelectedBackground");
                selectedBackground = backgroundTransform.gameObject;

                if (selectedBackground) 
                {
                    selectedBackground.SetActive(false);
                }
            }

            if (!selectedShape) return;
            m_selectedObject = selectedShape.transform.parent.gameObject;
            backgroundTransform = m_selectedObject.transform.Find("SelectedBackground");

            // 임의 기능. 선택되었을 때 배경으로 오브젝트를 달아준다.
            if (!backgroundTransform) return;
            selectedBackground = backgroundTransform.gameObject;

            if (!selectedBackground) return;
            selectedBackground.SetActive(true);
        }
    }

    private void SelectObjectOld()
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
                if (m_selectedObject != null)
                {
                    selectedBackground = m_selectedObject.transform.Find("SelectedBackground").gameObject;
                    selectedBackground.SetActive(false);
                }

                m_selectedObject = rayHit.collider.gameObject;

                // 임의 기능. 선택되었을 때 배경으로 오브젝트를 달아준다.
                selectedBackground = m_selectedObject.transform.Find("SelectedBackground").gameObject;
                selectedBackground.SetActive(true);
            }
        }
    }

    public string GetSelectedObjectName() 
    {
        return m_selectedObject.name;
    }
    public GameObject GetSelectedObject()
    {
        return m_selectedObject;
    }
}
