using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    //public Transform player;
    //public Transform chosenBullet;
    //public Transform[] allBulletTypes;
    //public Transform shootPoint;

    public Rigidbody projectile;
    public float speed = 20;
   //public float radius;

  

    // Update is called once per frame
    void Update()
    {
        // het enige wat nodig is is dat je forward schiet, of met je pijltjes zoals Binding of Isaac
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        }

    }
}
