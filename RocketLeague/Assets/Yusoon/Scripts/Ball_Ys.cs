using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball_Ys : MonoBehaviourPunCallbacks,IPunObservable
{
    Rigidbody rb;
    Vector3 networkPosition;
    Quaternion networkRotation;
    Vector3 velocity;
    Vector3 angularVelocity;
    // Start is called before the first frame update
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FreezeBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        StartCoroutine(FreezeCoolDown());
    }
    private IEnumerator FreezeCoolDown()
    {
        yield return new WaitForSeconds(3);
        rb.useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
      
        rb.useGravity = true;        

        
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        //if (!photonView.IsMine)
        //{
        //    rb.position = Vector3.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
        //    rb.rotation = Quaternion.RotateTowards(rb.rotation, networkRotation, Time.fixedDeltaTime * 100.0f);
        //}
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 보낼 때 Rigidbody의 상태를 직렬화합니다.
            stream.SendNext(rb.velocity);
            stream.SendNext(rb.angularVelocity);
        }
        else
        {
            // 데이터를 받을 때 Rigidbody의 상태를 역직렬화합니다.
            rb.velocity = (Vector3)stream.ReceiveNext();
            rb.angularVelocity = (Vector3)stream.ReceiveNext();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    rb.useGravity = true;
    //    StopAllCoroutines();
    //}
}
