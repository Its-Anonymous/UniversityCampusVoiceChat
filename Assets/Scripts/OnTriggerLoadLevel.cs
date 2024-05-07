using Photon.Pun;
using UnityEngine;
using Invector.vCharacterController;
using System.Collections;

public class OnTriggerLoadLevel : MonoBehaviour
{
    public GameObject SpawnPoint;
    public GameObject blackScreen;

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            Debug.Log("gameObject.name: " + gameObject.name);
            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && plyr.GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(plyr.GetComponent<PhotonView>());
                //plyr.gameObject.SetActive(false);
                blackScreen.SetActive(true);
                //plyr.GetComponent<vThirdPersonController>().enabled = false;
                StartCoroutine(_TransitionDelay(plyr.gameObject));
            }
        }
    }


    IEnumerator _TransitionDelay(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        //LeanTween.move(player,SpawnPoint.transform,0f);
        SpawnCharacter();
        yield return new WaitForSeconds(0.5f);
        //player.SetActive(true);
        //player.GetComponent<vThirdPersonController>().enabled = true;
        blackScreen.SetActive(false);
    }



    private void SpawnCharacter()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            //AgoraManager.Instance.JoinChannel(); //Agora sdk????????????????????
            int SeletedPlayer = GameManager.Instance.SelectedPlayer;
            Transform spawnPoint = SpawnPoint.transform;
            string prefabName = string.Empty;
            switch (SeletedPlayer)
            {
                case 1:
                    prefabName = CampusManager.Instance.male1.name;
                    break;
                case 2:
                    prefabName = CampusManager.Instance.male2.name;
                    break;
                case 3:
                    prefabName = CampusManager.Instance.male3.name;
                    break;

                case 4:
                    prefabName = CampusManager.Instance.female1.name;
                    break;
                case 5:
                    prefabName = CampusManager.Instance.female2.name;
                    break;
                case 6:
                    prefabName = CampusManager.Instance.female3.name;
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

}