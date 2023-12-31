﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = default;   // 게임 버전

    //public Text connectionInfoText;   // 네트워크 정보를 표시할 텍스트
    public Button joinButton;   // 룸 접속 버튼
    public GameObject playButtons;
    public GameObject gameModeButtons;
    public GameObject loadingObject;
    public Canvas loadingCanvas;
    public int gameMode;
    public TMP_Text nickName;
    const int STANDARD = 0;
    const int DOUBLE = 1;
    const int RUMBLE = 2;
    string gameModeStr;
    int roomIdx = 0;

    void Awake()
    {
        gameVersion = "0.1.1";   // 게임 버전 값
    }

       // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        // 서버에 접속된 상태일 경우
        if (PhotonNetwork.IsConnected)
        {
            // 접속 정보 표시
            //connectionInfoText.text = "Online: connected to master server succed";
            // 로딩 오브젝트 비활성화
            loadingObject.SetActive(false);
        }

        // 서버에 접속이 되지 않았을 경우
        else
        {
               // 접속에 필요한 정보 설정
            PhotonNetwork.GameVersion = gameVersion;
               // 설정한 정보로 마스터 서버 접속 시도
            PhotonNetwork.ConnectUsingSettings();
               // 룸 접속 버튼 잠시 비활성화
            joinButton.interactable = false;
               // 접속 시도 중 임을 텍스트로 표시
            //connectionInfoText.text = "Connect to master server ...";
        }
    }      // Start()

       // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
           // 룸 접속 버튼 활성화
        joinButton.interactable = true;
        // 접속 정보 표시
        //connectionInfoText.text = "Online: connected to master server succed";
        // 로딩 오브젝트 비활성화
        loadingObject.SetActive(false);
    }      // OnConnectedToMaster()

       // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
           // 룸 접속 버튼 비활성화
        joinButton.interactable = false;
           // 룸 접속 정보 표시
        //connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
           // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }      // OnDisconnected ()



    public void StandardMode()
    {
        gameMode= STANDARD;
        Connect();
    }
    public void DoubleMode()
    {
        gameMode = DOUBLE;
        Connect();
    }

    public void RumbleMode()
    {
        gameMode=RUMBLE;
        Connect();
    }

       // 룸 접속 시도
    public void Connect()
    {
           // 중복 접속 시도를 막기 위해 접속 버튼 잠시 비활성화
        joinButton.interactable = false;

        // 마스터 서버에 접속 중이라면
        if (PhotonNetwork.IsConnected)
        {

            PhotonNetwork.NickName=nickName.text;
            // �� ���� ����
            RoomOptions roomOptions = new RoomOptions
            {
               

                MaxPlayers = 6, // 최대 플레이어 수 조정
            
                IsOpen = true, // 방 열기
                IsVisible = true, // 방 표시하기
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "GameMode", gameMode } // 게임 모드를 방 속성으로 설정
            }
            };


            switch(gameMode)
            {
                case STANDARD:
                    gameModeStr=string.Format("Standard{0}", roomIdx);
                  
                    break;
                    case DOUBLE:
                    gameModeStr=string.Format("Double{0}",roomIdx);
                    roomOptions.MaxPlayers=4;
                  
                    break;
                case RUMBLE:
                    gameModeStr=string.Format("Rumble{0}",roomIdx);

                    break;
                    default:
                    break;

            }
            //connectionInfoText.text = "Connect to Room ...";
            PhotonNetwork.JoinOrCreateRoom(gameModeStr, roomOptions,TypedLobby.Default);
        }
        else
        {
            // ������ ������ ���� ���� �ƴ϶�� ������ ������ ���� �õ�
            //connectionInfoText.text = string.Format("{0}\n{1}", "offline: Disconnected to master server", "Retry connect now ...");
        }
    }   // Connect()

       // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
           // 접속 상태 표시
        //connectionInfoText.text = "Nothing to empty room, Create new room ...";
           // �ִ� 6�� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 6 });
    }      // OnJoinRandomFailed()

    public override void OnJoinRoomFailed(short returnCode, string message)
    {

        roomIdx+=1;
        RoomOptions roomOptions = new RoomOptions
        {


            MaxPlayers = 6, // 최대 플레이어 수 조정

            IsOpen = true, // 방 열기
            IsVisible = true, // 방 표시하기
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "GameMode", gameMode } // 게임 모드를 방 속성으로 설정
            }
        };
        switch (gameMode)
        {
            case STANDARD:
                gameModeStr=string.Format("Standard{0}", roomIdx);

                break;
            case DOUBLE:
                gameModeStr=string.Format("Double{0}", roomIdx);
                roomOptions.MaxPlayers=4;

                break;
            case RUMBLE:
                gameModeStr=string.Format("Rumble{0}", roomIdx);

                break;
            default:
                break;

        }
        //connectionInfoText.text = "Connect to Room ...";
        PhotonNetwork.CreateRoom(gameModeStr, roomOptions, TypedLobby.Default);

    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    { 
            // 접속 상태 표시
        //connectionInfoText.text = "Successes joined room";
        PhotonNetwork.LeaveLobby();   // Lobby 씬을 떠나는 함수 실행
        // 플레이 버튼을 누르면 게임 룸 씬으로 이동
        if(gameMode == STANDARD)
        {
            // Loading_Canvas를 활성화
            ShowLoadingUI();
            PhotonNetwork.LoadLevel("StandardScene");
        }
        if(gameMode == DOUBLE)
        {
            ShowLoadingUI();
            PhotonNetwork.LoadLevel("DoubleScene");

        }
        if(gameMode==RUMBLE)
        {
            // Loading_Canvas를 활성화
            ShowLoadingUI();
            PhotonNetwork.LoadLevel("RumbleScene");
        }
    }      // OnJoinedRoom()
    private void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4, // 최대 플레이어 수 조정
            IsOpen = true, // 방 열기
            IsVisible = true, // 방 표시하기
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "GameMode", gameMode } // 게임 모드를 방 속성으로 설정
            }
        };

        // 방 생성 시도
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    public void MatchButtonActive()
    {
        playButtons.SetActive(false);
        gameModeButtons.SetActive(true);
    }

    // 커스터마이징 버튼을 눌렀을 때 호출되는 함수
    // "CustomizingScene"을 로드한다.
    public void OnCustomizingButton()
    {
        // Loading_Canvas를 활성화
        ShowLoadingUI();
        // "CustomizingScene"을 로드
        SceneManager.LoadScene("CustomizingScene");
    }

    public void BackButton()
    {
        playButtons.SetActive(true);
        gameModeButtons.SetActive(false);
    }

    // 게임을 종료하는 함수
    // 종료 버튼을 눌렀을 때 호출된다.
    public void ExitButton()
    {
        // 게임 종료
        Application.Quit();
    }

    // Loading_Canvas를 활성화 하는 함수
    private void ShowLoadingUI()
    {
        // Loading_Canvas를 활성화 
        loadingCanvas.gameObject.SetActive(true);
    }
}