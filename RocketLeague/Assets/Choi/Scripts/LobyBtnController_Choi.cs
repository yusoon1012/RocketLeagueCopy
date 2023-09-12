using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobyBtnController_Choi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ButtonEffect")]
    private Button button;
    private TMP_Text buttonText;
    private Color highlightedColor;
    private Color defaultColor;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).
            gameObject.GetComponent<TMP_Text>(); // index 0�� �ڽ� �ؽ�Ʈ�� ������
        highlightedColor = new Color(196f/255f, 171f/255f, 96f/255f); // hightlighted �� �� ������ ����
        defaultColor = Color.white; // �⺻����
    }

    // ��ư�� HightLighted�� �� ���� �����ϴ� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightedColor;
    }

    // ��ư�� UnhightLighted�� �� ���� �����ϴ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = defaultColor;
    }
}
