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
            gameObject.GetComponent<TMP_Text>(); // index 0인 자식 텍스트를 가져옴
        highlightedColor = new Color(196f/255f, 171f/255f, 96f/255f); // hightlighted 일 때 변경할 색상
        defaultColor = Color.white; // 기본색상
    }

    // 버튼이 HightLighted일 때 색상 변경하는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightedColor;
    }

    // 버튼이 UnhightLighted일 때 색상 변경하는 함수
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
