using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;   // instance ����

    Vector3 ballResetVector = Vector3.zero;   // �౸�� ���� Vector ��ġ��

    public int[] score = new int[2];   // score[0] : 1��, score[1] : 2�� ���ھ�
    public bool isGoal = false;   // Goal �� ���� �������� �ƴ��� üũ

    private GameObject ball;   // �౸�� ������Ʈ
    private GameObject ballReposition;   // �౸�� ���� ��ġ ������Ʈ

    void Awake()
    {
           // �ʱ� ������ ����
        if (instance == null || instance == default) { instance = this; }   // TestManager instance �ߺ��� �˻�
        else { Destroy(this.gameObject); }

        ball = GameObject.Find("Ball");
        ballReposition = GameObject.Find("BallSpawn");
           // end �ʱ� ������ ����
    }

    void Start()
    {
        ballResetVector = ballReposition.transform.position;   // �౸�� ���� ��ġ�� Vector �� �����Ѵ�
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GoalTeam2();
        }
    }

    public void GoalTeam1()   // 1���� 2���� ��뿡 ���� �־����� ����
    {
        score[0] += 1;   // 1���� score �� 1 ���� �����ش�
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().rotation = Quaternion.identity;
        ball.gameObject.SetActive(false);
        StartCoroutine(GameResetWaitTime());   // ���� ���� �� ��� ������ �ð��� �ִ� �Լ��� �����Ѵ�
    }

    public void GoalTeam2()   // 2���� 1���� ��뿡 ���� �־����� ����
    {
        score[1] += 1;   // 2���� score �� 1 ���� �����ش�
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().rotation = Quaternion.identity;
        ball.gameObject.SetActive(false);
        StartCoroutine(GameResetWaitTime());   // ���� ���� �� ��� ������ �ð��� �ִ� �Լ��� �����Ѵ�
    }

    IEnumerator GameResetWaitTime()   // ���� ���� �� ��� ������ �ð��� �ִ� �Լ�
    {
        Debug.Log("��Ÿ�� ����");
        yield return new WaitForSeconds(3f);   // 3�ʰ� ������ �ð� ���� GameReset() �Լ��� �����Ѵ�
        GameReset();
    }

    public void GameReset()   // ������ �ٽ� ���۵� �� ����Ǵ� �Լ�
    {
        ball.gameObject.SetActive(true);
        ball.transform.position = ballResetVector;   // �౸���� ���� ��ġ�� �̵���Ų��
        Debug.Log("�౸�� ���� �Ϸ�");
    }
}
