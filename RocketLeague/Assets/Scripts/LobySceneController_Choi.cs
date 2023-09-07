using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobySceneController_Choi : MonoBehaviour
{
    #region �����
    public GameObject[] objs; // �Ʒ��� ���� ������Ʈ�� �ε����� ����
                              // [0] = Txt_StartMsg, [1] = Btn_Start, [2] = Img_GameLogo
    private bool isStart = false;
    #endregion

    void Start()
    {
        // ���� ��ư �׼� �Լ� ȣ��
        float[] actionTimesForStartMsg = {1f, 1f, 2f};
        StartCoroutine(DOActionStartMsg(actionTimesForStartMsg));

        // Ÿ��Ʋ ���� �ΰ� �׼� �Լ� ȣ��
        //float[] actionTimesForGameLogo = {1f, 3f};
        //StartCoroutine(DOActionGameLogo(actionTimesForGameLogo));
    }

    // ���� ��ư �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionStartMsg(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[0].SetActive(true); // ���� ��ư Ȱ��ȭ

        TMP_Text startMsg = objs[0].GetComponent<TMP_Text>();
        startMsg.DOFade(1f, 1f).SetDelay(2f); // 2�ʰ� ���� �ؽ�Ʈ ���̵���

        yield return new WaitForSeconds(times[1]);
        RectTransform startMsgRectTransform = startMsg.rectTransform;
        startMsgRectTransform.DOAnchorPos(new Vector3(0f, 330f, 0f), 2f); // 2�ʰ� ��Ŀ PosY 330f�� �̵�

        yield return new WaitForSeconds(times[2]);

        // �����ϱ� ������ �����ϰ� ���� �ؽ�Ʈ�� ������ ���� 
        Color randomColor = new Color(0f, 0f, 0f);

        // startMsg�� ���׸���� ���̴��� ������
        Material startMsgMaterial = startMsg.fontSharedMaterial;
        Shader startMsgShader = startMsgMaterial.shader;

        // Shader���� Glow�� ���̵� ã��
        int startMsgGlowId = Shader.PropertyToID("_GlowColor");

        float colorChangeTime = 3f;
        while (isStart == false)
        {
            randomColor = new Color(Random.value, 
                Random.value, Random.value); // Random.value�� ����Ͽ� 0�� 1������ ������ �Ҽ� ���� ����
            ChangeGlowColorForDOTween_Choi.DOColor(startMsgMaterial, startMsgGlowId,
                randomColor, colorChangeTime); //������ �ð� ���� glow�� ������ endColor�� ���� 
            yield return new WaitForSeconds(colorChangeTime); // ���� ������ ���� �� ���� ���
        }
    }

    // Ÿ��Ʋ ��� �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionGameLogo(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[0].SetActive(true); // Ÿ��Ʋ ���� �ΰ� Ȱ��ȭ

        yield return new WaitForSeconds(times[1]);
        Image titleBg = objs[0].GetComponent<Image>();
        titleBg.DOFade(0f, 1f).SetDelay(1f); // 1�ʰ� ���̵� �ƿ�
    }
}
