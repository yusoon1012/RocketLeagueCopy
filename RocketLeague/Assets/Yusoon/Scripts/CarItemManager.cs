using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Concurrent;
using Photon.Pun;
using JetBrains.Annotations;

public class CarItemManager : MonoBehaviourPun
{
    public GameObject magnetParticle;
    public GameObject PowerUpParticle;
    public Animator kickAnimator;
    public Animator punchAnimator;
    public Transform kickTransform;
    public Transform punchTransform;
    public GameObject myCollider;
    public GameObject skillSlider;
    public Image skillSliderbg;
    public TMP_Text cooltimeText;
    public float skillCoolTime;
    public Image[] skillIcon;
    private string targetTag;
    bool skillActive = false;
    public bool usedSkill = false;
    float radius = 50f;
    Vector3 newPosition;
    private Collider nearestCollider;
    private float coolTimeMax = 10f;
    Rigidbody ballRigidBody;
    Vector3 ballDir;
    LineRenderer lineRenderer;
    const int MAGNET = 0;
    const int KICK = 1;
    const int PUNCH = 2;
    const int ICE = 3;
    const int ENEMY_BOOST = 4;
    //const int POWER_UP = 5;


    float currentCooltime;
    int randomIdx;
    public bool magnetOn = false;
    bool isPunch = false;
    Vector3[] linePoints = new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {

        skillCoolTime = 0;
        StartCoroutine(SkillCoolTimeRoutine());
        if (myCollider.tag == "Car_Orange")
        {
            targetTag = "Car_Blue";

        }
        else
        {
            targetTag = "Car_Orange";

        }
        DeActiveIcons();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        linePoints[0] = transform.position;
        linePoints[0].y = 4;
        lineRenderer.SetPosition(0, linePoints[0]);
        if (magnetOn)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;

        }

        if (!photonView.IsMine)
        {
            return;
        }

        newPosition = transform.position;
        newPosition.y = 2.4f;
        Collider[] colliders = Physics.OverlapSphere(newPosition, radius);
        currentCooltime = coolTimeMax - skillCoolTime;
        cooltimeText.text = currentCooltime.ToString();
        skillSliderbg.fillAmount = skillCoolTime / coolTimeMax;


        if (skillCoolTime == 10)
        {
            skillSlider.SetActive(false);
            //if(usedSkill==true)
            //{
            //    usedSkill = false;
            //}
            skillActive = true;
        }
        else
        {
            skillActive = false;

        }
        //if (skillActive)
        //{
        //    if (randomIdx == POWER_UP)
        //    {
        //        if (Input.GetKeyDown(KeyCode.R))
        //        {
        //            PowerUpParticle.SetActive(true);
        //            StartCoroutine(SkillUseCoolTime(5));
        //        }
        //    }
        //}
        float nearestDistance = float.MaxValue;
        foreach (Collider col in colliders)
        {
            if (myCollider.tag == col.tag) continue;
            if (col.tag == targetTag)
            {
                if (col.gameObject.name == "Collider")
                {
                    //Debug.LogFormat("내 태그 {0} 상대 태그 {1}", myCollider.tag, col.tag);

                    float distance = Vector3.Distance(newPosition, col.gameObject.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestCollider = col;
                        nearestDistance = distance;
                    }
                    if (nearestCollider != null)
                    {
                        //Debug.LogFormat("내 태그 {0}, 가장 가까운 상대 태그 {1}", myCollider.tag, nearestCollider.tag);
                        Rigidbody rb = nearestCollider.gameObject.GetComponent<Rigidbody>();
                        Vector3 dir = (col.transform.position - transform.position).normalized;
                        if (rb != null)
                        {
                            //Debug.Log("rb 잘 찾음");
                            if (skillActive)
                            {

                                if (Input.GetKeyDown(KeyCode.R))
                                {

                                    if (usedSkill == false)
                                    {
                                      

                                        switch (randomIdx)

                                        {


                                            case KICK:
                                                usedSkill = true;
                                                //kickTransform.LookAt(col.transform.position);
                                                //kickAnimator.Play("KickAnimation");
                                                int viewId;
                                                PhotonView rbView = nearestCollider.gameObject.GetComponent<PhotonView>();
                                                viewId = rbView.ViewID;
                                                //Debug.Log(viewId);
                                                //rb.AddForce(dir * 30, ForceMode.VelocityChange);
                                                //StartCoroutine(SkillUseCoolTime(1));
                                                //Debug.Log("킥 실행함");
                                                photonView.RPC("Kick", RpcTarget.All, dir, col.gameObject.transform.position, viewId);
                                                break;
                                            case ENEMY_BOOST:
                                                usedSkill = true;
                                                CarParent colParent = col.gameObject.GetComponentInParent<CarParent>();
                                                if (colParent != null)
                                                {
                                                    NewCar newCar = colParent.gameObject.GetComponentInChildren<NewCar>();
                                                    if (newCar != null)
                                                    {
                                                        //newCar.outOfControl = true;
                                                        //StartCoroutine(SkillUseCoolTime(5));
                                                        int carViewId = newCar.gameObject.GetComponent<PhotonView>().ViewID;
                                                        photonView.RPC("EnemyBoost", RpcTarget.All, carViewId);
                                                    }
                                                }
                                                break;


                                        }

                                    }

                                }
                            }

                        }
                        // nearestCollider를 사용하여 필요한 작업을 수행할 수 있습니다.
                    }
                }
            }



        }

        photonView.RPC("receiveInfo", RpcTarget.Others, linePoints);


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballRigidBody = other.GetComponent<Rigidbody>();
            ballDir = (other.transform.position - transform.position).normalized;

            if (magnetOn)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    ballRigidBody.AddForce(-ballDir * 30);
                }
            }

            if (!photonView.IsMine)
            {
                linePoints[1] = other.transform.position;
                lineRenderer.SetPosition(1, linePoints[1]);
                return;
            }

            //이 아래부터는 로컬에서만 실행하는 부분
            linePoints[1] = other.transform.position;
            lineRenderer.SetPosition(1, linePoints[1]);

            if (skillActive)
            {
                if (usedSkill == false)
                {

                    if (randomIdx == PUNCH)
                    {
                        if (Input.GetKeyDown(KeyCode.R))
                        {

                            if (isPunch == false)
                            {
                                usedSkill = true;

                                //    isPunch=true;
                                //punchAnimator.Play("PunchAnimation");
                                //punchTransform.LookAt(other.transform.position);
                                //StartCoroutine(SkillUseCoolTime(1));
                                //ballRigidBody.velocity = Vector3.zero;
                                //ballRigidBody.AddForce(ballDir * 70, ForceMode.Impulse);

                                photonView.RPC("BallPunch", RpcTarget.All, other.transform.position);
                            }
                        }
                    }
                    if (randomIdx == MAGNET)
                    {

                        if (Input.GetKeyDown(KeyCode.R))
                        {
                        

                            if (magnetOn == false)
                            {

                                usedSkill = true;
                                //magnetOn = true;
                                //magnetParticle.SetActive(true);
                                //StartCoroutine(SkillUseCoolTime(8));

                                photonView.RPC("BallMargnet", RpcTarget.All);

                            }

                        }
                    }
                    if (randomIdx == ICE)
                    {
                        if (Input.GetKeyDown(KeyCode.R))
                        {
                            usedSkill = true;
                            int ballViewId = other.GetComponent<PhotonView>().ViewID;
                            if (ballViewId != default)
                            {
                                //ball.FreezeBall();
                                //StartCoroutine(SkillUseCoolTime(3));

                                photonView.RPC("BallIce", RpcTarget.All, ballViewId);

                            }
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            lineRenderer.enabled = false;
        }
    }

    private void DeActiveIcons()
    {
        for (int i = 0; i < skillIcon.Length; i++)
        {
            skillIcon[i].enabled = false;
        }
    }

    private IEnumerator SkillUseCoolTime(int second)
    {
        yield return new WaitForSeconds(second);
        DeActiveIcons();
        skillSlider.SetActive(true);
        skillCoolTime = 0;
        if (magnetParticle != null)
        {
            magnetParticle.SetActive(false);

        }
        if (PowerUpParticle != null)
        {
            PowerUpParticle.SetActive(false);

        }


        isPunch = false;
        magnetOn = false;

      
        StartCoroutine(SkillCoolTimeRoutine());
        yield break;
    }

    private IEnumerator SkillCoolTimeRoutine()
    {
        while (skillCoolTime < 10)
        {
            yield return new WaitForSeconds(1);
            skillCoolTime += 1;
        }

        if (!photonView.IsMine)
        {
            yield break;
        }

        randomIdx = Random.Range(0, 5);
        //randomIdx = 4;

        skillIcon[randomIdx].enabled = true;
        usedSkill = false;
        yield break;
    }

    [PunRPC]
    void receiveInfo(Vector3[] linePts)
    {
        linePoints = linePts;
    }

    [PunRPC]
    void BallPunch(Vector3 position)
    {
        isPunch = true;
        punchAnimator.Play("PunchAnimation");
        punchTransform.LookAt(position);
        StartCoroutine(SkillUseCoolTime(1));
        if (PhotonNetwork.IsMasterClient)
        {
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.AddForce(ballDir * 70, ForceMode.Impulse);
           

        }
        
    }

    [PunRPC]
    void BallMargnet()
    {
        magnetOn = true;
        magnetParticle.SetActive(true);
        StartCoroutine(SkillUseCoolTime(8));

    }

    [PunRPC]
    void BallIce(int ballViewId)
    {
        StartCoroutine(SkillUseCoolTime(3));
        Ball_Ys ball = PhotonView.Find(ballViewId).gameObject.GetComponent<Ball_Ys>();
        ball.FreezeBall();
        
    }

    [PunRPC]
    void Kick(Vector3 nearDir, Vector3 nearPosition, int viewId)
    {
        Vector3 newPosition = nearPosition;
        newPosition.y -= 3.04f;
        //Debug.Log("여기 들어옴?");
        kickTransform.LookAt(newPosition);
        kickAnimator.Play("KickAnimation");
        StartCoroutine(SkillUseCoolTime(1));


        PhotonView targetView = PhotonView.Find(viewId);
        //Debug.Log("여기 들어옴?");
        Rigidbody rb = targetView.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(nearDir * 150, ForceMode.VelocityChange);
    }


    [PunRPC]
    void EnemyBoost(int viewId)
    {
        StartCoroutine(SkillUseCoolTime(5));

        PhotonView targetView = PhotonView.Find(viewId);
        NewCar newCar = targetView.gameObject.GetComponent<NewCar>();
        newCar.outOfControl = true;
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(newPosition, radius);
    //}

}
