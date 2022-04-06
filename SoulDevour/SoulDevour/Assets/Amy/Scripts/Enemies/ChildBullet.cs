using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBullet : MonoBehaviour
{
    float timeLeft = 0.05f;
    bool addforceyes = false;
    public Rigidbody rb;
    // Update is called once per frame
    void Update()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && !addforceyes)
        {
           // rb.AddExplosionForce(100, transform.position, 10, 10, ForceMode.Impulse);
            rb.AddForce(transform.forward * 10000);
            addforceyes = true;
            Debug.Log("Addforceee");
        }

       
    }
}
