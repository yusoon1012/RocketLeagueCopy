using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeActiveIFnotMine : MonoBehaviourPunCallbacks
{
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!photonView.IsMine)
        {
            gameObject.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameOver)
        {
            if(gameOver==false)
            {
                gameOver = true;
                StartCoroutine(CameraDeActive());
            }
        }    
    }

    private IEnumerator CameraDeActive()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

    }
}
