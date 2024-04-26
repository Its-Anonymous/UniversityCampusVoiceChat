using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public GameObject doorTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter" + other.tag);
        
        if (other.tag.Equals("Player"))
        {
            LeanTween.moveLocalX(doorTransform, 2.4f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit" + other.tag);
        if (other.tag.Equals("Player"))
        {
            LeanTween.moveLocalX(doorTransform, 1.167044f, 1f);
        }
    }
}