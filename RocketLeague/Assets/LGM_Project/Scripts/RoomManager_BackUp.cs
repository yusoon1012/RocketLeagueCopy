using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class RoomManager_BackUp : MonoBehaviourPunCallbacks, IPunObservable, IInRoomCallbacks   // �� ���� �ݹ� ��� ���
{
    private PhotonView pv;
    private Text playerCount;   // ���� �뿡 ������ �÷��̾� �� ǥ��
    private Button[] roomButton = new Button[4];   // ���� �뿡 Ȱ��ȭ �Ǿ��ִ� ��ư�� �迭�� ����
    private TextMeshProUGUI[] teamCountText = new TextMeshProUGUI[2];   // �� ������ �÷��̾� ǥ�� �ؽ�Ʈ ����
    private Text infoText;   // �� ���� ���� ǥ�� �ؽ�Ʈ ����
    private GameValue gameValue;

    private int currPlayer = default;   // ���� ���� �뿡 �����ϴ� �÷��̾� ��
    private int maxPlayer = default;   // ���� ���� ���� �ִ� �÷��̾� ��
    private int[] teamCount = new int[2];   // ���� ������ ������ �÷��̾� ����
    private int maxTeamCount = default;   // ������ ������ �� �ִ� �ִ� �÷��̾� ��
    private int randomSelect = default;   // �ڵ����� �� ������ �� ������
    private int teamCheck = default;
    private bool isSelectTeam = false;
    private bool isGameStart = false;

    void Awake()
    {
        maxTeamCount = PhotonNetwork.CurrentRoom.MaxPlayers / 2;   // ������ ������ �� �ִ� �ִ� �÷��̾� ���� ���� �ִ� �÷��̾� ���ڿ� 2 �� �������ش�
        randomSelect = 0;
        teamCheck = 0;
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
        playerCount = GameObject.Find("Text_PlayerCount").GetComponent<Text>();
        roomButton[0] = GameObject.Find("Button_1").GetComponent<Button>();
        roomButton[1] = GameObject.Find("Button_2").GetComponent<Button>();
        roomButton[2] = GameObject.Find("Button_3").GetComponent<Button>();
        roomButton[3] = GameObject.Find("Button_4").GetComponent<Button>();
        roomButton[0].onClick.AddListener(JoinBlueTeam);
        roomButton[1].onClick.AddListener(JoinRandomTeam);
        roomButton[2].onClick.AddListener(JoinOrangeTeam);
        roomButton[3].onClick.AddListener(ExitRoom);
        teamCountText[0] = GameObject.Find("Text_BlueCount").GetComponent<TextMeshProUGUI>();
        teamCountText[1] = GameObject.Find("Text_OrangeCount").GetComponent<TextMeshProUGUI>();
        infoText = GameObject.Find("Text_Info").GetComponent<Text>();
        gameValue = GameObject.Find("GameValue").GetComponent<GameValue>();

        Invoke("CheckPlayerCount", 0.5f);   // ���� �� ���� �� 0.5 �� �Ŀ� �� ������ �������ش�
    }

    void Update()
    {
        if (isGameStart == true) { return; }

        if (teamCount[0] == maxTeamCount && teamCount[1] == maxTeamCount)
        {
            isGameStart = true;
            StartCoroutine(GameStart());
        }
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
            if (teamCount[0] >= maxTeamCount) { return; }   // �ش� ���� �̹� �շ��� �ο��� �ִ� �ο��̸� return ����
            if (teamCheck == 1) { return; }

            pv.RPC("FormatBlueTeamCount", RpcTarget.AllBuffered, teamCount[0], teamCount[1]);
        }
    }

    public void JoinOrangeTeam()   // ������ �� ���� ��ư�� ���� �� ����
    {
        if (pv.IsMine == true)
        {
            if (teamCount[1] >= maxTeamCount) { return; }   // �ش� ���� �̹� �շ��� �ο��� �ִ� �ο��̸� return ����
            if (teamCheck == 2) { return; }

            pv.RPC("FormatOrangeTeamCount", RpcTarget.AllBuffered, teamCount[0], teamCount[1]);
        }
    }

    public void JoinRandomTeam()   // �ڵ� �� ���� ��ư�� ���� �� ����
    {
        if (pv.IsMine == true)
        {
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

    [PunRPC]
    public void FormatBlueTeamCount(int teamCount1, int teamCount2)
    {
        teamCount[0] = teamCount1;
        teamCount[1] = teamCount2;

        teamCount[0] += 1;   // ���� �շ��� �ο��� 1 ������Ų��
        if (teamCheck == 2) { teamCount[1] -= 1; }

        teamCheck = 1;
        gameValue.GetComponent<GameValue>().playerTeamCheck = 1;

        infoText.text = string.Format("{0}, {1}, {2}", teamCheck, teamCount[0], teamCount[1]);

        teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // ������Ų ���� ���
        teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);
    }

    [PunRPC]
    public void FormatOrangeTeamCount(int teamCount1, int teamCount2)
    {
        teamCount[0] = teamCount1;
        teamCount[1] = teamCount2;

        teamCount[1] += 1;   // ���� �շ��� �ο��� 1 ������Ų��
        if (teamCheck == 1) { teamCount[0] -= 1; }

        teamCheck = 2;
        gameValue.GetComponent<GameValue>().playerTeamCheck = 2;

        infoText.text = string.Format("{0}, {1}, {2}", teamCheck, teamCount[0], teamCount[1]);

        teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // ������Ų ���� ���
        teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);
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

    // �ֱ������� �ڵ� ����Ǵ�, ����ȭ �޼���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���� ������Ʈ��� ���� �κ��� �����
        if (stream.IsWriting)
        {
            // ��Ʈ��ũ�� ���� score ���� ������
            stream.SendNext(teamCount[0]);
            stream.SendNext(teamCount[1]);
        }
        else
        {
            // ����Ʈ ������Ʈ��� �б� �κ��� �����

            // ��Ʈ��ũ�� ���� score �� �ޱ�
            teamCount[0] = (int)stream.ReceiveNext();
            teamCount[1] = (int)stream.ReceiveNext();
            // ����ȭ�Ͽ� ���� ������ UI�� ǥ��

            teamCountText[0].text = string.Format("{0} / {1}", teamCount[0], maxTeamCount);   // ������Ų ���� ���
            teamCountText[1].text = string.Format("{0} / {1}", teamCount[1], maxTeamCount);

            AfterSerializeView();
        }
    }

    public void AfterSerializeView()
    {
        infoText.text = string.Format("{0}, {1}, {2}", teamCheck, teamCount[0], teamCount[1]);
    }
}
