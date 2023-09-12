using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobyBtnController_Choi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ButtonEffect")]
    private Button button;
    private TMP_Text buttonText;
    private Color highlightedColor;
    private Color defaultColor;
    public GameObject playButton;
    public GameObject MatchButton;

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
    public void MatchButtonActive()
    {
        playButton.SetActive(false);
        MatchButton.SetActive(true);
    }
    public void MatchBackButton()
    {
        playButton.SetActive(true);
        MatchButton.SetActive(false);
    }
}
