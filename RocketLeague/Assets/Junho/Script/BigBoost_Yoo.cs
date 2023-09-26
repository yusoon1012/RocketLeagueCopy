using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BigBoost_Yoo : MonoBehaviourPun
{
    private BoostUI_Yoo boostUI;
    private CarBooster_Yoo carBooster;
    private Collider boostCollider;
    private float regenTime;
    private float timeAfterUse;

    // Start is called before the first frame update
    void Start()
    {
        boostUI = FindFirstObjectByType<BoostUI_Yoo>();
        gameObject.transform.localScale = Vector3.zero;
        boostCollider = GetComponent<Collider>();
        regenTime = 10f;
        if (PhotonNetwork.IsMasterClient)
        {
            DoScaleOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        // �ν����� �������� 0�̶��
        if (gameObject.transform.localScale == Vector3.zero)
        {
            // ������� �ð��� ������Ŵ
            timeAfterUse += Time.deltaTime;
            //Debug.LogFormat("ū�� ��������� �����ð�:" + (regenTime - timeAfterUse));
            // ������� �ð��� ����� �ð��̻��̸�
            if (timeAfterUse >= regenTime)
            {
                // �ν����� �������� 2,2,2�� �ʱ�ȭ �� ������� �ð� 0���� �ʱ�ȭ
                DoScaleOn();

                //gameObject.transform.localScale = Vector3.one * 2;

                //boostCollider.enabled = true;
                //Debug.Log("ū�� �����Ϸ�");
                timeAfterUse = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("�������?");
        // �ڵ����� �ε��� ��� �ε��� �ڵ����� ��ũ��Ʈ�� �ҷ��ͼ� �ν��� ������ �����Լ��� �ߵ���Ŵ
        if (collision.CompareTag("Car_Orange")||collision.CompareTag("Car_Blue"))
        {
            if(collision.gameObject.name != "Body")
            {
                return;
            }

            //Debug.Log("����� ����?");
            if (collision.gameObject.transform.parent.parent.gameObject.GetComponent<CarBooster_Yoo>() != null)
            {
                carBooster = collision.gameObject.transform.parent.parent.gameObject.GetComponent<CarBooster_Yoo>();

                carBooster.AddBoost(100);

                if (PhotonNetwork.IsMasterClient)
                {
                    DoScaleOff();
                }

                //gameObject.transform.localScale = Vector3.zero;

                //boostCollider.enabled = false;
            }
        }
    }

    [PunRPC]
    void ScaleOff()
    {
        gameObject.transform.localScale = Vector3.zero;

        boostCollider.enabled = false;
    }

    [PunRPC]
    void ScaleOn()
    {
        gameObject.transform.localScale = Vector3.one * 2;

        boostCollider.enabled = true;
    }

    void DoScaleOn()
    {
        photonView.RPC("ScaleOn", RpcTarget.AllBuffered);
    }

    void DoScaleOff()
    {
        photonView.RPC("ScaleOff", RpcTarget.AllBuffered);
    }
}
