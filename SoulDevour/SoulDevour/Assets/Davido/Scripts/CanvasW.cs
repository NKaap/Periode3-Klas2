using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasW : MonoBehaviour
{
    public Transform cameraObj;
   
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(cameraObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
