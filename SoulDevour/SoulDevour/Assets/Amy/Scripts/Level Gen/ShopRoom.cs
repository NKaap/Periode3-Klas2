using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopRoom : MonoBehaviour
{

    public GameObject[] allItems;
    public Transform spawnOne, spawnTwo, spawnThree;
 


    // Start is called before the first frame update
    void Start()
    {
       
        GameObject itemOne =   Instantiate(allItems[1], spawnOne.position, spawnOne.rotation);
        //costOne.text = itemOne.GetComponent<ItemBase>().itemCost.ToString();

        GameObject itemTwo = Instantiate(allItems[2], spawnTwo.position, spawnTwo.rotation);
        //costTwo.text = itemTwo.GetComponent<ItemBase>().itemCost.ToString();

        GameObject itemThree = Instantiate(allItems[5], spawnThree.position, spawnThree.rotation);
       // costThree.text = itemThree.GetComponent<ItemBase>().itemCost.ToString();

        // werkt met seed, verander de seed, dan veranderd dit ook.
    }

}
