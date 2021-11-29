﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : ServerManager
{
    #region Roomlist variable
    [SerializeField] Transform listContent_TR;
    #endregion

    #region Public Variable
    public bool isConnected = false;
    public GameObject roomList_prefab;
    #endregion

    #region Private Variable
    private RoomInfo roomInfo;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if(!PhotonNetwork.IsConnected)
        {
            ConnectedToServer();
            isConnected = true;
            Debug.Log("Lobby 입장 완료");
        }
        else
        {
            ConnectedToServer();
            isConnected = true;
            Debug.Log("Lobby 재연결 성공 / " + "isConnected state : " + PhotonNetwork.NetworkClientState);
        }
    }
    #endregion



    #region Public Method
    public void CreateInit(float slider_value)
    {
        return;
    }

    /// <summary>
    /// 커스텀룸프로퍼티를 사용하여 방 생성
    /// </summary>
    /// <param name="roomName">LobbyUI에서 입력받는 Input값, 방이름</param>
    /// <param name="player">LobbyUI에서 Slider의 값을 받아오는 매개변수, 참가자 수</param>
    public void Create(string roomName, string nickName ,float player)
    {
        string room_name = roomName;
        string nick_name = nickName;
        if (string.IsNullOrEmpty(roomName) || string.IsNullOrEmpty(nickName))
        {
            return;
        }

        if (string.IsNullOrEmpty(room_name) || string.IsNullOrEmpty(nick_name))
        {
            roomName = "";
            nickName = "";
        }
        else
        {
            roomName = "";
            nickName = "";

            Hashtable roomHT = new Hashtable();
            roomHT.Add("RoomName", room_name);
            string[] roomList = new string[1];
            roomList[0] = "RoomName";

            RoomOptions roomOp = new RoomOptions();
            roomOp.MaxPlayers = (byte)player;
            roomOp.CustomRoomProperties = roomHT;
            roomOp.CustomRoomPropertiesForLobby = roomList;
            PhotonNetwork.CreateRoom(room_name, roomOp, null);


            Debug.Log(room_name + "방 생성");
            AppManager.Instance.ChangeScene(AppManager.eSceneState.Room);
        }
    }

    public void Quick_JoinRoom(string roomName, string nickName, float player)
    {
        string room_name = roomName;
        string nick_name = nickName; if (string.IsNullOrEmpty(roomName) || string.IsNullOrEmpty(nickName))
        {
            return;
        }

        if (string.IsNullOrEmpty(room_name) || string.IsNullOrEmpty(nick_name))
        {
            roomName = "";
            nickName = "";
        }
        else
        {
            roomName = "";
            nickName = "";

            Hashtable roomHT = new Hashtable();
            roomHT.Add("RoomName", room_name);
            string[] roomList = new string[1];
            roomList[0] = "RoomName";

            RoomOptions roomOp = new RoomOptions();
            roomOp.MaxPlayers = (byte)player;
            roomOp.CustomRoomProperties = roomHT;
            roomOp.CustomRoomPropertiesForLobby = roomList;
            //PhotonNetwork.JoinRandomOrCreateRoom(null, null, null, null, null, room_name, roomOp, null); ;
            PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null, room_name, roomOp, null);


            Debug.Log(room_name + "방 생성");
            AppManager.Instance.ChangeScene(AppManager.eSceneState.Room);
        }
    }

    /// <summary>
    /// 애플리케이션 종료
    /// </summary>
    public void Exit_BTN()
    {
        Application.Quit();
        Debug.Log("앱 종료성공");
    }
    #endregion

    #region Pun Method
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속 완료!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            roomInfo = roomList[i];
            if (roomInfo.PlayerCount == 0 || roomInfo.MaxPlayers == 0)
            {
                continue;
            }
            Instantiate(roomList_prefab, listContent_TR).GetComponent<RoomListInfo>().Set_RoomInfo(roomList[i]);
        }
    }
    #endregion

}
