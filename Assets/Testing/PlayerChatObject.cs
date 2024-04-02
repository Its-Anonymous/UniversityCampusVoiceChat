using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChatObject : MonoBehaviour
{
    public string userId;
    public string nickName;

    public Text nickNameTxt;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(()=> CampusChatHandler.instance.TypeMessage(this));
    }

    // Start is called before the first frame update
    public void SetData(string userId, string nickName)
    {
        this.userId = userId;
        this.nickName = nickName;
        SetText(this.nickName);
    }

    private void SetText(string nickName)
    {
        this.nickNameTxt.text = nickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
