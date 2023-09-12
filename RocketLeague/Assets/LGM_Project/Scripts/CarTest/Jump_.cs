using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_ : MonoBehaviour
{
    public Transform kartNormal;

    private Rigidbody carRb;   // RC ī �����ٵ�
    private Transform kartBody;   // īƮ Ʈ������
    private int jumpCount = default;
    private float zInput = default;   // ���� ȸ�� �� Z �� �Է°�

    void Awake()
    {
           // �ʱ� ���� ����
        carRb = GetComponent<Rigidbody>();
        kartBody = GameObject.Find("Kart").transform;   // Kart ��� ������Ʈ�� ã�� Ʈ������ �� ����

        jumpCount = 0;
        zInput = 0f;
           // end �ʱ� ���� ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount += 1;
            if (jumpCount <= 2)
            {
                carRb.AddForce(kartNormal.up * 2000f, ForceMode.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.W) && jumpCount > 0)   // W Ű�� ������ ������ ���� ��ŭ ������ Z �� - ȸ��
        {
            ZInputDown();
        }

        if (Input.GetKey(KeyCode.S) && jumpCount > 0)   // S Ű�� ������ ������ ���� ��ŭ ������ Z �� + ȸ��
        {
            ZInputUp();
        }

        RotationCar();   // RC ī ȸ�� �� ���� ����

        if (Input.GetKeyDown(KeyCode.R))   // test : R Ű�� ������ RC ī ȸ���� �ʱⰪ���� ����
        {
            carRb.AddForce(kartNormal.up * 500f, ForceMode.Impulse);
            kartBody.transform.rotation = Quaternion.Euler(0f, kartBody.transform.rotation.y, 0f);
        }
    }

    public void ZInputUp()   // Ű�� ������ ������ zInput ���� 1 �� �����Ѵ�
    {
        zInput += 1f;
    }

    public void ZInputDown()   // Ű�� ������ ������ zInput ���� 1 �� �����Ѵ�
    {
        zInput -= 1f;
    }

    public void RotationCar()   // RC ī ȸ�� �� ���� ����
    {
        //kartBody.transform.rotation = Quaternion.Euler(kartBody.transform.rotation.x, kartBody.transform.rotation.y, zInput);
        kartBody.transform.rotation = Quaternion.Euler(0f, 0f, zInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            jumpCount = 0;
            /*zInput = 0f;*/   // "Floor" ��ũ�� �ٴڿ� RC ī�� ������ zInput ���� 0 ���� �ʱ�ȭ ��Ų��
               // "Floor" �±��� �ٴڿ� RC ī�� ������ ȸ�����̴� ������ �ʱⰪ���� �����ش�
            //kartBody.transform.rotation = Quaternion.Euler(0f, kartBody.transform.rotation.y, 0f);
        }
    }
}
