﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class RoomUI : UIManager
{
    [Header("Room UI Container")]
    [SerializeField] GameObject Roominfo_Container;
    [SerializeField] GameObject Icon_Container;

    [Header("RoomInfo UI")]
    [SerializeField] TextMeshProUGUI roomName_TEXT;
    [SerializeField] TextMeshProUGUI playerCount_TEXT;

    [Header("Icon UI")]
    [SerializeField] Button exit_BTN;
    [SerializeField] Button cam_BTN;
    [SerializeField] TextMeshProUGUI cam_TEXT;
    [SerializeField] Sprite toggle_LEFT;
    [SerializeField] Sprite toggle_RIGHT;

    [SerializeField] RoomManager roomManager;

    #region public variable
    public bool isFPV = false;
    public int click_btn = 0;
    #endregion

    #region LifeCycle
    public override void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }
    private void Start()
    {
        exit_BTN.onClick.AddListener(Exit_BTN);
        cam_BTN.onClick.AddListener(Change_Cam);
    }
    #endregion

    #region Public Method
    /// <summary>
    /// FPV / TPV 시점을 전환처리하는 메서드
    /// </summary>
    public void Change_Cam()
    { 
        if (isFPV == false)
        {
            CameraController.Instance.SetCamera_Third();
            cam_TEXT.text = string.Format("TPV");
            cam_BTN.image.sprite = toggle_LEFT;
            isFPV = true;
            // 1인칭이 아닐 경우 시점을 3인칭으로 바꾸고 버튼에 대한 이미지를 변경
            // isFPV : 카메라의 시점을 판단하는 bool형식 변수
        }
        else if (isFPV == true)
        {
            CameraController.Instance.SetCamera_First();
            cam_TEXT.text = string.Format("FPV");
            cam_BTN.image.sprite = toggle_RIGHT;
            isFPV = false;
            // 3인칭이 아닐 경우 시점을 1인칭으로 바꾸고 버튼에 대한 이미지를 변경
            // isFPV : 카메라의 시점을 판단하는 bool형식 변수
        }
    }

    public override void Exit_BTN()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Room => Lobby 이동");
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
    }
    #endregion

}
