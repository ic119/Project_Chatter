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
    [SerializeField] Button option_Btn;

    [SerializeField] RoomManager roomManager;

    #region LifeCycle
    public override void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        option_Btn.enabled = false;
    }

    private void Start()
    {
        if (roomManager.isMaster == true)
        {
            Active_Option();
        }

        exit_BTN.onClick.AddListener(Exit_BTN);
    }
    #endregion

    #region Private Method
    private void Active_Option()
    {
        option_Btn.enabled = true;
    }

    public override void Exit_BTN()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Room => Lobby 이동");
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
    }
    #endregion

}
