using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopRoom : MonoBehaviour
{

    public GameObject[] allItems;
    public Transform spawnOne, spawnTwo, spawnThree;

    public bool one, two, three;

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, allItems.Length);

       
       Instantiate(allItems[random], spawnOne.position, spawnOne.rotation);
      
         
        //costOne.text = itemOne.GetComponent<ItemBase>().itemCost.ToString();

       Instantiate(allItems[Random.Range(0, allItems.Length)], spawnTwo.position, spawnTwo.rotation);
        
        //costTwo.text = itemTwo.GetComponent<ItemBase>().itemCost.ToString();

       Instantiate(allItems[Random.Range(0, allItems.Length)], spawnThree.position, spawnThree.rotation);
       // costThree.text = itemThree.GetComponent<ItemBase>().itemCost.ToString();

        // werkt met seed, verander de seed, dan veranderd dit ook.
    }

}
