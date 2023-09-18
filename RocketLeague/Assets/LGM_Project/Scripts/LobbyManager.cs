using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = default;   // ���� ����

    public Text connectionInfoText;   // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton;   // �� ���� ��ư
    public GameObject playButtons;
    public GameObject gameModeButtons;
    void Awake()
    {
        gameVersion = "0.1.1";   // ���� ���� ��
    }

       // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
           // ���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
           // ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

           // �� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
           // ���� �õ� �� ���� �ؽ�Ʈ�� ǥ��
        connectionInfoText.text = "Connect to master server ...";
    }      // Start()

       // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
           // �� ���� ��ư Ȱ��ȭ
        joinButton.interactable = true;
           // ���� ���� ǥ��
        connectionInfoText.text = "Online: connected to master server succed";
    }      // OnConnectedToMaster()

       // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
           // �� ���� ��ư ��Ȱ��ȭ
        joinButton.interactable = false;
           // �� ���� ���� ǥ��
        connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
           // ������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }      // OnDisconnected ()

       // �� ���� �õ�
    public void Connect()
    {
           // �ߺ� ���� �õ��� ���� ���� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;

           // ������ ������ ���� ���̶��
        if (PhotonNetwork.IsConnected)
        {
               // �� ���� ����
            connectionInfoText.text = "Connect to Room ...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
               // ������ ������ ���� ���� �ƴ϶�� ������ ������ ���� �õ�
            connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }   // Connect()

       // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
           // ���� ���� ǥ��
        connectionInfoText.text = "Nothing to empty room, Create new room ...";
           // �ִ� 6�� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }      // OnJoinRandomFailed()



       // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    { 
            // ���� ���� ǥ��
        connectionInfoText.text = "Successes joined room";
        PhotonNetwork.LeaveLobby();   // Lobby ���� ������ �Լ� ����
        // �÷��� ��ư�� ������ ���� �� ������ �̵�
        PhotonNetwork.LoadLevel("StandardScene");
    }      // OnJoinedRoom()

    public void MatchButtonActive()
    {
        playButtons.SetActive(false);
        gameModeButtons.SetActive(true);
    }

    public void BackButton()
    {
        playButtons.SetActive(true);
        gameModeButtons.SetActive(false);
    }
}