using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class RoomManager : MonoBehaviourPunCallbacks, IInRoomCallbacks   // �� ���� �ݹ� ��� ���
{
    [SerializeField] private Canvas roomCanvas;
    [SerializeField] private Image playerCountImage;
    [SerializeField] private Image[] teamColorImage = new Image[2];
    [SerializeField] private Image infoImage;
    [SerializeField] private PhotonView pv;
    [SerializeField] private Text playerCount;   // ���� �뿡 ������ �÷��̾� �� ǥ��
    [SerializeField] private Button[] roomButton = new Button[4];   // ���� �뿡 Ȱ��ȭ �Ǿ��ִ� ��ư�� �迭�� ����
    [SerializeField] private TextMeshProUGUI[] teamCountText = new TextMeshProUGUI[2];   // �� ������ �÷��̾� ǥ�� �ؽ�Ʈ ����
    [SerializeField] private Text infoText;   // �� ���� ���� ǥ�� �ؽ�Ʈ ����
    [SerializeField] private GameValue gameValue;
    
    private int currPlayer = default;   // ���� ���� �뿡 �����ϴ� �÷��̾� ��
    private int maxPlayer = default;   // ���� ���� ���� �ִ� �÷��̾� ��
    private int[] teamCount = new int[2];   // ���� ������ ������ �÷��̾� ����
    private int maxTeamCount = default;   // ������ ������ �� �ִ� �ִ� �÷��̾� ��
    private int randomSelect = default;   // �ڵ����� �� ������ �� ������
    private int teamCheck = default;
    private bool isSelectTeam = false;
    private bool isGameStart = false;
    private int[] _teamCount = new int[2];
    private string[] setText = new string[2];

    void Awake()
    {
        maxTeamCount = PhotonNetwork.CurrentRoom.MaxPlayers / 2;   // ������ ������ �� �ִ� �ִ� �÷��̾� ���� ���� �ִ� �÷��̾� ���ڿ� 2 �� �������ش�
        randomSelect = 0;
        teamCheck = 0;
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();

        roomCanvas = transform.Find("JoinedRoom_UI_Canvas").GetComponent<Canvas>();
        playerCountImage = roomCanvas.transform.Find("PlayerCount").GetComponent<Image>();
        playerCount = playerCountImage.transform.Find("Text_PlayerCount").GetComponent<Text>();

        roomButton[0] = roomCanvas.transform.Find("Button_1").GetComponent<Button>();
        roomButton[0].onClick.AddListener(GetComponent<RoomManager>().JoinBlueTeam);
        roomButton[1] = roomCanvas.transform.Find("Button_2").GetComponent<Button>();
        roomButton[1].onClick.AddListener(GetComponent<RoomManager>().JoinRandomTeam);
        roomButton[2] = roomCanvas.transform.Find("Button_3").GetComponent<Button>();
        roomButton[2].onClick.AddListener(GetComponent<RoomManager>().JoinOrangeTeam);
        roomButton[3] = roomCanvas.transform.Find("Button_4").GetComponent<Button>();
        roomButton[3].onClick.AddListener(GetComponent<RoomManager>().ExitRoom);

        teamColorImage[0] = roomCanvas.transform.Find("Blue").GetComponent<Image>();
        teamCountText[0] = teamColorImage[0].transform.Find("Text_BlueCount").GetComponent<TextMeshProUGUI>();
        teamColorImage[1] = roomCanvas.transform.Find("Orange").GetComponent<Image>();
        teamCountText[1] = teamColorImage[1].transform.Find("Text_OrangeCount").GetComponent<TextMeshProUGUI>();

        infoImage = roomCanvas.transform.Find("Info").GetComponent<Image>();
        infoText = infoImage.transform.Find("Text_Info").GetComponent<Text>();

        gameValue = GameObject.Find("GameValue").GetComponent<GameValue>();

        Invoke("CheckPlayerCount", 0.5f);   // ���� �� ���� �� 0.5 �� �Ŀ� �� ������ �������ش�
    }

    void Update()
    {
        //if (isGameStart == true) { return; }
        Debug.LogFormat("{0}, {1}", teamCount[0], teamCount[1]);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)   // ���ο� �÷��̾ �뿡 ������ �� ���� �� ������ �������ش�
    {
        CheckPlayerCount();   // �� ���� ���� �Լ�
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)   // �÷��̾ �뿡�� ���� �� ���� �� ������ �������ش�
    {
        CheckPlayerCount();   // �� ���� ���� �Լ�
    }

    public void CheckPlayerCount()   // �� ���� ���� �Լ�
    {
        currPlayer = PhotonNetwork.PlayerList.Length;   // ���� ���� �뿡 �������� �÷��̾� ��
        maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;   // ���� ���� ���� �ִ� �÷��̾� ��
        playerCount.text = string.Format("Player : {0} / {1}", currPlayer, maxPlayer);
    }

    public void ExitRoom()   // ���� �뿡�� ���� �� ����Ǵ� �Լ�
    {
        PhotonNetwork.LeaveRoom();   // Room ���� ������ �Լ� ����
        SceneManager.LoadScene("SampleScene");   // �κ� ���� �ε��Ѵ�
        Invoke("CheckPlayerCount", 0.5f);   // ���� ���� ���� �� 0.5 �� �Ŀ� �� ������ �������ش�
    }

    public void JoinBlueTeam()   // ��翡 �� ���� ��ư�� ���� �� ����
    {
        if (pv.IsMine == true)
        {
            if (isSelectTeam == true) { return; }
            if (teamCount[0] >= maxTeamCount) { return; }   // �ش� ���� �̹� �շ��� �ο��� �ִ� �ο��̸� return ����
            if (teamCheck == 1) { return; }

            teamCheck = 1;
            isSelectTeam = true;
            gameValue.GetComponent<GameValue>().playerTeamCheck = 1;

            FormatBlueTeamCount();

            //pv.RPC("FormatBlueTeamCount", RpcTarget.AllBuffered, teamCount[0], teamCount[1]);
        }
    }

    public void JoinOrangeTeam()   // ������ �� ���� ��ư�� ���� �� ����
    {
        if (pv.IsMine == true)
        {
            if (isSelectTeam == true) { return; }
            if (teamCount[1] >= maxTeamCount) { return; }   // �ش� ���� �̹� �շ��� �ο��� �ִ� �ο��̸� return ����
            if (teamCheck == 2) { return; }

            teamCheck = 2;
            isSelectTeam = true;
            gameValue.GetComponent<GameValue>().playerTeamCheck = 2;

            FormatOrangeTeamCount();

            //pv.RPC("FormatOrangeTeamCount", RpcTarget.AllBuffered, teamCount[0], teamCount[1]);
        }
    }

    public void JoinRandomTeam()   // �ڵ� �� ���� ��ư�� ���� �� ����
    {
        if (pv.IsMine == true)
        {
            if (isSelectTeam == true) { return; }

            randomSelect = Random.Range(0, 2);   // int 0, 1 ���� �������� ����

            if (teamCount[randomSelect] < maxTeamCount)   // �ڵ����� ������ ���� �÷��̾� ���� ���� �ִ� ������ �Ǵ�
            {
                if (randomSelect == 0) { JoinBlueTeam(); }   // �ִ� �� ���� ������ ���� ����ŭ �� ���� ����
                else if (randomSelect == 1) { JoinOrangeTeam(); }
            }
            else
            {
                ChangeRandom();   // �ڵ����� ������ ���� �÷��̾� ���� ���� �ִ� �� �϶� ����
            }
        }
    }

    public void ChangeRandom()   // �ڵ����� ������ ���� �÷��̾� ���� ���� �ִ� �� �϶� ����
    {
        if (randomSelect == 0)
        {
            if (teamCount[1] < maxTeamCount) { JoinOrangeTeam(); }   // �ڵ����� ������ ���� �ƴ� �ٸ� ���� ������
        }
        else if (randomSelect == 1)
        {
            if (teamCount[0] < maxTeamCount) { JoinBlueTeam(); }   // �ڵ����� ������ ���� �ƴ� �ٸ� ���� ������
        }
    }

    public void FormatBlueTeamCount()
    {
        teamCount[0] += 1;   // ���� �շ��� �ο��� 1 ������Ų��

        teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // ������Ų ���� ���
        setText[0] = teamCountText[0].text;

        infoText.text = string.Format(setText[0]);

        pv.RPC("ApplyBlueTeamCount", RpcTarget.AllBuffered, teamCount[0], setText[0]);
    }

    [PunRPC]
    public void ApplyBlueTeamCount(int teamCount1, string setText1)
    {
        teamCount[0] = teamCount1;
        setText[0] = setText1;

        teamCountText[0].text = string.Format(setText[0]);   // ������Ų ���� ���
    }

    public void FormatOrangeTeamCount()
    {
        teamCount[1] += 1;   // ���� �շ��� �ο��� 1 ������Ų��

        teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);   // ������Ų ���� ���
        setText[1] = teamCountText[1].text;

        infoText.text = string.Format(setText[1]);

        pv.RPC("ApplyOrangeTeamCount", RpcTarget.AllBuffered, teamCount[1], setText[1]);
    }

    [PunRPC]
    public void ApplyOrangeTeamCount(int teamCount2, string setText2)
    {
        teamCount[1] = teamCount2;
        setText[1] = setText2;

        teamCountText[1].text = string.Format(setText[1]);   // ������Ų ���� ���
    }

    IEnumerator GameStart()
    {
        infoText.text = string.Format("��� �� ������ ���۵˴ϴ�");

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 3; i++)
        {
            infoText.text = string.Format("{0} �� �� ������ ���۵˴ϴ�", 3 - i);

            yield return new WaitForSeconds(1f);
        }

        infoText.text = string.Format("���� ����");
        PhotonNetwork.LoadLevel("StandardScene");   // ���� ���� �� �ε�
    }

    public void TestGame()
    {
        PhotonNetwork.LoadLevel("StandardScene");   // ���� ���� �� �ε�
    }

    //// �ֱ������� �ڵ� ����Ǵ�, ����ȭ �޼���
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
    //    }
    //}

    //public void AfterSerializeView()
    //{
    //    infoText.text = string.Format("{0}, {1}, {2}", teamCheck, teamCount[0], teamCount[1]);
    //}
}
