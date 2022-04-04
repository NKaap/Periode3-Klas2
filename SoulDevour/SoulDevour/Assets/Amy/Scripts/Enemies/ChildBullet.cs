using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBullet : MonoBehaviour
{
    
    private bool shot;
  

    // Update is called once per frame
    void Update()
    {
        if(!shot)
        {
            
            shot = true;
        }
        this.GetComponentInChildren<Rigidbody>().AddForce(Vector3.forward * 20);
    }
}
