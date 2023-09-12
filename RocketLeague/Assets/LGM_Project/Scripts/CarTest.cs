using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour
{
    Vector3 pushVector = Vector3.zero;   // ��� �� RC ī���� �о Vector3 ��ġ ��

    private Rigidbody carRb;   // RC ī�� �����ٵ�

    void Awake()
    {
           // �ʱ� ������ ����
        carRb = GetComponent<Rigidbody>();
           // end �ʱ� ������ ����
    }

    public void GoalEffect(Vector3 _EffectPosition)   // ���� ȿ�� �������� ������ �۵��Ǵ� �Լ�
    {
        pushVector = transform.position - _EffectPosition;   // ���� RC ī ��ġ���� ���� ���� ��ġ�� ����
        pushVector = pushVector.normalized;   // ���� ��ġ���� 1 �� ������ �������ش�
        pushVector += Vector3.up * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��
        pushVector += Vector3.forward * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��.
        carRb.AddForce(pushVector * 30, ForceMode.Impulse);   // ���� ȿ�� �������� �ִ� RC ī���� AddForce �� ���� �� �о��
    }
}
