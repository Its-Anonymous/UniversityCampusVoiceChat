using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CampusChatHandler : MonoBehaviourPunCallbacks
{
    public VoiceManager voiceManager;
    public PlayerChatObject playerChatObjectPrefab;
    public Transform contentParent;
    public Transform playerChatObjectParentTransform;

    public List<PlayerChatObject> playerChatObjects = new();
    public static CampusChatHandler instance;

    public GameObject ChatBox;
    public InputField ChatBoxInputField;
    public Button ChatBoxSendBtn;

    PlayerChatObject PlayerChatObject;

    public GameObject MessagePopUp;

    internal void TypeMessage(PlayerChatObject playerChatObject)
    {
        ChatBox.SetActive(true);
        ChatBoxInputField.text = "";
        this.PlayerChatObject = playerChatObject;
    }

    private void Awake()
    {
        instance = this;
        ChatBoxInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ChatBoxSendBtn.onClick.AddListener(delegate { SendChatMessage(); });
        contentParent.gameObject.SetActive(playerChatObjects.Count > 0);

    }

    private void SendChatMessage()
    {
        if (PhotonNetwork.IsConnected)
        {
            GetComponent<PhotonView>().RPC(nameof(ShowChatRPC), RpcTarget.Others, ChatBoxInputField.text, PhotonNetwork.LocalPlayer.UserId, this.PlayerChatObject.userId);
        }
        ChatBox.SetActive(false);
    }


    [PunRPC]
    public void ShowChatRPC(string Message, string senderId, string receiverId)
    {
        if (receiverId.Equals(PhotonNetwork.LocalPlayer.UserId))
        {
            foreach (var item in PhotonNetwork.CurrentRoom.Players)
            {
                if (item.Value.UserId.Equals(senderId))
                {
                    Debug.Log("I Receive a message from " + item.Value.NickName);
                    Debug.Log("Message = " + Message);

                    string msg = "Message Receives From "+item.Value.NickName  +"\n" + Message;
                    ShowMessage(msg);
                    break;
                }
            }
        }
    }

    [PunRPC]
    public void UpdateCostumeRPC(string userId, int index)
    {
        StartCoroutine(_ChangeAfterDelay(userId, index));
    }

    public IEnumerator _ChangeAfterDelay(string userId, int index)
    {
        yield return new WaitForSeconds(1f);
        foreach (var item in FindObjectsOfType<ClothDynamic>(true))
        {
            Debug.Log("UpdateCostume: " + item.GetComponentInChildren<MessageUI>().userId);
            if (item.GetComponentInChildren<MessageUI>().userId == userId)
            {
                Debug.Log("item.name" + item.name, item);
                item.UpdateMaterial(index);
            }
        }
    }

    public void ShowMessage(string msg)
    {
        MessagePopUp.GetComponentInChildren<Text>().text = msg;
        MessagePopUp.gameObject.SetActive(true);
        Invoke(nameof(HideMessage) , 3f);
    }

    public void HideMessage()
    {
        MessagePopUp.gameObject.SetActive(false);
    }

    private void ValueChangeCheck()
    {
        ChatBoxSendBtn.interactable = !string.IsNullOrEmpty(ChatBoxInputField.text.Trim());
    }

    public override void OnJoinedRoom()
    {
        voiceManager.ResetMicIcon();
        voiceManager.EnableDisableVoiceManager();

        UpdatePlayerChatList();
        Debug.Log("Total CountOfPlayers: " + PhotonNetwork.CountOfPlayers);

    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom: " + newPlayer.NickName);
        UpdatePlayerChatList();
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
            UpdateCostume(newPlayer);
    }

    private void UpdateCostume(Photon.Realtime.Player newPlayer)
    {
        int maleCount = 0;
        int femaleCount = 0;

        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            if (item.Value.CustomProperties.ContainsKey("Gender"))
            {
                Debug.Log(item.Value.CustomProperties["Gender"]);
                if (item.Value.CustomProperties["Gender"].ToString().Equals("Male"))
                {
                        maleCount++;
                }

                if (item.Value.CustomProperties["Gender"].ToString().Equals("Female"))
                {
                    femaleCount++;
                }
            }
        }

        Debug.Log("maleCount: " + maleCount + ", femaleCount: " + femaleCount);

        if (newPlayer.CustomProperties["Gender"].ToString().Equals("Male"))
        {
            if (maleCount == 1)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 0);
            }

            else if (maleCount == 2)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 1);
            }
            else if (maleCount == 3)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 0);
            }
            else
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 1);
            }
        }

        if (newPlayer.CustomProperties["Gender"].ToString().Equals("Female"))
        {
            if (femaleCount == 1)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 0);
            }

            else if (femaleCount == 2)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 1);
            }
            else if (femaleCount == 3)
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 0);
            }
            else
            {
                GetComponent<PhotonView>().RPC(nameof(UpdateCostumeRPC), RpcTarget.All, newPlayer.UserId, 1);
            }
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom: " + otherPlayer.NickName);
        if (playerChatObjects.Exists(x => x.userId.Equals(otherPlayer.UserId)))
        {
            var obj = playerChatObjects.Find(x=> x.userId.Equals(otherPlayer.UserId));
            if (obj)
            {
                playerChatObjects.Remove(obj);
                Destroy(obj.gameObject);
            }
        }

        contentParent.gameObject.SetActive(playerChatObjects.Count > 0);

    }

    public void UpdatePlayerChatList() 
    {
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            if (playerChatObjects.Exists(x=> x.userId.Equals(item.Value.UserId)) || item.Value.UserId.Equals(PhotonNetwork.LocalPlayer.UserId))
            {
                continue;
            }

            var obj = Instantiate(playerChatObjectPrefab.gameObject, playerChatObjectParentTransform).GetComponent<PlayerChatObject>();
            obj.SetData(item.Value.UserId , item.Value.NickName);
            playerChatObjects.Add(obj);
        }


        contentParent.gameObject.SetActive(playerChatObjects.Count > 0);
    }
}

public enum Gender {male,female }