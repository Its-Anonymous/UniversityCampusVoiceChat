using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothDynamic : MonoBehaviour
{
    public Renderer myRenderer;
    public Material[] materials;

    public void UpdateMaterial(int index)
    {
        myRenderer.materials[0] = materials[index];
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
