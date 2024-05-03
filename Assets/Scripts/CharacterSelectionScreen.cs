using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionScreen : MonoBehaviour
{
    public List<CharacterButton> characters = new();
    public Button backBtn;

    private void Start()
    {
        backBtn.onClick.AddListener(()=> BackEvent());
    }

    private void BackEvent()
    {
        LoginScene.instance.ShowPanel(LoginScene.SELECT_PANEL);
    }

    private void OnEnable()
    {
        foreach (var item in characters)
        {
            item.character.character.SetActive(item.character.gender == GameManager.Instance.SelectedGender);
        }
    }


    public void SelectCharacter(CharacterButton character)
    {
        GameManager.Instance.SelectedPlayer = characters.IndexOf(character)+1;
        LoginScene.instance.ShowPanel(LoginScene.START_PANEL) ;
        LoginScene.instance.ShowTip("Game loading");
        LoginScene.instance.JoinRoom();
    }
}


[Serializable]
public class Character
{
    public Gender gender;
    public GameObject character;
}
