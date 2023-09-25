using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class BoostUI_Yoo : MonoBehaviourPun
{
    public float boost { get; private set; }

    public float newBoost { get; private set; }

    private float pastBoost;
    private int boostInt;
    private string boostString;
    private TMP_Text boostText;
    private Image boostImage;
    // Start is called before the first frame update
    void Start()
    {
        boostImage = transform.GetChild(1).GetComponent<Image>();
        boostText = transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>();
        boost = 33;
        boostImage.fillAmount = 0.65f * (boost / 100);
        boostInt = (int)boost;
        boostString = boostInt.ToString();
        boostText.text = boostString;
    }

    // Update is called once per frame
    void Update()
    {
        boostImage.fillAmount = 0.65f * (boost / 100);

        boostInt = (int)boost;
        boostString = boostInt.ToString();
        boostText.text = boostString;
        pastBoost = boost;
    }

    public void SetBoost(float nBoost)
    {
        boost = nBoost;
    }

    public void SetNewBoost(float nBoost)
    {
        newBoost = nBoost;
    }

    // 부스터 게이지값을 설정하는 함수
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
}
