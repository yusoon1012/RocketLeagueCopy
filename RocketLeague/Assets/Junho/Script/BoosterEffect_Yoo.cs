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
            Debug.Log("서버와 연결되지 않음");
            return;
        }

        // 부스터 사용중 아니고, 2단 부스터 사용중 아니고, 강제 부스팅 상태가 아니면 이펙트 끔
        if (booster.useBoost == false && car.useSecondBoost == false && car.outOfControl == false)
        {
            photonView.RPC("TwoBoostOff", RpcTarget.AllBuffered, boostLevel1, boostLevel2);
            //boostLevel1.SetActive(false);
            //boostLevel2.SetActive(false);
        }
        // 부스터 사용중 일때 1단 부스터 이펙트켬
        if (booster.useBoost == true)
        {
            photonView.RPC("OneBoostOn", RpcTarget.AllBuffered, boostLevel1);
            //boostLevel1.SetActive(true);
        }
        // 강제 부스팅 상태 일때 1단,2단 부스터 이펙트켬
        if (car.outOfControl == true)
        {
            photonView.RPC("TwoBoostOn", RpcTarget.AllBuffered, boostLevel1, boostLevel2);
            //boostLevel1.SetActive(true);
            //boostLevel2.SetActive(true);
        }
        // 2단 부스터 사용중 일때 1단, 2단 부스터 이펙트켬
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
