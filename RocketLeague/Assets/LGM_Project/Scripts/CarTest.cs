using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarTest : MonoBehaviourPun
{
    Vector3 pushVector = Vector3.zero;   // ��� �� RC ī���� �о Vector3 ��ġ ��

    private Rigidbody carRb;   // RC ī�� �����ٵ�

    void Awake()
    {
           // �ʱ� ������ ����
        carRb = GetComponent<Rigidbody>();
           // end �ʱ� ������ ����
    }

    [PunRPC]
    public void GoalEffect(Vector3 _EffectPosition)   // ���� ȿ�� �������� ������ �۵��Ǵ� �Լ�
    {
        pushVector = transform.position - _EffectPosition;   // ���� RC ī ��ġ���� ���� ���� ��ġ�� ����
        pushVector = pushVector.normalized;   // ���� ��ġ���� 1 �� ������ �������ش�
        pushVector += Vector3.up * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��
        pushVector += Vector3.forward * 5;   // ���� ��ġ���� 5 ��ŭ �������� ������Ų��.

        if (PhotonNetwork.IsMasterClient)
        {
            carRb.AddForce(pushVector * 20, ForceMode.Impulse);   // ���� ȿ�� �������� �ִ� RC ī���� AddForce �� ���� �� �о��

            ApplyGoalEffect(_EffectPosition);
        }
    }

    public void ApplyGoalEffect(Vector3 EffectPosition_)
    {
        photonView.RPC("GoalEffect", RpcTarget.Others, EffectPosition_);
    }
}
