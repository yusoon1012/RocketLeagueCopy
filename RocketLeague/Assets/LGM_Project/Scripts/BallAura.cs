using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAura : MonoBehaviour
{
    Vector3 ballPosition = Vector3.zero;   // �౸�� ��ġ Vecter3 ��ġ��

    private GameObject ballObj;   // �౸�� ������Ʈ
    private GameObject ballAuraInlineObj;   // �౸�� ��ġ ǥ���� ���� ǥ�� ������Ʈ

    private float ballY = default;   // �౸�� Y ��ġ��
    private float ballYRes = default;   // �౸�� Y ��ġ ����� ��갪
    private float auraSize = default;   // �౸�� ��ġ ǥ�� Scale ����� ��갪

    void Awake()
    {
           // �ʱ� ������ ����
        ballObj = GameObject.Find("Ball");
        ballAuraInlineObj = GameObject.Find("BallAuraInLine");

        ballY = 0f;
        ballYRes = 0f;
        auraSize = 0f;
           // end �ʱ� ������ ����
    }

    void Update()
    {
           // ���� �౸�� ��ġ Vecter3 ��ġ��
        ballPosition = new Vector3(ballObj.transform.position.x, transform.position.y, ballObj.transform.position.z);
        transform.position = ballPosition;   // �౸�� ǥ���� �౸�� X, Z ��ġ�� �̵���Ŵ

        ballY = ballObj.transform.position.y - 4f;   // Y �ּҰ��� 4f �����̹Ƿ� �౸���� Y ��ġ������ 4f �� ���ش�
        ballYRes = (ballY / (40f / 100f));   // �౸�� Y ��ġ���� ������� ���Ѵ�

        if (ballYRes > 99f) { ballYRes = 99f; }   // �౸�� Y ��ġ���� �ִ� �Ѱ谪�� ����
        if (ballYRes < 0f) { ballYRes = 0f; }   // �౸�� Y ��ġ���� �ּ� �Ѱ谪�� ����

        ballYRes = 100f - ballYRes;   // �౸�� ǥ���� �ִ밪���� �ּҰ����� ������� �ݴ��̹Ƿ� 100f ���� �౸�� Y ������� ���ش�
        auraSize = (6f / 100f) * ballYRes;   // �౸�� Y ��ġ �������ŭ �౸�� ǥ�� Scale ������� ���Ѵ�

        ballAuraInlineObj.transform.localScale = new Vector3(auraSize, auraSize, 1f);   // �౸�� Y ��ġ �������ŭ �౸�� ǥ�� Scale
                                                                                        // ���� �������ش�
    }
}
