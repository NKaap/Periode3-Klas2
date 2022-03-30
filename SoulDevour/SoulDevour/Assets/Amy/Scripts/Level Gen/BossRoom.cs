using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
   // elk level een andere soort teacher, eind level een Special teacher.
    public GameObject[] firstBoss;
    public Transform bossSpawnPoint;
    private int random;
    public bool yes;
   
    void Start()
    {
        random = Random.Range(0, firstBoss.Length -1);
        Instantiate(firstBoss[random], bossSpawnPoint.position, new Quaternion(0, 0, 0, 0)); 
        // werkt met seed, verander de seed, dan veranderd dit ook.
    }

 
}
