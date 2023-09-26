using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NickNameManager : MonoBehaviour
{
    public TMP_Text nickNameText;
    public TMP_InputField nickNameInputField;
    public GameObject nickNameUi;
    string savedNickName;

    private void Awake()
    {
        savedNickName = PlayerPrefs.GetString("PlayerNickName");
        if (string.IsNullOrEmpty(savedNickName))
        {
            // ����� �г����� ������ �Է� �ʵ带 Ȱ��ȭ�մϴ�.
            nickNameUi.gameObject.SetActive(true);
        }
        else
        {
            // ����� �г����� ������ �ؽ�Ʈ�� ǥ���մϴ�.
            nickNameText.text = savedNickName;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SaveNickName();
            
        }

    }
    // �Է� �ʵ忡�� �г����� �����ϴ� �Լ�
    public void SaveNickName()
    {
        string inputNickName = nickNameInputField.text;
        // �Էµ� �г����� PlayerPrefs�� �����մϴ�.
        PlayerPrefs.SetString("PlayerNickName", inputNickName);

        // �Է� �ʵ带 ��Ȱ��ȭ�ϰ� �ؽ�Ʈ�� ǥ���մϴ�.
        nickNameInputField.gameObject.SetActive(false);
        nickNameText.text = inputNickName;
        nickNameUi.SetActive(false);
    }
}
