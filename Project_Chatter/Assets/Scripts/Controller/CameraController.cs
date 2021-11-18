using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CameraController : MonoBehaviour
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
    public Vector3 off_set;
    public Transform cam_target;
    #endregion


    #region LifeCycle

    private void Start()
    {
        SetCamera();
    }
    private void Update()
    {
        gameObject.transform.position = cam_target.position + off_set;
    }
    #endregion

    #region Public Method
    public void SetCamera()
    {
        this.gameObject.transform.position = new Vector3(-0.2f, 5.5f, -4.5f);
    }
    #endregion
}
