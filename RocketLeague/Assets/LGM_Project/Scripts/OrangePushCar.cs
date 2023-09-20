using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePushCar : MonoBehaviour
{
    private Vector3 pushVector;   // ������ ���ư� �Ÿ� ���Ͱ�

    private Rigidbody onCarRb;   // ��� �Ÿ��� ���� ������ �����ٵ�

    private void OnTriggerEnter(Collider collision)
    {
           // �� �±װ� �޷��ִ� ������Ʈ�� push �ݶ��̴� �ȿ� ������� ����
        if ((collision.tag == "Car_Blue" || collision.tag == "Car_Orange"))
        {
            onCarRb = collision.gameObject.GetComponent<Rigidbody>();   // ���ư� ������ �����ٵ� �����´�
            pushVector = collision.transform.position - transform.position;   // ���� �� ��ġ���� ���� ���� ��ġ�� ����
            pushVector = pushVector.normalized;   // ���� ��ġ���� 1 �� ������ �������ش�
            pushVector += Vector3.up * 80;   // ���� ��ġ���� �������� ������Ų��
            pushVector += Vector3.right * 120;   // ���� ��ġ���� ���ư� �Ÿ����� ������Ų��
            onCarRb.AddForce(pushVector * 800, ForceMode.Impulse);   // ���� ȿ�� �������� �ִ� �� ���� AddForce �� ���� �� �о��
        }
    }
}
