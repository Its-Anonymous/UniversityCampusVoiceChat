using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Chat;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class LoginScene : MonoBehaviourPunCallbacks
{
    [Header("UI Panels")]
    public GameObject[] panels; //??????UI????


    public const string START_PANEL = "StartPanel";
    public const string LOGIN_PANEL = "LoginPanel";
    public const string SELECT_PANEL = "SelectPanel";
    public const string SELECT_Character_PANEL = "SelectCharacterPanel";

    public Text startTipsTxt;
    public InputField iptUserName;

    public static LoginScene instance;



    private void Awake()
    {
        ShowPanel(START_PANEL); //????????????????
        instance = this;
    }


    private IEnumerator Start()
    {
        ShowTip("Ready to connect the server");
        yield return new WaitForSeconds(1.0f); //1??????????????????
        
        PhotonNetwork.ConnectUsingSettings(); //??????????????
        ShowTip("Connecting server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");
        startTipsTxt.text = "Successfully connected the game server";
        PhotonNetwork.JoinLobby();
      
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
        startTipsTxt.text = "Successfully join the lobby";
        StartCoroutine(EnterLogin());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("newPlayer.NickName: " + newPlayer.NickName);
    }

    IEnumerator EnterLogin()
    {
        
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        startTipsTxt.text = "Loading login page";
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        ShowPanel(LOGIN_PANEL);
    }

    public void ShowTip(string message)
    {
        startTipsTxt.text = message;
    }

    string playerName = "";

    public void OnBtnEnterGameClick()
    {
        playerName = iptUserName.text.Trim();
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            ShowPanel(SELECT_PANEL);
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
    }
    public void OnBtnChoiceJackClick()
    {
        GameManager.Instance.SelectedGender = Gender.male;
        Hashtable hash = new Hashtable();
        hash.Add("Gender", "Male");
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        ShowPanel(SELECT_Character_PANEL);
    }
    public void OnBtnChoiceIronClick()
    {
        ShowTip("Game loading");
        GameManager.Instance.SelectedGender = Gender.female;
        Hashtable hash = new Hashtable();
        hash.Add("Gender", "Female");
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        ShowPanel(SELECT_Character_PANEL);
    }

    public void CharacterSelection()
    {

        //GameManager.Instance.SelectedPlayer = 1;
        //ShowTip("Game loading");
        //JoinRoom();
    }
    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            ChatManager.Instance.Connect(); //??????????????????
            PhotonNetwork.JoinOrCreateRoom("Campus", new Photon.Realtime.RoomOptions
            {
                MaxPlayers = 8,
                PublishUserId = true,
                IsOpen = true,
                IsVisible = true,
            }, Photon.Realtime.TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Campus");
    }
    
    public void ShowPanel(string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name == name) panels[i].SetActive(true);
            else panels[i].SetActive(false);
        }
    }
}
