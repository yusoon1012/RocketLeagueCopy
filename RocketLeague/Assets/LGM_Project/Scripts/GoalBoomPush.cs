using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBoomPush : MonoBehaviour
{
    private Collider[] boomCd = new Collider[8];   // �� ȿ���� �о RC ī �迭
    private GameObject car;   // �� ȿ���� �о RC ī ������Ʈ

    public void BoomOn(Vector3 _ballPosition)   // �� ȿ���� ���� RC ī �о�� �Լ�
    {
        transform.Translate(_ballPosition);   // �� ������Ʈ ��ġ�� ���� �� ������ �̵���Ų��

        boomCd = Physics.OverlapSphere(transform.position, 100f);   // �� ������Ʈ ��ġ�� �߽����� 100f ������ �ȿ� �ִ� ������Ʈ���� ã��
        for (int i = 0; i < boomCd.Length; i++)   // ã�� ������Ʈ �� ��ŭ for ���� �۵���Ŵ
        {
            if (boomCd[i].tag == ("Car"))   // ������Ʈ �±װ� "Car" �� ��쿡�� �Ʒ� ����� �۵�
            {
                car = boomCd[i].gameObject;   // �±׷� ã�� ������Ʈ�� car ������Ʈ�� ����
                car.GetComponent<CarTest>().GoalEffect(_ballPosition);   // car ������Ʈ�� "GoalEffect" �Լ��� ������ ��ġ�� �Բ� ����
            }
        }
    }
}
