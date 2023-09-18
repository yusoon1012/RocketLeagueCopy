using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviourPunCallbacks,IPunObservable
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

    private static GameManager m_instance;   // 싱글톤이 할당될 static 변수
    public GameObject ballPrefab;
    public GameObject blueCar;
    public GameObject orangeCar;
    public Transform ballSpawnTransform;
    public Transform[] blueCarSpawner;
    public Transform[] orangeCarSpawner;
    public int blueSpawnCheck = default;
    public int orangeSpawnCheck = default;
    public int gameMaxPlayers = default;
    public int playerTeamCheck = default;
    public TMP_Text blueScoreText;
    public TMP_Text orangeScoreText;
    public TMP_Text currentTimerText;

    public int blueScore;
    public int orangeScore;


    int playerCount;
 
    Transform blueSpawnPoint;
    Transform orangeSpawnPoint;
    void Awake()
    {

        playerCount=PhotonNetwork.PlayerList.Length;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(ballPrefab.name, ballSpawnTransform.position, Quaternion.identity);
        }
        if (playerCount%2==0)
        {
            
            if (playerCount==2)
            {
                blueSpawnPoint = blueCarSpawner[0];

            }
            else if (playerCount==4)
            {
                blueSpawnPoint = blueCarSpawner[1];

            }
            else if (playerCount==6)
            {
                blueSpawnPoint = blueCarSpawner[2];

            }
            PhotonNetwork.Instantiate(blueCar.name, blueSpawnPoint.position, blueSpawnPoint.rotation);



        }
        else
        {
           
            if (playerCount==1)
            {
                orangeSpawnPoint= orangeCarSpawner[0];

            }
            else if (playerCount==3)
            {
                orangeSpawnPoint= orangeCarSpawner[1];
            }
            else if (playerCount==5)
            {
                orangeSpawnPoint= orangeCarSpawner[2];

            }

            PhotonNetwork.Instantiate(orangeCar.name, orangeSpawnPoint.position, orangeSpawnPoint.rotation);





        }

        playerCount=PhotonNetwork.PlayerList.Length;
        
    }

    void Start()
    {
        
    }

   
    private void Update()
    {
        blueScoreText.text=blueScore.ToString();
        orangeScoreText.text= orangeScore.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      
    }

    [PunRPC]
    public void AddBlueScore()
    {
        blueScore+=1;
    }

    [PunRPC]
    public void AddOrangeScore()
    {
        orangeScore+=1;
    }

    public void OrangeScoreUp()
    {
        photonView.RPC("AddOrangeScore", RpcTarget.AllBuffered);
    }

    public void BlueScoreUp()
    {
        photonView.RPC("AddBlueScore", RpcTarget.AllBuffered);

    }


  
    public void BallRespawn()
    {
        photonView.RPC("BallSpawn", RpcTarget.MasterClient);

    }
    [PunRPC]
    private void BallSpawn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(ballPrefab.name, ballSpawnTransform.position, Quaternion.identity);
        }
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
