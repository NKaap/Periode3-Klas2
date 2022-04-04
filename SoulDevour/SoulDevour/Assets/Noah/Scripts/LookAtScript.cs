using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public Transform cameraObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        cameraObj = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.LookAt(cameraObj);
        //transform.rotation *= Quaternion.Euler(0, 180, 0);

        transform.rotation = Quaternion.LookRotation(transform.position - cameraObj.position);
    }
}
