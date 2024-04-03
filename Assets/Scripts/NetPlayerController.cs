using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class NetPlayerController : MonoBehaviourPun,IPunObservable
{
    public Canvas canvas;
    public Text userName;
    public GameObject thisPlayer;

    private Camera mainCam;
    private MessageUI messageUI;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (PhotonNetwork.InRoom)
        //{
        //    if (stream.IsWriting)
        //        stream.SendNext(PhotonNetwork.LocalPlayer.NickName);
        //    if (stream.IsReading)
        //        userName.text = stream.ReceiveNext().ToString();
        //}
    }

    private void Awake()
    {
        messageUI = GetComponentInChildren<MessageUI>();

        messageUI.playerName = userName.text = photonView.Owner.NickName;
        messageUI.userId = photonView.Owner.UserId;
    }


    void Start()
    {
        mainCam = Camera.main;
        //if (photonView.IsMine)
        //{
        //    photonView.RPC("BindMessageUI", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName,  PhotonNetwork.LocalPlayer.UserId , photonView.Owner.us);
        //}
    }

    private void Update()
    {
        canvas.transform.LookAt(mainCam.transform);
    }

    //[PunRPC]
    //private void BindMessageUI(string playerName)
    //{
    //    messageUI.BindPlayer(playerName);
    //}
    
}
