using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }
            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    public static GameManager m_instance;   // 싱글톤이 할당될 static 변수

    public GameObject ballPrefab;   // 축구공을 생성할 축구공 프리팹
    public GameObject ballAuraPf;   // 축구공 표식을 생성할 축구공 표식 프리팹
    public GameObject blueCar;   // 블루팀 RC 카 생성할 프리팹
    public GameObject orangeCar;   // 오렌지팀 RC 카 생성할 프리팹
    public Transform ballSpawnTransform;   // 축구공을 생성할 구역
    public Transform[] blueCarSpawner;   // 블루팀 RC 카를 생성할 구역
    public Transform[] orangeCarSpawner;   // 오렌지팀 RC 카를 생성할 구역
    public TMP_Text blueScoreText;   // 블루팀 스코어 텍스트
    public TMP_Text orangeScoreText;   // 오렌지팀 스코어 텍스트
    public TMP_Text currentTimerText;

    public int blueScore;   // 블루팀 골 스코어
    public int orangeScore;   // 오렌지팀 골 스코어
    public bool isGoaled = false;   // 현재 골 성공 상태인지 체크
    public bool timePassCheck = true;

    private GameObject ballOj;   // 축구공 오브젝트
    private Rigidbody ballRb;   // 축구공 리짓바디
    private Transform blueSpawnPoint;   // 플레이어가 RC 카를 생성할 블루팀 포인트 구역
    private Transform orangeSpawnPoint;   // 플레이어가 RC 카를 생성할 오렌지팀 포인트 구역
    private int playerCount = default;   // 룸에 참여중인 플레이어 수
    private int totalTime = default;   // 남은 게임 총 시간
    private int minuteTime = default;   // 표시할 분 시간
    private int secondTime = default;   // 표시할 초 시간
    private float checkTime = default;   // 1 초를 체크할 실수값

    void Awake()
    {
           // 초기 변수값 설정
        playerCount = PhotonNetwork.PlayerList.Length;   // 포톤 서버에 접속한 플레이어 수만큼 플레이어 수로 지정해준다

        totalTime = 300;
        minuteTime = 0;
        secondTime = 0;
        checkTime = 0f;
           // end 초기 변수값 설정
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)   // 마스터 클라이언트만 실행한다
        {
            // 마스터 클라이언트 일시 축구공 프리팹을 불러와 축구공 오브젝트를 생성한다
            ballOj = PhotonNetwork.Instantiate(ballPrefab.name, ballSpawnTransform.position, Quaternion.identity);
            // 축구공 표식 오브젝트도 프리팹을 불러와 오브젝트를 생성한다
            PhotonNetwork.Instantiate(ballAuraPf.name, new Vector3(0f, 1.6f, 0f), Quaternion.Euler(90f, 0f, 0f));

            ballRb = ballOj.GetComponent<Rigidbody>();   // 축구공 리짓바디를 변수로 저장한다
        }

        if (playerCount % 2 == 0)
        {
            if (playerCount == 2)
            {
                blueSpawnPoint = blueCarSpawner[0];
            }
            else if (playerCount == 4)
            {
                blueSpawnPoint = blueCarSpawner[1];
            }
            else if (playerCount == 6)
            {
                blueSpawnPoint = blueCarSpawner[2];
            }

            PhotonNetwork.Instantiate(blueCar.name, blueSpawnPoint.position, blueSpawnPoint.rotation);
        }
        else
        {
            if (playerCount == 1)
            {
                orangeSpawnPoint = orangeCarSpawner[0];
            }
            else if (playerCount == 3)
            {
                orangeSpawnPoint = orangeCarSpawner[1];
            }
            else if (playerCount == 5)
            {
                orangeSpawnPoint = orangeCarSpawner[2];
            }

            PhotonNetwork.Instantiate(orangeCar.name, orangeSpawnPoint.position, orangeSpawnPoint.rotation);
        }

        playerCount = PhotonNetwork.PlayerList.Length;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timePassCheck == true)
            {
                photonView.RPC("TimePassMaster", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    public void TimePassMaster()
    {
        checkTime += Time.deltaTime;
        if (checkTime >= 1f)
        {
            checkTime = 0f;
            totalTime -= 1;
            minuteTime = totalTime / 60;
            secondTime = totalTime % 60;

            photonView.RPC("ApplyTimePass", RpcTarget.AllBuffered, minuteTime, secondTime);
        }
    }

    [PunRPC]
    public void ApplyTimePass(int min, int sec)
    {
        minuteTime = min;
        secondTime = sec;

        currentTimerText.text = string.Format("{0} : {1:00}", minuteTime, secondTime);
    }

    public void GameTimePassOn()   // 다시 게임 시간이 흘러가게 하는 함수
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("GameTimePassOnMaster", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    public void GameTimePassOnMaster()   // 마스터 클라이언트만 true 값으로 변경해준다
    {
        timePassCheck = true;
        photonView.RPC("ApplyGameTimePassOn", RpcTarget.AllBuffered, true);
    }

    [PunRPC]
    public void ApplyGameTimePassOn(bool state)   // bool 값을 참조하여 모든 클라이언트의 값을 변경해준다
    {
        timePassCheck = state;
    }

    public void ResetGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            photonView.RPC("ResetGameMaster", RpcTarget.MasterClient);

            //* fix : goalEffect *//
        }
    }

    [PunRPC]
    public void ResetGameMaster()
    {
        ballOj.SetActive(false);
        timePassCheck = false;
        photonView.RPC("ApplyResetGame", RpcTarget.AllBuffered, false);
        photonView.RPC("ApplyTimePassOff", RpcTarget.AllBuffered, false);

        StartCoroutine(ResetGameDelay());
    }

    [PunRPC]
    public void ApplyResetGame(bool state)
    {
        ballOj.SetActive(state);
    }

    [PunRPC]
    public void ApplyTimePassOff(bool state)
    {
        timePassCheck = state;
    }

    IEnumerator ResetGameDelay()
    {
        yield return new WaitForSeconds(3f);

        if (PhotonNetwork.IsMasterClient)
        {
            ballOj.transform.position = ballSpawnTransform.position;
            photonView.RPC("RespawnBall", RpcTarget.MasterClient);
        }
    }

    

    [PunRPC]
    public void RespawnBall()
    {
        ballOj.SetActive(true);
        isGoaled = false;
        photonView.RPC("ApplyResetGame", RpcTarget.AllBuffered, true);
        photonView.RPC("UpdateGoalCheck", RpcTarget.AllBuffered, false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //* Empty *//
    }

    public void BlueScoreUp()   // 블루팀이 골 성공 시 실행
    {
        if (PhotonNetwork.IsMasterClient)   // 마스터 클라이언트인지 체크
        {
            photonView.RPC("AddBlueScore", RpcTarget.MasterClient);   // 마스터 클라이언트에서 스코어 값이 증가하도록 해줌
        }
    }

    public void OrangeScoreUp()   // 오렌지팀이 골 성공 시 실행
    {
        if (PhotonNetwork.IsMasterClient)   // 마스터 클라이언트인지 체크
        {
            photonView.RPC("AddOrangeScore", RpcTarget.MasterClient);   // 마스터 클라이언트에서 스코어 값이 증가하도록 해줌
        }
    }

    [PunRPC]
    public void AddBlueScore()   // 블루팀 스코어를 1 증가시키는 함수
    {
        blueScore += 1;   // 블루팀 스코어 1 증가
                          // 스코어 값을 모든 클라이언트와 동기화 시켜줌
        photonView.RPC("UpdateScore", RpcTarget.AllBuffered, blueScore, orangeScore);
    }

    [PunRPC]
    public void AddOrangeScore()   // 오렌지팀 스코어를 1 증가시키는 함수
    {
        orangeScore += 1;   // 오렌지팀 스코어 1 증가
                            // 스코어 값을 모든 클라이언트와 동기화 시켜줌
        photonView.RPC("UpdateScore", RpcTarget.AllBuffered, blueScore, orangeScore);
    }

    [PunRPC]
    public void UpdateScore(int _blueScore, int _orangeScore)   // 스코어값을 모든 클라이언트가 동기화하는 함수
    {
        blueScore = _blueScore;   // 블루팀 스코어값을 동기화
        orangeScore = _orangeScore;   // 오렌지팀 스코어값을 동기화

        blueScoreText.text = string.Format("{0}", blueScore);   // 블루팀 스코어 텍스트 값을 변경해준다
        orangeScoreText.text = string.Format("{0}", orangeScore);   // 오렌지팀 스코어 텍스트 값을 변경해준다
    }

    public void GoalCheck()   // 골인 상태 체크값을 변경해줄때 실행하는 함수
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ChangeCheck", RpcTarget.MasterClient);   // 마스터 클라이언트가 isGoaled bool 변수값을 변경하도록 함
        }
    }

    [PunRPC]
    public void ChangeCheck()   // 마스터 클라이언트가 isGoaled 값을 변경하도록 함
    {
        isGoaled = true;   // isGoaled 값을 true 값으로 변경
                           // 모든 클라이언트가 isGoaled 값을 동기화 하도록 실행
        photonView.RPC("UpdateGoalCheck", RpcTarget.AllBuffered, true);
    }

    [PunRPC]
    public void UpdateGoalCheck(bool state)   // 모든 클라이언트가 isGoaled 값을 동기화 하는 함수
    {
        isGoaled = state;   // isGoaled 값을 모든 클라이언트가 동기화 함
        Debug.Log(state);
    }

    // 주기적으로 자동 실행되는, 동기화 메서드
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    // 로컬 오브젝트라면 쓰기 부분이 실행됨
    //    if (stream.IsWriting)
    //    {
    //        // 네트워크를 통해 score 값을 보내기
    //        stream.SendNext(teamCount[0]);
    //        stream.SendNext(teamCount[1]);
    //    }
    //    else
    //    {
    //        // 리모트 오브젝트라면 읽기 부분이 실행됨

    //        // 네트워크를 통해 score 값 받기
    //        teamCount[0] = (int)stream.ReceiveNext();
    //        teamCount[1] = (int)stream.ReceiveNext();
    //        // 동기화하여 받은 점수를 UI로 표시

    //        teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // 증가시킨 값을 출력
    //        teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);

    //        AfterSerializeView();
    //    }
    //}
}
