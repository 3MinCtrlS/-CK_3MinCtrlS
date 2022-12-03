using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ۼ��� : ������
// �ۼ��� : 2022.11.14
// ��� ��� : DetectKeyInput()�� �̿��Ͽ� Ű �Է� ����, wasd�� ������, �浹 ���� (�̴ϰ���)
// ���� ���� : Player ��ü ��Ʈ��.
public class Player : MonoBehaviour
{
    // SerializeField : ����ȭ. �ν����Ϳ��� ���� ����������, �ܺ� ��ũ��Ʈ������ ������ �Ұ����ϵ��� ���� �۾�.
    [SerializeField] private float m_playerSpeed = 1f;
    private Rigidbody2D m_playerRigidBody;


    private void Start() { Initalize(); }

    private void Update()
    {
        DetectKeyInput();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
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

            StartMiniGame();
        }
    }

    private void StartMiniGame() 
    {
        GameManager.Instance.PauseGame();

        int randomMinigame = GameManager.Instance.Random(1, 4);
        MinigameManager.Instance.StartMinigame(randomMinigame);

        //GameManager.Instance.ResumeGame();
    }



}