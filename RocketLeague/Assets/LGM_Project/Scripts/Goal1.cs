using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal1 : MonoBehaviour
{
    private Rigidbody goalLineRb;   // ��� �����ٵ�
    private Collider goalLineCd;   // ��� �ݶ��̴�

    void Awake()
    {
        // �ʱ� ������ ����
        goalLineRb = GetComponent<Rigidbody>();
        goalLineCd = GetComponent<Collider>();
        // end �ʱ� ������ ����
    }

    private void OnTriggerEnter(Collider collision)   // �౸���� ��� �� �ݶ��̴��� ������ ����
    {
        // �ݶ��̴��� ���� ������Ʈ�� "Ball" �±��̰�, TestManager isGoal ���� false �� �� ����
        if (collision.tag == "Ball" && TestManager.instance.isGoal == false)
        {
            TestManager.instance.isGoal = true;   // TestManager isGoal ���� true �� ������ ���� ���� ���·� �ٲ�
            TestManager.instance.GoalTeam2();
            Debug.Log("Goal!");
        }
    }
}
