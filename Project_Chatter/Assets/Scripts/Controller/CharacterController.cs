using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviourPunCallbacks, IPunObservable
{

    #region Private variable
    private Transform player_Tr;
    private Vector3 cur_Pos;
    private Quaternion cur_Rot;
    private JoyStickController joyStickController;
    private PhotonView pv;

    private bool IsRun;
    private bool IsIdle;
    #endregion
    [SerializeField] Animator player_Anim;

    #region LifeCycle
    private void Awake()
    {
        pv = this.gameObject.GetComponent<PhotonView>();
        player_Tr = this.gameObject.GetComponent<Transform>();
        joyStickController = FindObjectOfType<JoyStickController>();
        player_Tr = this.transform;
    }

    private void Start()
    {
        /*
        player_Tr = this.transform;
        if(pv.IsMine)
        {
            CameraController.Instance.cam_target = player_Tr;
        }
        */
    }

    private void Update()
    {
        if(!pv.IsMine)
        {
            return;
        }
        else if (pv.IsMine)
        {
            //cam_target = player_Tr;
            CameraController.Instance.cam_target = player_Tr;
            this.gameObject.transform.position += joyStickController.move_Vec;
            if (joyStickController.move_Vec == Vector3.zero)
            {
                IsIdle = true;
                IsRun = false;
                player_Anim.SetBool("IsIdle", true);
                player_Anim.SetBool("IsRun", false);
            }
            else
            {
                IsIdle = false;
                IsRun = true;
                player_Anim.SetBool("IsIdle", false);
                player_Anim.SetBool("IsRun", true);
            }
            if (joyStickController.value != null)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(joyStickController.value.x, joyStickController.value.y) * Mathf.Rad2Deg, 0.0f);
            }
        }

        if(!pv.IsMine)
        {
            player_Tr.position = Vector3.Lerp(player_Tr.position, cur_Pos, Time.deltaTime * 5.0f);
            player_Tr.rotation = Quaternion.Slerp(player_Tr.rotation, cur_Rot, Time.deltaTime * 5.0f);
        }
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player_Tr.position);
            stream.SendNext(player_Tr.rotation);
        }
        else
        {
            cur_Pos = (Vector3)stream.ReceiveNext();
            cur_Rot = (Quaternion)stream.ReceiveNext();
        }
    }
}
