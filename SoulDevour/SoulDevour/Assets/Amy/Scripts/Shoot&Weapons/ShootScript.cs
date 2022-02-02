using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    public Transform player;
    public Transform chosenBullet;
    public Transform[] allBulletTypes;
    public Transform shootPoint;
   
   

    public float radius;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // het enige wat nodig is is dat je forward schiet, of met je pijltjes zoals Binding of Isaac


       Transform bulletGo = Instantiate(chosenBullet, shootPoint.position, shootPoint.rotation);
        
    }
}
