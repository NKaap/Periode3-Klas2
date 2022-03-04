using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoom : MonoBehaviour
{

    public GameObject[] allItems;

    public int random;
    // Start is called before the first frame update
    void Start()
    {
        //  Instantiate(allItems[Random.RandomRange(0, allItems.Length)], gameObject.transform.position, gameObject.transform.rotation);
        
        // werkt ook niet.    returned altijd dezelfde int. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
