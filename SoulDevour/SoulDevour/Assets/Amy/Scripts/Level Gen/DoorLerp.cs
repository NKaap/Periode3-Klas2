using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLerp : MonoBehaviour
{

    public float radius;


    // Start is called before the first frame update
    void Start()
    {
        
    } 




    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Player") && Input.GetButtonDown("E"))
            {
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0); // voor nu 0,0,0, ff testen als muren er goed in zitten 
                Debug.Log("Yass");
            }
        }

    }
}
