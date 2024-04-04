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
        List<Material> test = new();
        test.Add(materials[index]);
        myRenderer.materials = test.ToArray();
        Debug.Log(materials[index].name);
    }

    // Start is called before the first frame update
    void Start()
    {
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
