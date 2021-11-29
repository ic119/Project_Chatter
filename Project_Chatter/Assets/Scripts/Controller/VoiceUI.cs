using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;
using Photon.Realtime;

public class VoiceUI : MonoBehaviour
{
    [SerializeField] Image speaking_IMG;
    [SerializeField] Image recording_IMG;
    [SerializeField] Text playerName_TEXT;

    #region Private variable
    private Canvas canvas;
    private PhotonView pv;
    private PhotonVoiceView pv_voice;
    private Player player;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        pv = this.gameObject.GetComponentInParent<PhotonView>();
        pv_voice = this.gameObject.GetComponent<PhotonVoiceView>();
        canvas = this.GetComponent<Canvas>();
    }

    private void Start()
    {
        if(pv.IsMine)
        {
            //playerName_TEXT.text = PlayerPrefs.GetString("User_Name");
            playerName_TEXT.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

    private void Update()
    {
        if (pv_voice.IsSpeaking)
        {
            speaking_IMG.enabled = true;
        }
        else
        {
            speaking_IMG.enabled = false;
        }

        if(pv_voice.IsRecording)
        {
            recording_IMG.enabled = true;
        }
        else
        {
            recording_IMG.enabled = false;
        }

        /*
        speaking_IMG.enabled = pv_voice.IsSpeaking;
        recording_IMG.enabled = pv_voice.IsRecording;
        */
    }
    #endregion
}
