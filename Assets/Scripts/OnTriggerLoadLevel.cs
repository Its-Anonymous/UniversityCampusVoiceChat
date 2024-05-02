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
                plyr.gameObject.SetActive(false);
                blackScreen.SetActive(true);
                plyr.GetComponent<vThirdPersonController>().enabled = false;
                StartCoroutine(_TransitionDelay(plyr.gameObject));
            }
        }
    }


    IEnumerator _TransitionDelay(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.move(player,SpawnPoint.transform,0f);
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        player.GetComponent<vThirdPersonController>().enabled = true;
        blackScreen.SetActive(false);
    }
}