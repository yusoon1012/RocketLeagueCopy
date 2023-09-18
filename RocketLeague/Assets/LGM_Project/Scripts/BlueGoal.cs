using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGoal : MonoBehaviour
{
    //private Vector3 ballPosition = Vector3.zero;   // �ݶ��̴� �ȿ� ���� �౸�� ��ġ�� Vecter
    //private Rigidbody goalLineRb;   // ��� �����ٵ�
    //private GameObject goalBoomObject;   // GoalBoomPusher ������Ʈ ����

    public Collider goalLineCd;   // ��� �ݶ��̴�
    public Collider pushCd;   // �� ������ �о�� ���� �ݶ��̴�

    private GameObject onCarOj;
    private Transform onCarChild;
    private GameObject goalPost;
    private Rigidbody onCarRb;
    private Vector3 pushVector;

    void Awake()
    {
        goalPost = GameObject.Find("BlueGoal");
    }

    private void OnTriggerEnter(Collider collision)   // �౸���� ��� �� �ݶ��̴��� ������ ����
    {
           // �ݶ��̴��� ���� ������Ʈ�� "Ball" �±��̰�, GameManager �� isGoaled ���� false �̸� �� ����
        if (collision.tag == "Ball" && GameManager.instance.isGoaled == false)
        {
            GameManager.instance.isGoaled = true;   // GameManager �� isGoaled ���� true �� ������ ���� ���� ���·� �ٲ�
            pushCd.enabled = true;
            GameManager.instance.GoalTeam2(1);   // TestManager �� score �� �����ִ� �Լ��� ����

            Debug.Log("�� ����");

            StartCoroutine(GoalPushDelay());

            //   // �ݶ��̴��� ���� �౸�� ��ġ�� Vector3 ����
            //ballPosition = new Vector3(collision.transform.position.x, collision.transform.position.y,
            //    collision.transform.position.z);

            //goalBoomObject.GetComponent<GoalBoomPush>().BoomOn(ballPosition);   // GoalBoomPusher ������Ʈ�� �౸�� ��ġ���� �Բ� ����
        }

        if ((collision.tag == "Car_Blue" || collision.tag == "Car_Orange") && GameManager.instance.isGoaled == true)
        {
            onCarOj = collision.gameObject;
            onCarChild = transform.Find("Collider");
            Debug.Log(onCarChild.name);
            onCarRb = onCarChild.GetComponent<Rigidbody>();
            Debug.Log(onCarRb.name);
            pushVector = collision.transform.position - goalPost.transform.position;   // ���� �� ��ġ���� ���� ���� ��ġ�� ����
            pushVector = pushVector.normalized;   // ���� ��ġ���� 1 �� ������ �������ش�
            pushVector += Vector3.up * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��
            pushVector += Vector3.forward * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��
            onCarRb.AddForce(pushVector * 20, ForceMode.Impulse);   // ���� ȿ�� �������� �ִ� �� ���� AddForce �� ���� �� �о��

            Debug.Log("Ǫ�� ����");
        }
    }

    IEnumerator GoalPushDelay()
    {
        yield return new WaitForSeconds(0.5f);

        pushCd.enabled = false;
    }
}
