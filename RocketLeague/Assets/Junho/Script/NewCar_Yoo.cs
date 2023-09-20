using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class NewCar_Yoo : MonoBehaviourPun
{
    public Transform kartNormal;
    public Transform kartModel;
    public float acceleration;
    public float steering;
    public float gravity;
    public LayerMask layerMask;
    public LayerMask fieldMask;

    //int jumpCount = 0;
    bool drifting = false;
    float speed;
    public float currentSpeed;
    float rotate;
    float currentRotate;
    public Rigidbody sphere;

    // �ν��� ���� �߰�
    #region
    private CarBooster_Yoo booster;
    private float normalAcceleration;
    private float timeAfterFirstBoost;
    private float useSecondBoostDelay = 0.75f;
    public bool useSecondBoost { get; private set; }
    #endregion

    // ������ ���� �߰�
    #region
    public bool compulsionBoost;
    public bool powerUp;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //if(photonView.IsMine)
        //{
        //    // �ν��� ���� �߰�
        //    #region
        //    booster = GetComponent<CarBooster_Yoo>();
        //    normalAcceleration = acceleration;
        //    useSecondBoost = false;
        //    #endregion
        //}
        // �ν��� ���� �߰�

        #region
        booster = GetComponent<CarBooster_Yoo>();
        normalAcceleration = acceleration;
        useSecondBoost = false;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //// ���� ���� �߰�
        //if (!photonView.IsMine)
        //{
        //    return;
        //}

        Vector3 newposition = new Vector3(sphere.transform.position.x, sphere.transform.position.y - 3.5f, sphere.transform.position.z);
        transform.position = newposition;
        float speedDir = Input.GetAxis("Vertical");

        // �ν��� ���� �߰�
        #region
        if (booster.useBoost == true)
        {
            speedDir = 1;
            if (useSecondBoost == false)
            {
                if (acceleration >= normalAcceleration * 2.25f)
                {
                    timeAfterFirstBoost += Time.deltaTime;
                    //if(timeAfterFirstBoost <= useSecondBoostDelay)
                    //{
                    //    Debug.Log("2�� �ν��ͱ��� ���� �ð�" + (useSecondBoostDelay - timeAfterFirstBoost));
                    //}
                }

                if (timeAfterFirstBoost >= useSecondBoostDelay)
                {
                    useSecondBoost = true;
                    //Debug.Log("2�� �ν��� ���");
                }
            }
        }

        if (booster.useBoost == false)
        { 
            timeAfterFirstBoost = 0;
            useSecondBoost = false;
        }
        #endregion

        // ������ ���� �߰�
        #region
        if (compulsionBoost == true)
        {
            speedDir = 1;
        }

        if (compulsionBoost == false && booster.useBoost == false)
        {
            acceleration = normalAcceleration;
        }
        #endregion

        speed = speedDir * acceleration;

        if (Input.GetAxis("Horizontal") != 0)
        {

            float dir = Input.GetAxis("Horizontal");
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            if (speedDir != -1)
            {
                Steer(dir, amount);

            }
            else
            {
                Steer(-dir, amount);

            }

        }
        Vector3 rayStartPoint;
        if (Input.GetAxis("Vertical") > 0)
        {
            rayStartPoint = kartNormal.position + (kartNormal.up * .3f) + (kartNormal.right * 2f);
        }
        else
        {
            rayStartPoint = kartNormal.position + (kartNormal.up * .3f) + (-kartNormal.right * 2f);


        }


        Vector3 rayDirection = -kartNormal.up; // Ray�� �Ʒ��� ��� �������� ����

        RaycastHit hitOn;
        RaycastHit hitNear;

        Vector3 newGravityDirection = -kartNormal.up; // ������ ���ĵ� ������ �߷� �������� ���
        //Physics.gravity = newGravityDirection * gravity;
        Physics.Raycast(rayStartPoint, rayDirection, out hitOn, 4f, layerMask);
        Physics.Raycast(rayStartPoint, rayDirection, out hitNear, 4f, layerMask);

        // Visualize the raycast
        Debug.DrawRay(rayStartPoint, rayDirection * 4f, Color.green); // Ray�� �ð�ȭ�մϴ�.

        // Calculate the rotation to align kartNormal with the ground normal
        Vector3 targetNormal = hitNear.normal;
        Quaternion targetRotation = Quaternion.FromToRotation(kartNormal.up, targetNormal);

        // Smoothly adjust the kartNormal's rotation
        kartNormal.rotation = Quaternion.Slerp(kartNormal.rotation, targetRotation * kartNormal.rotation, Time.deltaTime * 8.0f);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);


        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;

        if (!drifting)
        {


            kartModel.localEulerAngles = Vector3.Lerp(kartModel.localEulerAngles, new Vector3(0, 90 + (Input.GetAxis("Horizontal") * 15), kartModel.localEulerAngles.z), .2f);


        }
    }
    private void FixedUpdate()
    {
        //// ���� ���� �߰�
        //if(!photonView.IsMine)
        //{
        //    return;
        //}

        sphere.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);

        //sphere.AddForce(-kartNormal.transform.up * gravity, ForceMode.Acceleration);

        // �ν��� ���� �߰�
        #region
        if (booster.useBoost == true && useSecondBoost == false)
        {
            acceleration *= 1.1f;
            if (acceleration >= normalAcceleration * 2.25f)
            {
                acceleration = normalAcceleration * 2.25f;
            }
        }

        if (useSecondBoost == true)
        {
            acceleration = normalAcceleration * 3.5f;
        }
        #endregion

        // ������ ���� �߰�
        #region
        if (compulsionBoost == true)
        {
            acceleration = normalAcceleration * 3.5f;
        }
        #endregion

        //RaycastHit hitOn;
        //RaycastHit hitNear;

        //Physics.Raycast(transform.position + (transform.up*.1f), Vector3.down, out hitOn, 3f, layerMask);
        //Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 3f, layerMask);

        ////Normal Rotation
        //kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        //kartNormal.Rotate(0, transform.eulerAngles.y, 0);


        //Vector3 rayDirection = -kartNormal.up; // Ray�� �Ʒ��� ��� �������� ����

        //RaycastHit hitOn;
        //RaycastHit hitNear;

        //Physics.Raycast(kartNormal.position + (kartNormal.up * .3f), rayDirection, out hitOn, 3f, layerMask);
        //Physics.Raycast(kartNormal.position + (kartNormal.up * .3f), rayDirection, out hitNear, 3f, layerMask);

        //// Visualize the raycast
        //Debug.DrawRay(kartNormal.position + (kartNormal.up * .1f), rayDirection * 3f, Color.green); // Visualize the ray

        //// Calculate the rotation to align kartNormal with the ground normal
        //Vector3 targetNormal = hitNear.normal;
        //Quaternion targetRotation = Quaternion.FromToRotation(kartNormal.up, targetNormal);

        //// Smoothly adjust the kartNormal's rotation
        //kartNormal.rotation = Quaternion.Slerp(kartNormal.rotation, targetRotation * kartNormal.rotation, Time.deltaTime * 8.0f);



    }
    public void Steer(float direction, float amount)
    {
        rotate += (steering * direction) * amount;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("field"))
        {
            //jumpCount = 0;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Vector3 wallNormal = collision.contacts[0].normal;
        transform.position += wallNormal * currentSpeed * Time.deltaTime; // moveSpeed�� ������ �ӵ��Դϴ�.
        transform.rotation = Quaternion.FromToRotation(transform.up, wallNormal) * transform.rotation;
    }

}
