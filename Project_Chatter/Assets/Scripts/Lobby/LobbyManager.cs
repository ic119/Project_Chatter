using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : ServerManager
{


    #region Public Variable
    public bool isConnected = false;
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
    public void Create(string roomName, float player)
    {
        string name = roomName;
        if (string.IsNullOrEmpty(roomName))
        {
            return;
        }

        if (string.IsNullOrEmpty(name))
        {
            roomName = "";
        }
        else
        {
            roomName = "";

            Hashtable roomHT = new Hashtable();
            roomHT.Add("RoomName", name);
            string[] roomList = new string[1];
            roomList[0] = "RoomName";

            RoomOptions roomOp = new RoomOptions();
            roomOp.MaxPlayers = (byte)player;
            roomOp.CustomRoomProperties = roomHT;
            roomOp.CustomRoomPropertiesForLobby = roomList;
            PhotonNetwork.CreateRoom(name, roomOp, null);

            Debug.Log(name + "방 생성");
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

    #region Private Method
    
    #endregion
}
