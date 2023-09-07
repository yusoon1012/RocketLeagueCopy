using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� ��ġ
        Vector3 rayStart = transform.position;
        // ������ ���� (����������)
        Vector3 rayDirection = Vector3.forward;
        // ������ �ִ� �Ÿ�
        float rayDistance = 3f;

        // ����ĳ��Ʈ�� ����Ͽ� �浹 �˻�
        RaycastHit hitWall;
        if (Physics.Raycast(rayStart, rayDirection, out hitWall, rayDistance, layerMask))
        {
            // �浹�� ������Ʈ�� �̸��� �����ͼ� ���
            string name = hitWall.collider.name;
            Debug.Log("Hit object name: " + name);

            // ���� �ð������� ǥ��
            Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.red);
        }
        else
        {
            // ���� �ð������� ǥ�� (���̰� �浹���� ���� ���)
            Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.green);
        }

    }
}
