using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeGoal : MonoBehaviour
{
    public Collider goalLineCd;   // ��� �ݶ��̴�
    public Collider pushCd;   // �� ���� Ǫ�� �ݶ��̴�

    private void OnTriggerEnter(Collider collision)   // �౸���� ��� �� �ݶ��̴��� ������ ����
    {
           // �ݶ��̴��� ���� ������Ʈ�� "Ball" �±��̰�, GameManager �� isGoaled ���� false �̸� �� ����
        if (collision.tag == "Ball")
        {
            //GameManager.instance.isGoaled = true;   // GameManager �� isGoaled ���� true �� ������ ���� ���� ���·� �ٲ�
            pushCd.enabled = true;   // push �ݶ��̴��� Ȱ��ȭ ��Ų��
            GameManager.instance.BlueScoreUp();   // TestManager �� score �� �����ִ� �Լ��� ����
            
            StartCoroutine(GoalPushDelay());   // ���� �� Ȱ��ȭ �� �ݶ��̴��� �ٽ� ��Ȱ��ȭ ��Ű�� �Լ�
        }
    }

    IEnumerator GoalPushDelay()
    {
        yield return new WaitForSeconds(0.5f);

        pushCd.enabled = false;   // Ȱ��ȭ �Ǿ� �ִ� �ݶ��̴��� �ٽ� ��Ȱ��ȭ ��Ų��
    }
}
