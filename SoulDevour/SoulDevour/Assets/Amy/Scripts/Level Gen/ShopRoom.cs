using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopRoom : MonoBehaviour
{

    public GameObject[] allItems;
    public Transform spawnOne, spawnTwo, spawnThree;
    public Text costOne, costTwo, costThree;


    public int cost;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in allItems)
        {
            cost = obj.GetComponent<ItemBase>().itemCost; // get all the prices of the items.
            Debug.Log(cost);
        }

        GameObject itemOne =   Instantiate(allItems[Random.Range(0, allItems.Length)], spawnOne.position, spawnOne.rotation);
        costOne.text = itemOne.GetComponent<ItemBase>().itemCost.ToString();
        GameObject itemTwo = Instantiate(allItems[Random.Range(5, allItems.Length)], spawnTwo.position, spawnTwo.rotation);
        costTwo.text = itemTwo.GetComponent<ItemBase>().itemCost.ToString();
        GameObject itemThree = Instantiate(allItems[Random.Range(10, allItems.Length)], spawnThree.position, spawnThree.rotation);
        costThree.text = itemThree.GetComponent<ItemBase>().itemCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
