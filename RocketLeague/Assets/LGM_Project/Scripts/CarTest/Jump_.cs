using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_ : MonoBehaviour
{
    public Transform kartNormal;

    private Rigidbody carRb;   // RC 카 리짓바디
    private Transform kartBody;   // 카트 트랜스폼
    private int jumpCount = default;
    private float zInput = default;   // 공중 회전 시 Z 축 입력값

    void Awake()
    {
           // 초기 변수 설정
        carRb = GetComponent<Rigidbody>();
        kartBody = GameObject.Find("Kart").transform;   // Kart 라는 오브젝트를 찾아 트랜스폼 값 저장

        jumpCount = 0;
        zInput = 0f;
           // end 초기 변수 설정
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

        if (Input.GetKey(KeyCode.W) && jumpCount > 0)   // W 키를 누르고 있으면 누른 만큼 차량의 Z 축 - 회전
        {
            ZInputDown();
        }

        if (Input.GetKey(KeyCode.S) && jumpCount > 0)   // S 키를 누르고 있으면 누른 만큼 차량의 Z 축 + 회전
        {
            ZInputUp();
        }

        RotationCar();   // RC 카 회전 값 최종 실행

        if (Input.GetKeyDown(KeyCode.R))   // test : R 키를 누르면 RC 카 회전이 초기값으로 변경
        {
            carRb.AddForce(kartNormal.up * 500f, ForceMode.Impulse);
            kartBody.transform.rotation = Quaternion.Euler(0f, kartBody.transform.rotation.y, 0f);
        }
    }

    public void ZInputUp()   // 키를 누르고 있으면 zInput 값이 1 씩 증가한다
    {
        zInput += 1f;
    }

    public void ZInputDown()   // 키를 누르고 있으면 zInput 값이 1 씩 감소한다
    {
        zInput -= 1f;
    }

    public void RotationCar()   // RC 카 회전 값 최종 실행
    {
        //kartBody.transform.rotation = Quaternion.Euler(kartBody.transform.rotation.x, kartBody.transform.rotation.y, zInput);
        kartBody.transform.rotation = Quaternion.Euler(0f, 0f, zInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            jumpCount = 0;
            /*zInput = 0f;*/   // "Floor" 태크의 바닥에 RC 카가 닿으면 zInput 값을 0 으로 초기화 시킨다
               // "Floor" 태그의 바닥에 RC 카가 닿으면 회전중이던 방향을 초기값으로 돌려준다
            //kartBody.transform.rotation = Quaternion.Euler(0f, kartBody.transform.rotation.y, 0f);
        }
    }
}
