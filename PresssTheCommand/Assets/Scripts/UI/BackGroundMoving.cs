using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMoving : MonoBehaviour
{
    Vector3 pos; //������ġ
    public float delta = 3.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 0.5f; // �̵��ӵ�

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        v.y += delta * Mathf.Sin(Time.time * speed / 2);
        transform.position = v;
    }
}