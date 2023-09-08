using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.velocity=Vector3.forward*20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
