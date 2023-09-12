using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobySceneController_Choi : MonoBehaviour
{
    [Header("LobyEffect")]
    public GameObject[] objs; // �Ʒ��� ���� ������Ʈ�� �ε����� ����
                              // [0] = Txt_StartMsg, [1] = Btn_Start, [2] = Img_GameLogo, [3] = Img_BlackBg
    private bool isStart = false;

    void Start()
    {
        // ���� ��ư �׼� �Լ� ȣ��
        float[] actionTimesForStartMsg = {1f, 1f, 2f};
        StartCoroutine(DOActionStartMsg(actionTimesForStartMsg));

        // Ÿ��Ʋ ���� �ΰ� �׼� �Լ� ȣ��
        float[] actionTimesForGameLogo = {4f};
        StartCoroutine(DOActionGameLogo(actionTimesForGameLogo));
    }

    // ���� ��ư �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionStartMsg(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[1].SetActive(true); // ���� ��ư Ȱ��ȭ

        Image blackBg = objs[3].GetComponent<Image>();
        blackBg.DOFade(0f, 1f).SetDelay(2f); // 2�ʰ� ���� ��� ���̵� �ƿ�

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

    // ���� �ΰ� �׼� �ڷ�ƾ �Լ�
    private IEnumerator DOActionGameLogo(float[] times)
    {
        yield return new WaitForSeconds(times[0]);
        objs[2].SetActive(true); // ���� �ΰ� Ȱ��ȭ

        Image gameLogo = objs[2].GetComponent<Image>();
        gameLogo.DOFade(1f, 1f).SetDelay(1f); // 1�ʰ� ���̵� ��

        RectTransform gameLogoRectTramsform = gameLogo.GetComponent<RectTransform>();
        gameLogoRectTramsform.DOAnchorPos(new Vector3(0f, -380f, 0f), 2f); // 2�ʰ� �Ʒ��� �̵�
    }
}
