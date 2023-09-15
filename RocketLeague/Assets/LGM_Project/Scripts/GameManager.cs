using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks /*IPunObservable*/
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

    public GameObject blueCar;
    public GameObject orangeCar;
    public Transform[] blueCarSpawner;
    public Transform[] orangeCarSpawner;
    public int blueSpawnCheck = default;
    public int orangeSpawnCheck = default;
    public int gameMaxPlayers = default;
    public int playerTeamCheck = default;

    private GameObject gameValue_;

    void Awake()
    {
        if (instance != this) { Destroy(gameObject); }

        gameValue_ = GameObject.Find("GameValue");
    }

    void Start()
    {
        gameMaxPlayers = gameValue_.GetComponent<GameValue>().gameMaxPlayer;
        playerTeamCheck = gameValue_.GetComponent<GameValue>().playerTeamCheck;

        CarSpawn();
    }

    public void CarSpawn()
    {
        if (gameValue_.GetComponent<GameValue>().playerTeamCheck == 1)
        {
            PhotonNetwork.Instantiate(blueCar.name, blueCarSpawner[0].position, blueCarSpawner[0].rotation, 0);
        }
        else if (gameValue_.GetComponent<GameValue>().playerTeamCheck == 2)
        {
            PhotonNetwork.Instantiate(orangeCar.name, orangeCarSpawner[0].position, orangeCarSpawner[0].rotation, 0);
        }
    }
}
