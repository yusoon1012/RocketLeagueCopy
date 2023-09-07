using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController_Choi : MonoBehaviour
{
    #region �����
    public GameObject[] objs; // �Ʒ��� ���� ������Ʈ�� �ε����� ����
                              // [0] = Img_TitleBg, [1] = Img_TitleCompanyLogo
    #endregion

    void Start()
    {
        // Ÿ��Ʋ ��� �׼� �Լ� ȣ��
        float[] actionTimesForTitleBg = {1f, 3f};
        StartCoroutine(DOActionTitleBg(actionTimesForTitleBg));

        // ȸ�� �ΰ� �׼� �Լ� ȣ��
        float[] actionTimesForCompanyLogo = {7f, 2f, 1f, 4f, 1f};
        StartCoroutine(DOActionCompanyLogo(actionTimesForCompanyLogo));
    }

    // Ÿ��Ʋ ��� �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionTitleBg(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[0].SetActive(true); // ��� Ȱ��ȭ

        yield return new WaitForSeconds(times[1]);
        Image titleBg = objs[0].GetComponent<Image>();
        titleBg.DOFade(0f, 1f).SetDelay(1f); // 1�ʰ� ���̵� �ƿ�
    }

    // ȸ�� �ΰ� �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionCompanyLogo(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[1].SetActive(true); // �ΰ� Ȱ��ȭ

        Image companyLogo = objs[1].GetComponent<Image>();
        companyLogo.DOColor(Color.white, 2f); // 2�ʰ� ���̵� �� / DOFade�� ���̵��ν� �����δ� �����ϳ�
                                              // ����� ������ �ʴ� �������� DOColor�� �����ϰ� ����
        yield return new WaitForSeconds(times[1]);
        Transform companyLogoTransform = objs[1].GetComponent<Transform>();
        companyLogoTransform.DOScale(new Vector3(1.05f, 1.05f, 1f), 3.0f); //3�ʰ� ������ x 1.05

        yield return new WaitForSeconds(times[2]);
        companyLogo.DOFade(0f, 1f).SetDelay(1f); // 1�ʰ� ���̵� �ƿ�

        yield return new WaitForSeconds(times[3]);
        // �� �ε�
    }
}
