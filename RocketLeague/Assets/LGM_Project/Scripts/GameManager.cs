using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks /*IPunObservable*/
{
    public static GameManager instance
    {
        get
        {
               // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                   // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }
               // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance;   // �̱����� �Ҵ�� static ����

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

    // �ֱ������� �ڵ� ����Ǵ�, ����ȭ �޼���
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    // ���� ������Ʈ��� ���� �κ��� �����
    //    if (stream.IsWriting)
    //    {
    //        // ��Ʈ��ũ�� ���� score ���� ������
    //        stream.SendNext(teamCount[0]);
    //        stream.SendNext(teamCount[1]);
    //    }
    //    else
    //    {
    //        // ����Ʈ ������Ʈ��� �б� �κ��� �����

    //        // ��Ʈ��ũ�� ���� score �� �ޱ�
    //        teamCount[0] = (int)stream.ReceiveNext();
    //        teamCount[1] = (int)stream.ReceiveNext();
    //        // ����ȭ�Ͽ� ���� ������ UI�� ǥ��

    //        teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // ������Ų ���� ���
    //        teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);

    //        AfterSerializeView();
    //    }
    //}
}
