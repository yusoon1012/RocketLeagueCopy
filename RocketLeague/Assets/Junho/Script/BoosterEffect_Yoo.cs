using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoosterEffect_Yoo : MonoBehaviourPun
{
    NewCar car;
    CarBooster_Yoo booster;
    public GameObject boostLevel1 { get; private set; }
    public GameObject boostLevel2 { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<NewCar>();
        booster = GetComponent<CarBooster_Yoo>();
        boostLevel1 = transform.GetChild(0).GetChild(0).Find("BoostLevel1").gameObject;
        boostLevel2 = transform.GetChild(0).GetChild(0).Find("BoostLevel2").gameObject;
        Debug.Log(boostLevel1.gameObject.name);
        Debug.Log(boostLevel2.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("������ ������� ����");
            return;
        }

        // �ν��� ����� �ƴϰ�, 2�� �ν��� ����� �ƴϰ�, ���� �ν��� ���°� �ƴϸ� ����Ʈ ��
        if (booster.useBoost == false && car.useSecondBoost == false && car.outOfControl == false)
        {
            //photonView.RPC("TwoBoostOff", RpcTarget.All, boostLevel1, boostLevel2);

            //boostLevel1.SetActive(false);
            //boostLevel2.SetActive(false);
            //if (photonView.IsMine)
            //{
            //    photonView.RPC("TwoBoostOff", RpcTarget.Others, boostLevel1, boostLevel2);
            //}

            DoTwoBoostOff();
        }
        // �ν��� ����� �϶� 1�� �ν��� ����Ʈ��
        if (booster.useBoost == true)
        {
            //photonView.RPC("OneBoostOn", RpcTarget.All, boostLevel1);

            //boostLevel1.SetActive(true);
            //if (photonView.IsMine)
            //{
            //    photonView.RPC("OneBoostOn", RpcTarget.Others, boostLevel1);
            //}

            DoOneBoostOn();
        }
        // ���� �ν��� ���� �϶� 1��,2�� �ν��� ����Ʈ��
        if (car.outOfControl == true)
        {
            //photonView.RPC("TwoBoostOn", RpcTarget.All, boostLevel1, boostLevel2);

            //boostLevel1.SetActive(true);
            //boostLevel2.SetActive(true);
            //if (photonView.IsMine)
            //{
            //    photonView.RPC("TwoBoostOn", RpcTarget.Others, boostLevel1, boostLevel2);
            //}

            DoTwoBoostOn();
        }
        // 2�� �ν��� ����� �϶� 1��, 2�� �ν��� ����Ʈ��
        if (car.useSecondBoost == true)
        {
            //photonView.RPC("TwoBoostOn", RpcTarget.All, boostLevel1, boostLevel2);

            //boostLevel1.SetActive(true);
            //boostLevel2.SetActive(true);
            //if (photonView.IsMine)
            //{
            //    photonView.RPC("TwoBoostOn", RpcTarget.Others, boostLevel1, boostLevel2);
            //}

            DoTwoBoostOn();
        }
    }

    [PunRPC]
    void OneBoostOn()
    {
        //boost1.SetActive(true);
        boostLevel1.SetActive(true);
    }

    [PunRPC]
    void TwoBoostOn()
    {
        //boost1.SetActive(true);
        //boost2.SetActive(true);
        boostLevel1.SetActive(true);
        boostLevel2.SetActive(true);
    }

    [PunRPC]
    void TwoBoostOff()
    {
        //boost1.SetActive(false); 
        //boost2.SetActive(false);
        boostLevel1.SetActive(false);
        boostLevel2.SetActive(false);
    }

    void DoOneBoostOn()
    {
        //boostLevel1.SetActive(true);
        if (photonView.IsMine)
        {
            photonView.RPC("OneBoostOn", RpcTarget.All);
        }
    }

    void DoTwoBoostOn()
    {
        //boostLevel1.SetActive(true);
        //boostLevel2.SetActive(true);
        if (photonView.IsMine)
        {
            photonView.RPC("TwoBoostOn", RpcTarget.All);
        }
    }

    void DoTwoBoostOff()
    {
        //photonView.RPC("TwoBoostOff", RpcTarget.All, boostLevel1, boostLevel2);

        //boostLevel1.SetActive(false);
        //boostLevel2.SetActive(false);
        if (photonView.IsMine)
        {
            photonView.RPC("TwoBoostOff", RpcTarget.All);
        }
    }
}
