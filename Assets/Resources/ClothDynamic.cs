using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ClothDynamic : MonoBehaviour
{
    public Gender gender;

    public Renderer myRenderer;
    public Material[] materials;

    public void UpdateMaterial(int index)
    {
        Debug.Log("UpdateMaterial index: " + index);
        myRenderer.materials[0] = materials[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCostume();
    }

    private void UpdateCostume()
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

        //if (newPlayer.CustomProperties["Gender"].ToString().Equals("Male"))
        if (gender.Equals(Gender.male))
        {
            if (maleCount == 1)
            {
                UpdateMaterial(0);
            }

            else if (maleCount == 2)
            {
                UpdateMaterial(1);
            }
            else
            {
                UpdateMaterial(0);
                //GetComponent<PhotonView>().RPC(nameof(UpdateCostume), RpcTarget.All, newPlayer.UserId, 0);
            }
        }

        //if (newPlayer.CustomProperties["Gender"].ToString().Equals("Female"))
        if (gender.Equals(Gender.female))
        {
            if (femaleCount == 1)
            {
                UpdateMaterial(0);
                //GetComponent<PhotonView>().RPC(nameof(UpdateCostume), RpcTarget.All, newPlayer.UserId, 0);
            }
            else if (femaleCount == 2)
            {
                UpdateMaterial(1);
                //GetComponent<PhotonView>().RPC(nameof(UpdateCostume), RpcTarget.All, newPlayer.UserId, 1);
            }
            else
            {
                UpdateMaterial(0);
                //GetComponent<PhotonView>().RPC(nameof(UpdateCostume), RpcTarget.All, newPlayer.UserId, 0);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
