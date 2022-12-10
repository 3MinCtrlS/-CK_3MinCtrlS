using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �ۼ��� : ������
// �ۼ��� : 2022.11.14
// ��� ��� : DetectKeyInput()�� �̿��Ͽ� Ű �Է� ����, wasd�� ������, �浹 ���� (�̴ϰ���)
// ���� ���� : Player ��ü ��Ʈ��.
public class Player : MonoBehaviour
{
    // SerializeField : ����ȭ. �ν����Ϳ��� ���� ����������, �ܺ� ��ũ��Ʈ������ ������ �Ұ����ϵ��� ���� �۾�.
    [SerializeField] private float m_playerSpeed = 1f;
    private Rigidbody2D m_playerRigidBody;
    private GameObject m_triggeredObject;
    [SerializeField] private GameObject m_clearScene;
    [SerializeField] private GameObject m_clearButton;
    [SerializeField] private GameObject m_cutSceneCanvas;


    private void Start() { Initalize(); }

    private void Update()
    {
        DetectKeyInput();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        if (m_clearScene) 
        {
            m_clearScene.GetComponent<Image>().enabled = false;
        }
        if (m_clearButton) 
        {
            m_clearButton.GetComponent<Image>().enabled = false;
            m_clearButton.GetComponent<Button>().enabled = false;
        }
        //Debug.Log("Player :: Initalize - Done");
    }

    // Ű �Է� ����
    private void DetectKeyInput()
    {
        // To-Do : ��Ÿ �Է¶��� ó�� �� �־�� ��.

        MovePosition();
    }

    // ������ ó��
    private void MovePosition()
    {
        Vector2 currentPosition = transform.position;

        if (Input.GetKey("w"))
        {
            currentPosition.y += m_playerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("a"))
        {
            currentPosition.x -= m_playerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            currentPosition.y -= m_playerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            currentPosition.x += m_playerSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WallMinigame")
        {
            Debug.Log("Player :: OnTriggerEnter entered WallMinigame");

            m_triggeredObject = other.gameObject;
            StartMiniGame();
        }
        if (other.name == "Goal") 
        {
            if (m_clearScene)
            {
                m_cutSceneCanvas.SetActive(true);
                m_cutSceneCanvas.GetComponent<CutScene>().ClearScene();
                m_clearScene.GetComponent<Image>().enabled = true;
            }
            if (m_clearButton)
            {
                m_clearButton.GetComponent<Image>().enabled = true;
                m_clearButton.GetComponent<Button>().enabled = true;
            }

        }
    }

    public GameObject GetTriggeredObject() 
    {
        return m_triggeredObject;
    }

    private void StartMiniGame() 
    {
        GameManager.Instance.PauseGame();

        // MinigameBrush ��Ʈ�� ���� �����ϰ� �Ǿ� 1~3���� ���.
        int randomMinigame = GameManager.Instance.Random(1, 100) % 2 + 2;
        MinigameManager.Instance.StartMinigame(randomMinigame);
    }
}