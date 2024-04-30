using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ClothDynamic : MonoBehaviour
{
    public Gender gender;

    public Renderer myRenderer;
    public Material[] materials;

    public static int maleCount; 
    public static int femaleCount;

    public void UpdateMaterial(int index)
    {
        Debug.Log("UpdateMaterial index: " + index);
        List<Material> test = new();

        int remainder = index % materials.Length - 1;

        // If remainder is negative, add 4 to make it positive
        if (remainder < 0)
        {
            remainder += materials.Length - 1;
        }

        test.Add(materials[remainder]);
        myRenderer.materials = test.ToArray();
        Debug.Log(materials[remainder].name);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gender == Gender.male)
        {
            UpdateMaterial(maleCount);
            maleCount++;
        }
        else
        {
            UpdateMaterial(femaleCount);
            femaleCount++;
        }


        //UpdateMaterial(Random.Range(0, materials.Length));
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
