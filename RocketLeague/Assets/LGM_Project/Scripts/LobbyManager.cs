using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = default;   // 게임 버전

    public Text connectionInfoText;   // 네트워크 정보를 표시할 텍스트
    public Button joinButton;   // 룸 접속 버튼

    void Awake()
    {
        gameVersion = "0.1.0";   // 게임 버전 값
    }

       // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
           // 접속에 필요한 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
           // 설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

           // 룸 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
           // 접속 시도 중 임을 텍스트로 표시
        connectionInfoText.text = "Connect to master server ...";
    }      // Start()

       // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
           // 룸 접속 버튼 활성화
        joinButton.interactable = true;
           // 접속 정보 표시
        connectionInfoText.text = "Online: connected to master server succed";
    }      // OnConnectedToMaster()

       // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
           // 룸 접속 버튼 비활성화
        joinButton.interactable = false;
           // 룸 접속 정보 표시
        connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
           // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }      // OnDisconnected ()

       // 룸 접속 시도
    public void Connect()
    {
           // 중복 접속 시도를 막기 위해 접속 버튼 잠시 비활성화
        joinButton.interactable = false;

           // 마스터 서버에 접속 중이라면
        if (PhotonNetwork.IsConnected)
        {
               // 룸 접속 실행
            connectionInfoText.text = "Connect to Room ...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
               // 마스터 서버에 접속 중이 아니라면 마스터 서버에 접속 시도
            connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
        }
    }   // Connect()

       // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
           // 접속 상태 표시
        connectionInfoText.text = "Nothing to empty room, Create new room ...";
           // 최대 6명 수용 가능한 빈 방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }      // OnJoinRandomFailed()



       // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    { 
            // 접속 상태 표시
        connectionInfoText.text = "Successes joined room";
        PhotonNetwork.LeaveLobby();   // Lobby 씬을 떠나는 함수 실행
        // 플레이 버튼을 누르면 게임 룸 씬으로 이동
        PhotonNetwork.LoadLevel("RoomScene");
    }      // OnJoinedRoom()
}