using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 작성자 : 전시은
// 작성일 : 2022.11.14
// 사용 방법 : DetectKeyInput()을 이용하여 키 입력 감지, wasd로 움직임, 충돌 감지 (미니게임)
// 제작 사유 : Player 객체 컨트롤.
public class Player : MonoBehaviour
{
    // SerializeField : 직렬화. 인스펙터에서 접근 가능하지만, 외부 스크립트에서는 접근이 불가능하도록 막는 작업.
    [SerializeField] private float m_playerSpeed = 1f;
    private Rigidbody2D m_playerRigidBody;


    private void Start() { Initalize(); }

    private void Update()
    {
        DetectKeyInput();
    }


    // 초기 환경 셋팅
    private void Initalize()
    {
        //Debug.Log("Player :: Initalize - Done");
    }

    // 키 입력 감지
    private void DetectKeyInput()
    {
        // To-Do : 기타 입력또한 처리 해 주어야 함.

        MovePosition();
    }

    // 움직임 처리
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