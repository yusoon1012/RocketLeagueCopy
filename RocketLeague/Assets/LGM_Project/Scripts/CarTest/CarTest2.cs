using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest2 : MonoBehaviour
{
    private Rigidbody carRb;

    private float isWallGravity = default;
    private bool isWalled = false;
    
    void Awake()
    {
        carRb = GetComponent<Rigidbody>();

        isWallGravity = 100f;
    }

    void Update()
    {
        //if (isWalled)
        //{
        //    carRb.AddForce(Vector3.up * isWallGravity);
        //}
    }

    public void InWall()
    {
        isWalled = true;
        carRb.useGravity = false;
        Debug.Log("���� �پ���");
    }

    public void OutWall()
    {
        isWalled = false;
        carRb.useGravity = true;
        Debug.Log("������ ��������");
    }
}
