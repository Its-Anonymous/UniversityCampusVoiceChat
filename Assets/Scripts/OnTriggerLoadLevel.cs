using Photon.Pun;
using UnityEngine;

public class OnTriggerLoadLevel : MonoBehaviour
{
    public GameObject SpawnPoint;

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && plyr.GetComponent<PhotonView>().IsMine)
            {
                plyr.gameObject.SetActive(false);
                plyr.transform.SetParent(SpawnPoint.transform);
                plyr.transform.position = Vector3.zero;
                //plyr.transform.position = SpawnPoint.transform.position;
                plyr.transform.localPosition = Vector3.zero;
                plyr.gameObject.SetActive(true);
            }
        }
    }
}