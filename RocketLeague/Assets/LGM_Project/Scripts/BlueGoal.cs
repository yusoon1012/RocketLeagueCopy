using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class BlueGoal : MonoBehaviourPun
{
    public Collider goalLineCd;   // ��� �ݶ��̴�
    public Collider pushCd;   // �� ���� Ǫ�� �ݶ��̴�

    private void OnTriggerEnter(Collider collision)   // �౸���� ��� �� �ݶ��̴��� ������ ����
    {
           // �ݶ��̴��� ���� ������Ʈ�� "Ball" �±��̰�, GameManager �� isGoaled ���� false �̸� �� ����
        if (collision.tag == "Ball" && GameManager.instance.isGoaled == false)
        {
            if (GameManager.instance.overtimeCheck == false)
            {
                GameManager.instance.ResetGame();   // GameManager �� �ִ� ���� ���� ���� �Լ��� ����
            }
            else
            {
                GameManager.instance.OvertimeOrangeTeamWin();
            }

            GameManager.instance.GoalCheck();   // �� ���� �� �� �ߺ��� ���� ���� ���ӸŴ����� �ִ� GoalCheck �Լ��� �����Ѵ�
            GameManager.instance.OrangeScoreUp();   // GameManager �� score �� �����ִ� �Լ��� ����
            
               // ������ Ŭ���̾�Ʈ���� Ǫ�� �ݶ��̴��� Ȱ��ȭ �ϵ��� �����Ѵ�
            photonView.RPC("PushColliderOn", RpcTarget.MasterClient);
            Ball_Ys ball = collision.gameObject.GetComponent<Ball_Ys>();
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                Player player = PhotonNetwork.PlayerList[i];

                if (player.NickName == ball.orangeteamName)
                {
                    string name = ball.orangeteamName;
                    player.AddScore(100);
                    GameManager.instance.GoalTextUpdate(name);
                }
            }
        }
    }

    [PunRPC]
    public void PushColliderOn()   // Ǫ�� �ݶ��̴��� Ȱ��ȭ �����ִ� �Լ�
    {
        pushCd.enabled = true;   // Ǫ�� �ݶ��̴��� Ȱ��ȭ
        photonView.RPC("ApplyPushCollider", RpcTarget.AllBuffered, true);   // Ȱ��ȭ ���� ��� Ŭ���̾�Ʈ�� ����ȭ

        StartCoroutine(GoalPushDelay());   // �� �� �� Ȱ��ȭ �� �ݶ��̴��� �ٽ� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ �Լ�
    }

    [PunRPC]
    public void PushColliderOff()   // Ǫ�� �ݶ��̴��� ��Ȱ��ȭ �����ִ� �Լ�
    {
        pushCd.enabled = false;   // Ǫ�� �ݶ��̴��� ��Ȱ��ȭ
        photonView.RPC("ApplyPushCollider", RpcTarget.AllBuffered, false);   // Ǫ�� �ݶ��̴� ��Ȱ��ȭ�� ��� Ŭ���̾�Ʈ�� ����ȭ
    }

    [PunRPC]
    public void ApplyPushCollider(bool state)   // Ǫ�� �ݶ��̴��� ��� Ŭ���̾�Ʈ�� ����ȭ �ϴ� �Լ�
    {
        pushCd.enabled = state;   // Ǫ�� �ݶ��̴��� ��� Ŭ���̾�Ʈ�� ����ȭ
    }

    IEnumerator GoalPushDelay()   // �� �� �� Ǫ�� �ݶ��̴��� ��Ȱ��ȭ �����ִ� �ڷ�ƾ �Լ�
    {
        yield return new WaitForSeconds(0.5f);

           // ������ Ŭ���̾�Ʈ���� Ǫ�� �ݶ��̴� ��Ȱ��ȭ ��� �Լ��� �����ϵ��� �Ѵ�
        photonView.RPC("PushColliderOff", RpcTarget.MasterClient);
    }
}
