﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class RoomListInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomName_TEXT;
    [SerializeField] TextMeshProUGUI participantCount_TEXT;
    [SerializeField] Button join_BTN;

    #region private variable
    private RoomInfo roomInfo;
    private LobbyUI lobbyUI;
    #endregion


    #region LifeCycle
    private void Awake()
    {
        lobbyUI = GameObject.FindObjectOfType<LobbyUI>();
    }
    private void Start()
    {
        //join_BTN.onClick.AddListener();
    }

    #endregion

    #region Public Method
    /// <summary>
    /// 로비의 방리스트에 나타낼 방정보 세팅
    /// </summary>
    /// <param name="_roomInfo"></param>
    public void Set_RoomInfo(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomName_TEXT.text = (string)roomInfo.CustomProperties["RoomName"];
        participantCount_TEXT.text = string.Format(roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers);
    }


    /// <summary>
    /// join 버튼 클릭 시 닉네임 팝업창 활성화
    /// </summary>
    public void OnClick_Join()
    {
        lobbyUI.enterNickName_PopUp.SetActive(true);
        //PhotonNetwork.JoinRoom((string)roomInfo.CustomProperties["RoomName"]);
    }
    #endregion
}
