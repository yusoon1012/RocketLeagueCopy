using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallAura : MonoBehaviourPun
{
    Vector3 ballPosition;   // �౸�� ��ġ Vecter3 ��ġ��
    Vector3 ballAuraSize;

    private GameObject ballObj;   // �౸�� ������Ʈ
    public GameObject ballAura;
    public GameObject ballAuraInlineObj;   // �౸�� ��ġ ǥ���� ���� ǥ�� ������Ʈ

    private float ballY = default;   // �౸�� Y ��ġ��
    private float ballYRes = default;   // �౸�� Y ��ġ ����� ��갪
    private float auraSize = default;   // �౸�� ��ġ ǥ�� Scale ����� ��갪
    private bool startDelayTime = false;

    void Awake()
    {
        // �ʱ� ������ ����
        ballObj = GameObject.Find("Ball(Clone)").GetComponent<GameObject>();
        //ballAura = GetComponent<GameObject>();
        //ballAuraInlineObj = GetComponent<GameObject>();

        ballY = 0f;
        ballYRes = 0f;
        auraSize = 0f;
        // end �ʱ� ������ ����
    }

    void Update()
    {
        // ���� �౸�� ��ġ Vecter3 ��ġ��
        ballPosition = new Vector3(ballObj.transform.position.x, ballAuraInlineObj.transform.position.y, ballObj.transform.position.z);
        transform.position = ballPosition;   // �౸�� ǥ���� �౸�� X, Z ��ġ�� �̵���Ŵ

        ballY = ballObj.transform.position.y - 4f;   // Y �ּҰ��� 4f �����̹Ƿ� �౸���� Y ��ġ������ 4f �� ���ش�
        ballYRes = (ballY / (40f / 100f));   // �౸�� Y ��ġ���� ������� ���Ѵ�

        if (ballYRes > 99f) { ballYRes = 99f; }   // �౸�� Y ��ġ���� �ִ� �Ѱ谪�� ����
        if (ballYRes < 0f) { ballYRes = 0f; }   // �౸�� Y ��ġ���� �ּ� �Ѱ谪�� ����

        ballYRes = 100f - ballYRes;   // �౸�� ǥ���� �ִ밪���� �ּҰ����� ������� �ݴ��̹Ƿ� 100f ���� �౸�� Y ������� ���ش�
        auraSize = (6f / 100f) * ballYRes;   // �౸�� Y ��ġ �������ŭ �౸�� ǥ�� Scale ������� ���Ѵ�

        ballAuraSize = new Vector3(auraSize, auraSize, 1f);
        ballAuraInlineObj.transform.localScale = ballAuraSize;

        //FixAuraSize();   // �౸�� ǥ�� ������ PunRPC ����ȭ ����
    }

    //[PunRPC]
    //public void FixAuraSize()   // �౸�� ǥ�� ������ PunRPC ����ȭ ����
    //{
    //    if (PhotonNetwork.IsMasterClient)   // ������ Ŭ���̾�Ʈ�� �౸�� ǥ�� �������� �����Ѵ�
    //    {
    //        ballAuraInlineObj.transform.localScale = new Vector3(auraSize, auraSize, 1f);   // �౸�� Y ��ġ �������ŭ �౸�� ǥ�� Scale ���� �������ش�

    //        photonView.RPC("FixAuraSize", RpcTarget.Others, auraSize);   // �ٸ� ���õ鿡�� �౸�� ǥ�� ������ ������ ���� �����Ѵ�
    //    }
    //}
}
