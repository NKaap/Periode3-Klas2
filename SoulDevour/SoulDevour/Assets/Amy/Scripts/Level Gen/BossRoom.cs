using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
   
    public GameObject[] bossArray;
    public Transform bossSpawnPoint;
    public int random;
    public bool yes;
   
    void Start()
    {

        random = Random.Range(0, bossArray.Length -1);
        Instantiate(bossArray[random], bossSpawnPoint.position, new Quaternion(0, 0, 0, 0)); 

        // werkt met seed, verander de seed, dan veranderd dit ook.
    }

 
}
