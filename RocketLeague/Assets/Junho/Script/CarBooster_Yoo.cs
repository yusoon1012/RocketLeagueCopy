using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBooster_Yoo : MonoBehaviourPun
{
    public float boost { get; private set; }

    BoostUI_Yoo boostUI;
    public bool useBoost { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �߰�
        if (!photonView.IsMine)
        {
            boostUI = transform.GetChild(0).GetChild(0).GetChild(8).GetComponentInChildren<BoostUI_Yoo>();
            boostUI.gameObject.SetActive(false);
            return;
        }

        boost = 33;
        useBoost = false;
        boostUI = transform.GetChild(0).GetChild(0).GetChild(8).GetComponentInChildren<BoostUI_Yoo>();
    }

    private void FixedUpdate()
    {
        // ���� ���� �߰�
        if (!photonView.IsMine)
        {
            return;
        }

        if (useBoost)
        {
            UseBoost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �߰�
        if (!photonView.IsMine)
        {
            return;
        }

        //Debug.Log("���� �ν��� ������: " +  boost);
        boostUI.SetBoostGauge(boost);

        if(Input.GetMouseButton(0))
        {
            if(boost == 0)
            {
                //useBoost = false;
                return;
            }
            useBoost = true;
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            useBoost = false;
        }
    }

    // �ν��� ������ ���� �Լ�
    public void AddBoost(int boostGauge)
    {
        if (boost == 100)
        {
            return;
        }
        for (int i = 0; i < boostGauge; i ++)
        {
            boost += 1;
            if (boost > 100)
            {
                boost = 100;
            }
        }
    }

    // �ν��� ������ ��� �Լ�
    public void UseBoost()
    {
        if(boost == 0)
        {
            useBoost = false;
            return;
        }


        boost -= 1;
    }
}
