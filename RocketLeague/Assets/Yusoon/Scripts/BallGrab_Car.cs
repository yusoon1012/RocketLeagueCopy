using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrab_Car : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball_Ys ball = other.GetComponent<Ball_Ys>();
            if (ball!=null)
            {
                Renderer renderer = ball.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color= Color.blue;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Rigidbody rb_ = ball.GetComponent<Rigidbody>();
                    Vector3 dir = (other.transform.position-transform.position).normalized;
                    Debug.Log("BallGrab_Carµø¿€¡ﬂ");
                    dir.y=dir.y-0.1f;
                    //if (rb_!= null)
                    //{ rb_.AddForce(dir*50); }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball_Ys ball = other.GetComponent<Ball_Ys>();
            if (ball!=null)
            {
                Renderer renderer = ball.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color= Color.white;
                }
            }
        }
    }
}
