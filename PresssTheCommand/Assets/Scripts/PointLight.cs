using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ۼ��� : ������
// �ۼ��� : 2022.11.15
// ��� ��� : MovePosition()�� �̿��Ͽ� Player�� �����ϰԲ� ����.
// ���� ���� : PointLight ��ü ��Ʈ��.
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


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        //Debug.Log("PointLight :: Initalize - Done");
    }


    // Camera follows the player with specified offset position
    // Player�� ���� �� ������ message�� �佺�ϴ� �� ������ ���� �� �� �ִ��� ���� �ʿ�.
    private void MovePosition()
    {
        // Position z�� 0���� �� �־�� �������� Lightning�� �۵���.
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + 0);
    }
}