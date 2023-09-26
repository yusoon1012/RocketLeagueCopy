using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBooster_Yoo : MonoBehaviourPun
{
    private float newBoost;
    private float boost;
    private float pastBoost;

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
        boost = 33;
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

        pastBoost = boost;
        boostUI.SetBoost(boost);

        //Debug.Log("���� �ν��� ������: " +  boost);
        //boostUI.SetBoostGauge(boost);

        if (GameManager.instance.gameStartCheck == false || GameManager.instance.isGameOver == true) { return; }

        if (Input.GetMouseButton(0))
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
        else
        {
            newBoost = pastBoost + boostGauge;

            if(newBoost > 100)
            {
                newBoost = 100;
            }
        }

        SetBoostGauge(newBoost);
    }

    // �ν��� ������ ��� �Լ�
    public void UseBoost()
    {
        if(boost <= 0)
        {
            useBoost = false;
            boost = 0;
            return;
        }

        newBoost -= 0.666f;
        boost -= 0.666f;
    }

    public void SetBoostGauge(float gauge)
    {
        newBoost = gauge;
        //boostImage.fillAmount = Mathf.Lerp((pastBoost/100)*0.65f, (boost/100)*0.65f, t);
        StartCoroutine(BoostLerp());
    }

    IEnumerator BoostLerp()
    {
        float t;
        float delta = 0;
        float duration = 0.1f;

        while (duration > delta)
        {
            t = delta / duration;

            boost = Mathf.Lerp(pastBoost, newBoost, t);
            //boostImage.fillAmount = 0.65f * (boost / 100);

            //boostInt = (int)boost;
            //boostString = boostInt.ToString();
            //boostText.text = boostString;
            delta += Time.deltaTime;
            yield return null;
        }
        boost = newBoost;

        yield break;
    }

    public void SetBoost(float nBoost)
    {
        boost = nBoost;
    }

    public void SetNewBoost(float nBoost)
    {
        newBoost = nBoost;
    }
}
