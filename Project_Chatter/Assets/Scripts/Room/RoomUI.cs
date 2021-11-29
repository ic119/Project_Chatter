using System.Collections;
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

    public bool isFPV = false;
    public int click_btn = 0;

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
    public void Change_Cam()
    {
        if (click_btn == 0)
        {
            cam_TEXT.text = string.Format("TPV");
            cam_BTN.image.sprite = toggle_LEFT;
            click_btn = 1;
            Debug.Log(click_btn);
        }
        else if (click_btn == 1)
        {
            cam_TEXT.text = string.Format("FPV");
            cam_BTN.image.sprite = toggle_RIGHT;
            click_btn = 0;
            Debug.Log(click_btn);
        }
    }

    public override void Exit_BTN()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LeaveRoom();
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Room => Lobby 이동");
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
    }
    #endregion

}
