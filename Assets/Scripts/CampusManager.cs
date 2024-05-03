using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Invector.vCharacterController;
using UnityEngine.EventSystems;

public class CampusManager : MonoBehaviourPunCallbacks
{
    public GameObject male1;
    public GameObject male2;
    public GameObject male3;
    public GameObject female1;
    public GameObject female2;
    public GameObject female3;
    public Transform spawnPointRoot;
    public vThirdPersonInput playerInput;

    string nextSceneName = string.Empty;

    public static CampusManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            //AgoraManager.Instance.JoinChannel(); //Agora sdk????????????????????
            int SeletedPlayer = GameManager.Instance.SelectedPlayer;
            Transform spawnPoint = spawnPointRoot.GetChild(Random.Range(0, spawnPointRoot.childCount));
            string prefabName = string.Empty;
            switch (SeletedPlayer)
            {
                case 1:
                    prefabName = male1.name;
                    break;
                case 2:
                    prefabName = male2.name;
                    break;
                case 3:
                    prefabName = male3.name;
                    break;

                case 4:
                    prefabName = female1.name;
                    break;
                case 5:
                    prefabName = female2.name;
                    break;
                case 6:
                    prefabName = female3.name;
                    break;

                default:
                    break;
            }

            //PhotonNetwork.Instantiate(name, spawnPoint.position, spawnPoint.rotation);
            //PhotonNetwork.CleanRpcBufferIfMine(photonView);
            //PhotonNetwork.OpCleanRpcBuffer(photonView);
            //photonView.RPC("Create", RpcTarget.AllBuffered, name, spawnPoint.position, spawnPoint.rotation);
            PhotonNetwork.Instantiate(prefabName, spawnPoint.position, spawnPoint.rotation);
            return;
        }
        Debug.LogError("Fail to connect the server!");
    }

    //[PunRPC]
    //public void Create(string name, Vector3 pos, Quaternion rotate)
    //{
    //    PhotonNetwork.Instantiate(name, pos, rotate);
    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        SceneManager.LoadScene("Login");
        //AgoraManager.Instance.Leave();
    }


    //public override void OnConnectedToMaster()
    //{
    //    base.OnConnectedToMaster();
    //    Debug.Log("Successfully connected the game server!");


    //    string currSceneName = SceneManager.GetActiveScene().name;

    //    if (currSceneName == "Campus")
    //        nextSceneName = "Classroom";
    //    else if (currSceneName == "Classroom")
    //        nextSceneName = "Campus";

    //    if (PhotonNetwork.IsConnected)
    //    {
    //        PhotonNetwork.JoinOrCreateRoom(nextSceneName, new Photon.Realtime.RoomOptions
    //        {
    //            IsOpen = true,
    //            IsVisible = true,
    //            MaxPlayers = 0,
    //        }, Photon.Realtime.TypedLobby.Default);

    //        return;
    //    }
    //}
    //public override void OnJoinedRoom()
    //{
    //    base.OnJoinedRoom();
    //    SceneManager.LoadScene(nextSceneName);
    //}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {


    }


    private void Update()
    {
        if (Cursor.visible)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SetCursorVisible(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetCursorVisible(true);
            }
        }
    }


    public void SetCursorVisible(bool visible)
    {
        //Cursor.visible = visible;
        //Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        if(playerInput != null)
            playerInput.enableInput = !visible;
    }
}
