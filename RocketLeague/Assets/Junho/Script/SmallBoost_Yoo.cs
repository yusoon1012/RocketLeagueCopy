using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoost_Yoo : MonoBehaviour
{
    private CarBooster_Yoo carBooster;
    private float regenTime;
    private float timeAfterUse;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        regenTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // �ν����� �������� 0�̶��
        if(gameObject.transform.localScale == Vector3.zero)
        {
            // ������� �ð��� ������Ŵ
            timeAfterUse += Time.deltaTime;
            //Debug.LogFormat("�ۺ� ��������� �����ð�:" + (regenTime - timeAfterUse));
            // ������� �ð��� ����� �ð��̻��̸�
            if(timeAfterUse >= regenTime)
            {
                // �ν����� �������� 2,2,2�� �ʱ�ȭ �� ������� �ð� 0���� �ʱ�ȭ
                gameObject.transform.localScale = Vector3.one * 2;
                //Debug.Log("�ۺ� �����Ϸ�");
                timeAfterUse = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("�������?");
        // �ڵ����� �ε��� ��� �ε��� �ڵ����� ��ũ��Ʈ�� �ҷ��ͼ� �ν��� ������ �����Լ��� �ߵ���Ŵ
        if (collision.CompareTag("Car"))
        {
            //Debug.Log("����� ����?");
            if (collision.gameObject.transform.parent.gameObject.GetComponentInParent<CarBooster_Yoo>() != null)
            {
                carBooster = collision.gameObject.transform.parent.gameObject.GetComponentInParent<CarBooster_Yoo>();

                carBooster.AddBoost(12);

                gameObject.transform.localScale = Vector3.zero;
            }
        }
    }
}
