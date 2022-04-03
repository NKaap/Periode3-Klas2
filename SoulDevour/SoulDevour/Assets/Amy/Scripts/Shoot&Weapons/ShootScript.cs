using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    
    public MovPlayer player;

    [Header("Shoot Things")]

    public float speed = 20; // shoot speed;
    public float delay;
    private float calculatedDelay => ShootItems();
    public GameObject[] bulletTypes;
    public GameObject[] bombTypes;

    //private float delayBomb = 2f;


    // Update is called once per frame
    void Update()
    {
        
        //delayBomb -= Time.deltaTime;
        // het enige wat nodig is is dat je forward schiet, of met je pijltjes zoals Binding of Isaac
        if (Input.GetButtonDown("Fire1") && calculatedDelay < 0)
        {
            ShootItems();
            Rigidbody instantiatedProjectile = Instantiate(GetBulletModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            instantiatedProjectile.transform.rotation = Random.rotation;

        }
        //if (Input.GetButtonDown("E")) // inventory, niet op dezelfde manier als de bullet 
        //{
        //    //delayBomb = 2f; // delay gwn niet, en dan in de inventory bijv 3 bommen zodat je zelf kiest
        //    Rigidbody instantiatedBomb = Instantiate(GetBombModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
        //    instantiatedBomb.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        //    instantiatedBomb.transform.rotation = Random.rotation;
        //}

    }

    #region Item Stuff

    public float ShootItems()
    {
        float output = delay;
        output -= Time.deltaTime;

        foreach (ItemBase.ItemType item in player.items)
        {
            switch (item)
            {
                case ItemBase.ItemType.BadGrade:
                    {

                        output -= 0.2f;
                        break;
                    }
                case ItemBase.ItemType.Plant:
                    {
                        output -= 0.2f;

                        break;
                    }

            }
          
        }
        return output;

    }

    #region GetModels

    //public GameObject GetBombModel()
    //{
    //    GameObject outputBomb = bombTypes[0];

    //    foreach (ItemBase.ItemType item in player.items)
    //    {
    //        switch (item)
    //        {
    //            case ItemBase.ItemType.PeanutButter:
    //                {
    //                    outputBomb = bombTypes[1];  // let op dat nr 1 ook echt peanut butter is!
    //                    //bulletTypes[1].transform.rotation = Random.rotation;
    //                    break;
    //                }
    //            case ItemBase.ItemType.Lego:
    //                {
    //                    outputBomb = bombTypes[2];
    //                    break;
    //                }

    //        }
    //    }
    //    return outputBomb;
    //}

    public GameObject GetBulletModel()
    {
        GameObject output = bulletTypes[0];
       

        //foreach (ItemBase.ItemType item in player.items)
        //{
        //    switch (item)
        //    {           
        //        case ItemBase.ItemType.Fireballs:
        //            {
        //                output = bulletTypes[1]; // ball with fireee
        //                break;
        //            }
        //        case ItemBase.ItemType.Glasses:
        //            {
        //               // output = bulletTypes[2]; // deze prefab heeft 2 bullets.
        //                break;
        //            }
        //        case ItemBase.ItemType.Poop:
        //            {
        //                //output = bulletTypes[3]; // poop model.
        //                break;
        //            }
        //    }
        //}
       
        return output;
    }

    #endregion
    #endregion
}
