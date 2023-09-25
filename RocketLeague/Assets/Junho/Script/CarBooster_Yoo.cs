using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class CarBooster_Yoo : MonoBehaviourPun
{
    private float newBoost;

    BoostUI_Yoo boostUI;
    public bool useBoost { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �߰�
        if (!photonView.IsMine)
        {
            // �� �κ� ���� �ʿ� ���� ���Ĵٵ� �� �ǵ�� �۾� ���� ���� �� ���� ������ �ٷ� ���� ����
            // ������ ĵ������ ���� �ʿ䰡 ����, ���ӸŴ��� ���� ĵ������ ������ �ν��� UI�� ���� ����
            //boostUI = transform.GetChild(0).GetChild(0).GetChild(8).GetComponentInChildren<BoostUI_Yoo>();
            //boostUI.gameObject.SetActive(false);
            return;
        }

        useBoost = false;
        // �� �Ʒ����� �������� ��ġ�� �����ؾ���
        //boostUI = transform.GetChild(0).GetChild(0).GetChild(8).GetComponentInChildren<BoostUI_Yoo>();

        boostUI = FindFirstObjectByType<BoostUI_Yoo>();
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

        newBoost = boostUI.boost;

        //Debug.Log("���� �ν��� ������: " +  boost);
        //boostUI.SetBoostGauge(boost);

        if (GameManager.instance.gameStartCheck == false) { return; }

        if (Input.GetMouseButton(0))
        {
            if(boostUI.boost == 0)
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
        if (boostUI.boost == 100)
        {
            return;
        }
        else
        {
            newBoost += boostGauge;

            if(newBoost > 100)
            {
                newBoost = 100;
            }
        }

        boostUI.SetBoostGauge(newBoost);
    }

    // �ν��� ������ ��� �Լ�
    public void UseBoost()
    {
        if(boostUI.boost <= 0)
        {
            useBoost = false;
            boostUI.SetBoost(0);
            return;
        }
        
        boostUI.SetNewBoost(boostUI.newBoost - 0.666f);
        boostUI.SetBoost(boostUI.boost - 0.666f);
    }
}
