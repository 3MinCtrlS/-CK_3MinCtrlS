using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : 전시은
// 작성일 : 2022.11.15
// 사용 방법 : MovePosition()을 이용하여 Player를 추적하게끔 동작.
// 제작 사유 : PointLight 객체 컨트롤.
public class PointLight : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;


    // Start is called before the first frame update
    private void Start() { Initalize(); }

    // Update is called once per frame
    private void Update()
    {
        MovePosition();
    }


    // 초기 환경 셋팅
    private void Initalize()
    {
        //Debug.Log("PointLight :: Initalize - Done");
    }


    // Camera follows the player with specified offset position
    // Player가 무빙 할 때에만 message를 토스하는 등 동작을 제어 할 수 있는지 검토 필요.
    private void MovePosition()
    {
        // Position z를 0으로 해 주어야 정상적인 Lightning이 작동함.
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + 0);
    }
}