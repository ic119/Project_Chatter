using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera thirdCam;
    [SerializeField] Camera firstCam;

    #region Public variable
    public Transform cam_target;
    public enum Cam_State
    {
        Third_Cam,
        First_Cam
    }

    public Cam_State eCamera;
    #endregion

    #region Private variable
    private RoomUI roomUI;
    #endregion


    #region LifeCycle
    private void Awake()
    {
        firstCam.enabled = false;
        thirdCam.enabled = true;
        roomUI = GameObject.FindObjectOfType<RoomUI>();
    }
    private void Update()
    {
        switch(roomUI.click_btn)
        {
            case (int)Cam_State.Third_Cam:
                {
                    this.gameObject.transform.position = GameObject.FindWithTag("ThirdCam").transform.position;
                    firstCam.enabled = false;
                    thirdCam.enabled = true;
                    break;
                }

            case (int)Cam_State.First_Cam:
                {
                    this.gameObject.transform.position = GameObject.FindWithTag("FirstCam").transform.position;
                    thirdCam.enabled = false;
                    firstCam.enabled = true;
                    break;
                }
        }
        //this.gameObject.transform.position = GameObject.FindWithTag("ThirdCam").transform.position;
    }
    #endregion

    #region Public Method
    public void SetCamera_First()
    {

        this.gameObject.transform.position = GameObject.FindWithTag("FirstCam").transform.position;
    }

    public void SetCamera_Third()
    {
        this.gameObject.transform.position = GameObject.FindWithTag("ThirdCam").transform.position;
    }
    #endregion
}
