using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoom : MonoBehaviour
{

    public GameObject[] allItems;
    public int random;
    public Transform spawnPointPedestal;
    bool yea = true;

    private void Update()
    {
        if (yea)
        {
            Instantiate(allItems[Random.Range(0, allItems.Length)], spawnPointPedestal.position, spawnPointPedestal.rotation);
            // werkt met seed, verander de seed, dan veranderd dit ook.
            yea = false;
        }

    }



}
