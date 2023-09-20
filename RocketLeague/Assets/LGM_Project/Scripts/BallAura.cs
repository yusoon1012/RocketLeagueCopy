using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallAura : MonoBehaviourPun
{
    private GameObject ballObj;   // �౸�� ������Ʈ
    private GameObject ballAura;   // �౸�� ǥ�� ������Ʈ
    private Transform ballAuraInlineObj;   // �౸�� ��ġ ǥ���� ���� ǥ�� ������Ʈ

    private float ballY = default;   // �౸�� Y ��ġ��
    private float ballYRes = default;   // �౸�� Y ��ġ ����� ��갪
    private float ballYRes2 = default;   // �౸�� Y ��ġ ����� ��갪 2
    private float auraSize = default;   // �౸�� ��ġ ǥ�� Scale ����� ��갪

    void Awake()
    {
           // �ʱ� ������ ����
        ballObj = GameObject.Find("Ball(Clone)");   // �౸�� ������Ʈ ����
        ballAura = GameObject.Find("BallAuras(Clone)");   // �౸�� ǥ�� ������Ʈ ����
           // �౸�� ǥ�� ������Ʈ�� �ڽ� ������Ʈ ����
        ballAuraInlineObj = ballAura.transform.Find("BallAuraInLine").GetComponent<Transform>();

        ballY = 0f;
        ballYRes = 0f;
        ballYRes2 = 0f;
        auraSize = 0f;
           // end �ʱ� ������ ����
    }

    void Update()
    {
        photonView.RPC("ChangeAura", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void ChangeAura()
    {
        // �౸�� ǥ���� �౸�� X, Z ��ġ�� �̵���Ŵ
        ballAura.GetComponent<Transform>().transform.position = new Vector3(ballObj.transform.position.x, transform.position.y,
            ballObj.transform.position.z);

        ballY = ballObj.transform.position.y - 4f;   // Y �ּҰ��� 4f �����̹Ƿ� �౸���� Y ��ġ������ 4f �� ���ش�
        ballYRes = (ballY / (40f / 100f));   // �౸�� Y ��ġ���� ������� ���Ѵ�

        if (ballYRes > 99f) { ballYRes = 99f; }   // �౸�� Y ��ġ���� �ִ� �Ѱ谪�� ����
        if (ballYRes < 0f) { ballYRes = 0f; }   // �౸�� Y ��ġ���� �ּ� �Ѱ谪�� ����

        ballYRes2 = 100f - ballYRes;   // �౸�� ǥ���� �ִ밪���� �ּҰ����� ������� �ݴ��̹Ƿ� 100f ���� �౸�� Y ������� ���ش�
        auraSize = (6f / 100f) * ballYRes2;   // �౸�� Y ��ġ �������ŭ �౸�� ǥ�� Scale ������� ���Ѵ�

        photonView.RPC("UpdateAura", RpcTarget.All, auraSize);
    }

    [PunRPC]
    public void UpdateAura(float _auraSize)
    {
        ballAuraInlineObj.transform.localScale = new Vector3(_auraSize, _auraSize, 1f);
    }
}
