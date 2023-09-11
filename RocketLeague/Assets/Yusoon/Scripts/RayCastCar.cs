using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCar : MonoBehaviour
{
    public Transform leftFrontTire;
    public Transform rightFrontTire;
    public Transform leftRearTire;
    public Transform rightRearTire;

    Rigidbody rigidBody;
    public float rayLength = 1.0f;
    public float suspensionHeight = 1f; // ���ϴ� ������� ����
    public float suspensionForce = 10.0f; // ������� ��
 
    public float suspensionSpring = 5000.0f;
    public float suspensionDamper = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //  // ���� �� Ÿ�̾�� Ray �߻�
        //  RaycastHit hitInfoLeftFront;
        //  Vector3 rayDirectionLeftFront = -leftFrontTire.up;
        //  if (Physics.Raycast(leftFrontTire.position, rayDirectionLeftFront, out hitInfoLeftFront, rayLength))
        //  {
        //      // Ray�� � ��ü�� �ε����� ���� ������ ���⿡ �߰��ϼ���.
        //      rigidBody.AddForce(leftFrontTire.up*5);
        //      // ����� ���̸� �׸��ϴ�.
        //      Debug.DrawRay(leftFrontTire.position, rayDirectionLeftFront * rayLength, Color.red);
        //  }
        //  else
        //  {
        //      // Ray�� �ε����� �ʾ��� ���� ������ ���⿡ �߰��ϼ���.

        //      // ����� ���̸� �׸��ϴ�.
        //      Debug.DrawRay(leftFrontTire.position, rayDirectionLeftFront * rayLength, Color.green);
        //  }

        //  Vector3 rayDirectionRightFront = -rightFrontTire.up;
        //// ������ �� Ÿ�̾�� Ray �߻�
        //RaycastHit hitInfoRightFront;
        //  if (Physics.Raycast(rightFrontTire.position, rayDirectionRightFront, out hitInfoRightFront, rayLength))
        //  {
        //      rigidBody.AddForce(rightFrontTire.up * 5);
        //      // Ray�� � ��ü�� �ε����� ���� ������ ���⿡ �߰��ϼ���.
        //      Debug.DrawRay(rightFrontTire.position, rayDirectionRightFront * rayLength, Color.red);
        //  }
        //  else
        //  {
        //      // Ray�� �ε����� �ʾ��� ���� ������ ���⿡ �߰��ϼ���.

        //      // ����� ���̸� �׸��ϴ�.
        //      Debug.DrawRay(rightFrontTire.position, rayDirectionRightFront * rayLength, Color.green);
        //  }

        //  // ���� �� Ÿ�̾�� Ray �߻�
        //  RaycastHit hitInfoLeftRear;

        //  Vector3 rayDirectionLeftRear = -leftRearTire.up;

        //  if (Physics.Raycast(leftRearTire.position, rayDirectionLeftRear, out hitInfoLeftRear, rayLength))
        //  {
        //      rigidBody.AddForce(leftRearTire.up*5);
        //      // Ray�� � ��ü�� �ε����� ���� ������ ���⿡ �߰��ϼ���.
        //      Debug.DrawRay(leftRearTire.position, rayDirectionLeftRear * rayLength, Color.red);

        //  }
        //  else
        //  {
        //      // Ray�� �ε����� �ʾ��� ���� ������ ���⿡ �߰��ϼ���.

        //      // ����� ���̸� �׸��ϴ�.
        //      Debug.DrawRay(leftRearTire.position, rayDirectionLeftRear * rayLength, Color.green);

        //  }


        //  // ������ �� Ÿ�̾�� Ray �߻�
        //  RaycastHit hitInfoRightRear;
        //  Vector3 rayDirectionRightRear = -rightRearTire.up;

        //  if (Physics.Raycast(rightRearTire.position, rayDirectionRightRear, out hitInfoRightRear, rayLength))
        //  {
        //      rigidBody.AddForce(rightRearTire.up * 5);
        //      // Ray�� � ��ü�� �ε����� ���� ������ ���⿡ �߰��ϼ���.
        //      Debug.DrawRay(rightRearTire.position, rayDirectionRightRear * rayLength, Color.red);
        //  }
        //  else
        //  {
        //      // Ray�� �ε����� �ʾ��� ���� ������ ���⿡ �߰��ϼ���.

        //      // ����� ���̸� �׸��ϴ�.
        //      Debug.DrawRay(rightRearTire.position, rayDirectionRightRear * rayLength, Color.green);


        //  }
        UpdateSuspension(leftFrontTire);
        UpdateSuspension(rightFrontTire);
        UpdateSuspension(leftRearTire);
        UpdateSuspension(rightRearTire);
        if(Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(transform.forward*50, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(-transform.forward*50, ForceMode.Acceleration);
        }
      
    }

    void UpdateSuspension(Transform tire)
    {
        RaycastHit hitInfo;
        Vector3 rayDirection = -tire.up;
        Vector3 suspensionForceDirection = tire.up;

        if (Physics.Raycast(tire.position, rayDirection, out hitInfo, rayLength))
        {
            // ���� ����� ��, Ÿ�̾ ������� ���̷� ����ø���.
            float suspensionCompression = rayLength - hitInfo.distance;
            Vector3 suspensionForceVector = suspensionForceDirection * suspensionCompression * suspensionForce;
            rigidBody.AddForceAtPosition(suspensionForceVector, tire.position);
        }
    }
}
