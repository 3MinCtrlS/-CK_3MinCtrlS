using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ۼ��� : ������
// �ۼ��� : 2022.11.14
// ��� ��� : MovePosition()�� �̿��Ͽ� Player�� �����ϰԲ� ����.
// ���� ���� : Camera ��ü ��Ʈ��.
public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        MovePosition();
    }


    // �ʱ� ȯ�� ����
    private void Initalize()
    {
        Debug.Log("Camera :: Initalize - Done");
    }


    // Camera follows the player with specified offset position
    // Player�� ���� �� ������ message�� �佺�ϴ� �� ������ ���� �� �� �ִ��� ���� �ʿ�.
    private void MovePosition()
    {
        // Position z�� -10���� �� �־�� �������� Camera Sight�� �۵���.
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10);
    }
}