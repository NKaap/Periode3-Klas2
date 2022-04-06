using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    GameObject player;
    public GameObject item;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && player.GetComponent<MovPlayer>().money >= 8)
        {
            player.GetComponent<MovPlayer>().money -= 8;
            Instantiate(item, transform.position, transform.rotation);
            Destroy(gameObject);

           
        }
        if(player.GetComponent<MovPlayer>().money <= 8)
        {
            Debug.Log("Not enough!");
        }
    }
}
