using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoosterEffect_Yoo : MonoBehaviourPun
{
    NewCar car;
    CarBooster_Yoo booster;
    GameObject boostLevel1;
    GameObject boostLevel2;
    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<NewCar>();
        booster = GetComponent<CarBooster_Yoo>();
        boostLevel1 = transform.GetChild(0).GetChild(0).GetChild(6).gameObject;
        boostLevel2 = transform.GetChild(0).GetChild(0).GetChild(7).gameObject;
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
            photonView.RPC("TwoBoostOff", RpcTarget.AllBuffered, boostLevel1, boostLevel2);
            //boostLevel1.SetActive(false);
            //boostLevel2.SetActive(false);
        }
        // �ν��� ����� �϶� 1�� �ν��� ����Ʈ��
        if (booster.useBoost == true)
        {
            photonView.RPC("OneBoostOn", RpcTarget.AllBuffered, boostLevel1);
            //boostLevel1.SetActive(true);
        }
        // ���� �ν��� ���� �϶� 1��,2�� �ν��� ����Ʈ��
        if (car.outOfControl == true)
        {
            photonView.RPC("TwoBoostOn", RpcTarget.AllBuffered, boostLevel1, boostLevel2);
            //boostLevel1.SetActive(true);
            //boostLevel2.SetActive(true);
        }
        // 2�� �ν��� ����� �϶� 1��, 2�� �ν��� ����Ʈ��
        if (car.useSecondBoost == true)
        {
            photonView.RPC("TwoBoostOn", RpcTarget.AllBuffered, boostLevel1, boostLevel2);
            //boostLevel1.SetActive(true);
            //boostLevel2.SetActive(true);
        }
    }

    [PunRPC]
    void OneBoostOn(GameObject boost1)
    {
        boost1.SetActive(true);
    }

    [PunRPC]
    void TwoBoostOn(GameObject boost1, GameObject boost2)
    {
        boost1.SetActive(true);
        boost2.SetActive(true);
    }

    [PunRPC]
    void TwoBoostOff(GameObject boost1, GameObject boost2)
    {
        boost1.SetActive(false);
        boost2.SetActive(false);
    }
}
