using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class LobbyUI : MonoBehaviour
{
    [Header("Button Container")]
    [SerializeField]GameObject buttonContainer;
    [SerializeField]GameObject IconContainer;
    [SerializeField]Button createRoom_BTN;
    [SerializeField]Button quick_BTN;
    [SerializeField]Button Exit_BTN;

    [Header("Room List")]
    [SerializeField]GameObject roomListContainer;

    [Header("Server Link TEXT")]
    [SerializeField]TextMeshProUGUI serverLink_TEXT;

    [Header("Create Room UI")]
    [SerializeField]GameObject createRoom_PopUp;
    [SerializeField]Slider playerCounter_SLIDER;
    [SerializeField]TMP_InputField roomName_INPUT;
    [SerializeField]TMP_InputField nickName_INPUT;
    [SerializeField]Button create_BTN;
    [SerializeField]Button cancel_BTN;
    [SerializeField]TextMeshProUGUI playerCount_VALUE;


    [SerializeField]LobbyManager lobbyManager;

    #region Private Variable
    private const string linking_TEXT = "Connecting To Server...";
    private const string linked_TEXT = "Connected To Server";
    #endregion 

    #region LifeCycle
    private void Awake()
    {
        lobbyManager = GameObject.FindObjectOfType<LobbyManager>();
        Init();
        Create_Init();
    }
    

    private void Start()
    {

        if (lobbyManager.isConnected == true)
        {
            Invoke("LinkedServer", 1.5f);
        }

        Exit_BTN.onClick.AddListener(lobbyManager.Exit_BTN);
        createRoom_BTN.onClick.AddListener(PopUp_On);
        create_BTN.onClick.AddListener(CreateRoom);
        cancel_BTN.onClick.AddListener(PopUp_Off);

        playerCounter_SLIDER.onValueChanged.AddListener(delegate { SetPlayerCount(); });
    }
    #endregion

    #region Private Method
    /// <summary>
    /// 서버 연결 후 Lobby에 있는 UI 비활성화로 초기화
    /// </summary>
    private void Init()
    {
        serverLink_TEXT.text = linking_TEXT;
        roomListContainer.SetActive(false);
        buttonContainer.SetActive(false);
    }

    private void LinkedServer()
    {
        serverLink_TEXT.text = linked_TEXT;
        roomListContainer.SetActive(true);
        buttonContainer.SetActive(true);

    }
    private void PopUp_On()
    {
        createRoom_PopUp.SetActive(true);
    }

    private void PopUp_Off()
    {
        createRoom_PopUp.SetActive(false);
    }
    /// <summary>
    /// 방 생성 팝업창에서 slider의 default값을 2로 설정
    /// </summary>
    private void Create_Init()
    {
        playerCounter_SLIDER.value = 2;
        playerCount_VALUE.text = playerCounter_SLIDER.value.ToString();
        lobbyManager.CreateInit(playerCounter_SLIDER.value);
    }

    private void SetPlayerCount()
    {
        playerCount_VALUE.text = playerCounter_SLIDER.value.ToString();
    }

    private void CreateRoom()
    {
        lobbyManager.Create(roomName_INPUT.text, nickName_INPUT.text, playerCounter_SLIDER.value);
        PhotonNetwork.NickName = nickName_INPUT.text;
        PlayerPrefs.SetString("User_Name", PhotonNetwork.NickName);
        Debug.Log(PlayerPrefs.HasKey("User_Name"));
        
    }
    #endregion
}
