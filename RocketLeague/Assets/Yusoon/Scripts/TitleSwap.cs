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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!introSkip)
            {
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

    }
}
