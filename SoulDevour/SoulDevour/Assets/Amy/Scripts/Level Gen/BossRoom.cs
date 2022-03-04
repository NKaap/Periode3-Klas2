using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    // boss manager
    public GameObject[] bossArray; // alle soorten bosses
    public Transform bossSpawnPoint;
    public int random;
    public bool yes;
    // Start is called before the first frame update
    void Start()
    {

        //random = Random.Range(0, bossArray.Length -1);
        //Instantiate(bossArray[random], bossSpawnPoint.position, new Quaternion(0, 0, 0, 0)); 

        // werkt niet, elke keer dezelfde !               ^^^^^^
    }

    // Update is called once per frame
    void Update()
    {
     
           
        
    }
}
