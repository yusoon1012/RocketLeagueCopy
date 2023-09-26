using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSwap : MonoBehaviour
{
    public GameObject vcam1;
    public GameObject vcam2;
    public Canvas titleCanvas;
    public GameObject mainCanvas;
    bool introSkip=false;
    // Start is called before the first frame update
    void Start()
    {
        // isSkip�� true�� ���
        if (SceneData_Choi.isSkip == true)
        {
            // ��ŵ
            introSkip = true;
            vcam1.SetActive(true);
            vcam2.SetActive(false);
            titleCanvas.enabled = false;
            mainCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(!introSkip)
            {
             //PlayerPrefs.DeleteAll();


           introSkip = true;   
            vcam1.SetActive(true);
            vcam2.SetActive(false);
            titleCanvas.enabled=false;
            StartCoroutine(SwapRoutine());
            }
        }


    }
    private IEnumerator SwapRoutine()
    {
        yield return new WaitForSeconds(2);
        mainCanvas.SetActive(true);
        // Ÿ��Ʋ ��ŵ�� ���� SceneData.isSkip ���� true�� ����
        SceneData_Choi.isSkip = true;
    }
}
