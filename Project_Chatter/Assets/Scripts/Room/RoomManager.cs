﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : ServerManager
{

    #region public variable
    public GameObject[] characters;
    #endregion

    [Header("RoomInfo UI")]
    [SerializeField] TextMeshProUGUI roomName_TEXT;
    [SerializeField] TextMeshProUGUI playerCount_TEXT;

    [SerializeField] RoomUI roomUI;
    [SerializeField] Transform RespawnSpot;
    #region Public variable
    public bool isMaster = false;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        roomUI = GameObject.FindObjectOfType<RoomUI>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isMaster = true;
        }
    }
    #endregion

    #region Pun Method
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "방 입장 성공");
        PhotonNetwork.AutomaticallySyncScene = true;

        Hashtable infoHT = PhotonNetwork.CurrentRoom.CustomProperties;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room", "room" } });
            Hashtable info = PhotonNetwork.LocalPlayer.CustomProperties;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                PhotonNetwork.PlayerList[i].SetCustomProperties(new Hashtable {{"Room", "room"}});
            }
        }
        CreateCharacter();
        SetRoomInfo();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
    }
    #endregion

    #region Private Method
    private void SetRoomInfo()
    {
        roomName_TEXT.text = string.Format("Room : " + PhotonNetwork.CurrentRoom.Name + " Room");
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    private void UpdatePlayerCount()
    {
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    private void CreateCharacter()
    {
        if (characters == null)
        {
            Debug.Log("생성된 캐릭터가 없습니다.");
        }
        else
        {
            int randNum = Random.Range(0, 5);
            GameObject go = Instantiate(characters[randNum], RespawnSpot.transform.position, Quaternion.identity);
            Debug.Log("캐릭터 생성 완료");
            PhotonView pv = go.AddComponent<PhotonView>();
            
            PhotonTransformView pv_tr = go.AddComponent<PhotonTransformView>();
        }
    }
    #endregion
}