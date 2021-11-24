using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;

public class VoiceUI : MonoBehaviour
{
    [SerializeField] Image speaking_IMG;
    [SerializeField] Image recording_IMG;
    [SerializeField] Text playerName_TEXT;

    #region Private variable
    private Canvas canvas;
    private PhotonVoiceView pv_voice;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        pv_voice = this.gameObject.GetComponent<PhotonVoiceView>();
        canvas = this.GetComponent<Canvas>();
    }

    private void Start()
    {
        playerName_TEXT.text = PlayerPrefs.GetString("User_Name");
    }

    private void Update()
    {
        speaking_IMG.enabled = pv_voice.IsSpeaking;
        recording_IMG.enabled = pv_voice.IsRecording;
    }
    #endregion
}
