using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    
    public MovPlayer player;

    [Header("Shoot Things")]

    public float speed = 20;
    private float delayShoot = 0.5f;
    public GameObject[] bulletTypes;
    public GameObject[] bombTypes;

    //private float delayBomb = 2f;


    // Update is called once per frame
    void Update()
    {
        delayShoot -= Time.deltaTime;
        //delayBomb -= Time.deltaTime;
        // het enige wat nodig is is dat je forward schiet, of met je pijltjes zoals Binding of Isaac
        if (Input.GetButtonDown("Fire1") && delayShoot < 0)
        {
            delayShoot = 0.5f;
            Rigidbody instantiatedProjectile = Instantiate(GetBulletModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            instantiatedProjectile.transform.rotation = Random.rotation;

            ShootItems();
          
        }
        if (Input.GetButtonDown("E")) // inventory, niet op dezelfde manier als de bullet 
        {
            //delayBomb = 2f; // delay gwn niet, en dan in de inventory bijv 3 bommen zodat je zelf kiest
            Rigidbody instantiatedBomb = Instantiate(GetBombModel().GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
            instantiatedBomb.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            instantiatedBomb.transform.rotation = Random.rotation;
        }

    }

    #region Item Stuff

    public void ShootItems()
    {
        foreach (ItemBase item in player.items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.BadGrade:
                    {

                        delayShoot = 0.2f;
                        break;
                    }

            }
        }
    }

    #region GetModels

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
                case ItemBase.ItemType.Lego:
                    {
                        outputBomb = bombTypes[2];
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

    #endregion
    #endregion
}
