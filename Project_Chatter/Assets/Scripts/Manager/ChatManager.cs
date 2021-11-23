using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] Text chatView_TEXT;
    [SerializeField] InputField chatView_INPUT;
    [SerializeField] Button send_BTN;

    #region Private variable
    private string playerName;
    private string cur_chatChannel;
    private ChatClient chatClient;
    private ScrollRect scrollRect = null;
    #endregion

    #region LifeCycle
    private void Start()
    {
        scrollRect = GameObject.FindObjectOfType<ScrollRect>();
        Application.runInBackground = true;
        playerName = PlayerPrefs.GetString("User_Name");

        cur_chatChannel = "001";
        chatClient = new ChatClient(this);

        chatClient.UseBackgroundWorkerForSending = true;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0.0",
                            new Photon.Chat.AuthenticationValues(playerName));
    }

    private void Update()
    {
        chatClient.Service();
        OnClick_Send();
    }
    #endregion

    #region Public Method
    /// <summary>
    /// 채팅목록에 입력한 내용이 1줄씩 올라가도록 처리하는 메서드
    /// </summary>
    public void AddLine(string _chatLine)
    {
        chatView_TEXT.text += _chatLine + "\n";
    }


    public void OnApplicationQuit()
    {
        if (chatClient != null)
        {
            chatClient.Disconnect();
        }
    }
    
    /// <summary>
    /// 채팅에 보낼 내용을 입력 처리
    /// </summary>
    /// <param name="_inputTEXT"></param>
    public void SendChat(string _inputTEXT) 
    {
        if (string.IsNullOrEmpty(_inputTEXT))
        {
            return;
        }

        if (chatClient.State == ChatState.ConnectedToFrontEnd)
        {
            chatClient.PublishMessage(cur_chatChannel, _inputTEXT);
            chatView_INPUT.text = "";
        }
    }

    /// <summary>
    /// SendChat에 입력한 내용을 전송 후 input을 공란으로 초기화 처리
    /// </summary>
    public void SendMessage()
    {
        SendChat(chatView_INPUT.text);
        chatView_INPUT.text = "";
    }

    public void OnClick_Send()
    {
        send_BTN.onClick.AddListener(SendMessage);
    }
    #endregion

    #region IChatClientListener
    public void OnConnected()
    {
        AddLine("[" + playerName +"]" + "님께서 서버에 접속하였습니다.");
        chatClient.Subscribe(new string[] { cur_chatChannel }, 10);
    }

    public void OnDisconnected()
    {
        AddLine("[" + playerName + "]" + "님께서 서버에서 나가셨습니다.");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("OnChatState : " + state);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        AddLine(string.Format("채널({0}) 접속", string.Join(",", channels)));
    }

    public void OnUnsubscribed(string[] channels)
    {
        AddLine(string.Format("채널({0}) 접속 종료", string.Join(",", channels)));
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(cur_chatChannel))
        {
            for (int i = 0; i < messages.Length; i++)
            {
                AddLine(string.Format("[{0}] : {1}", senders[i], messages[i].ToString()));
            }
        }
    }
    #endregion











    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }



    

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
