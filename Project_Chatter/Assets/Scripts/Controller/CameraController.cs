using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    #region Singleton
    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CameraController();
            }
            return instance;
        }
    }
    #endregion

    #region Public variable
    public Transform cam_target;
    #endregion

    #region Private variable
    private RoomUI roomUI;

    [SerializeField] Camera firstCam;
    [SerializeField] Camera thirdCam;
    private PhotonView pv;
    //[SerializeField] Vector3 cam_offSet;
    #endregion


    #region LifeCycle
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        firstCam.enabled = false;
        thirdCam.enabled = false;
        roomUI = GameObject.FindObjectOfType<RoomUI>();
        pv = this.gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            return;
        }
        else if (pv.IsMine)
        {
            SetCamera_Third();
        }
    }

    private void Update()
    {
        this.gameObject.transform.position = cam_target.position;
    }
    #endregion
    #region Public Method
    
    /// <summary>
    /// FPV 카메라 연결
    /// </summary>
    public void SetCamera_First()
    {
        //this.gameObject.transform.position = GameObject.FindWithTag("FirstCam").transform.position;
        firstCam.enabled = true;
        thirdCam.enabled = false;
    }
    /// <summary>
    /// TPV 카메라 연결
    /// </summary>
    public void SetCamera_Third()
    {
        //this.gameObject.transform.position = GameObject.FindWithTag("MainCamera").transform.position;
        thirdCam.enabled = true;
        firstCam.enabled = false;
    }
    
    #endregion
}
