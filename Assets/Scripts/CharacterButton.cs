using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public Character character;
    public Button thisBtn;


    // Start is called before the first frame update
    void Start()
    {
        thisBtn.onClick.AddListener(()=> GetComponentInParent<CharacterSelectionScreen>().SelectCharacter(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
