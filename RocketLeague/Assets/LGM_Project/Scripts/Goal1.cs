using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal1 : MonoBehaviour
{
    private Vector3 ballPosition = Vector3.zero;   // �ݶ��̴� �ȿ� ���� �౸�� ��ġ�� Vecter

    private Rigidbody goalLineRb;   // ��� �����ٵ�
    private Collider goalLineCd;   // ��� �ݶ��̴�
    private GameObject goalBoomObject;   // GoalBoomPusher ������Ʈ ����

    void Awake()
    {
           // �ʱ� ������ ����
        goalBoomObject = GameObject.Find("GoalBoomPusher");

        goalLineRb = GetComponent<Rigidbody>();
        goalLineCd = GetComponent<Collider>();
           // end �ʱ� ������ ����
    }

    private void OnTriggerEnter(Collider collision)   // �౸���� ��� �� �ݶ��̴��� ������ ����
    {
           // �ݶ��̴��� ���� ������Ʈ�� "Ball" �±��̰�, TestManager isGoal ���� false �� �� ����
        //if (collision.tag == "Ball" && TestManager.instance.isGoal == false)
        //{
        //    TestManager.instance.isGoal = true;   // TestManager isGoal ���� true �� ������ ���� ���� ���·� �ٲ�
        //    TestManager.instance.GoalTeam2();   // TestManager �� score �� �����ִ� �Լ��� ����

        //       // �ݶ��̴��� ���� �౸�� ��ġ�� Vector3 ����
        //    ballPosition = new Vector3(collision.transform.position.x, collision.transform.position.y,
        //        collision.transform.position.z);

        //    goalBoomObject.GetComponent<GoalBoomPush>().BoomOn(ballPosition);   // GoalBoomPusher ������Ʈ�� �౸�� ��ġ���� �Բ� ����
        //}
    }
}
