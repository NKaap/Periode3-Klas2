using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    public float speed = 20;
    
    public MovPlayer player;

    public GameObject[] bulletTypes;
    public GameObject[] bombTypes;

    private float delay = 0.5f;

   

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        // het enige wat nodig is is dat je forward schiet, of met je pijltjes zoals Binding of Isaac
        if (Input.GetButtonDown("Fire1") && delay < 0)
        {
            delay = 0.5f;
            Rigidbody instantiatedProjectile = Instantiate(GetBulletModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            instantiatedProjectile.transform.rotation = Random.rotation;
          
        }
        if (Input.GetButtonDown("E"))
        {
            Rigidbody instantiatedBomb = Instantiate(GetBombModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
            instantiatedBomb.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            instantiatedBomb.transform.rotation = Random.rotation;
        }

    }

    public GameObject GetBombModel()
    {
        GameObject outputBomb = bombTypes[0];

        foreach (ItemBase item in player.items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.PeanutButter:
                    {
                        outputBomb = bombTypes[1];  // let op dat nr 1 ook echt peanut butter is!
                        //bulletTypes[1].transform.rotation = Random.rotation;
                        break;
                    }

            }
        }
        return outputBomb;
    }

    public GameObject GetBulletModel()
    {
        GameObject output = bulletTypes[0];
       

        foreach (ItemBase item in player.items)
        {
            switch (item.itemtype)
            {           
                case ItemBase.ItemType.Fireballs:
                    {
                        output = bulletTypes[1];
                        break;
                    }
            }
        }
       
        return output;
    }

}
