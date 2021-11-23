using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CharacterController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private variable
    private Transform player_Tr;
    private Vector3 cur_Pos;
    private Quaternion cur_Rot;
    #endregion

    [SerializeField] float moveSpeed = 5.0f;

    

    #region LifeCycle
    private void Awake()
    {
        player_Tr = this.gameObject.GetComponent<Transform>();
    }

    private void Start()
    {
        player_Tr = this.transform;
        if (photonView.IsMine)
        {
            Camera.main.GetComponent<CameraController>().cam_target = player_Tr;
        }
    }

    private void Update()
    {
        if(!photonView.IsMine)
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
