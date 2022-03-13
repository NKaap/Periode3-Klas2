using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public GameObject[] enemiesInRoom;
    public Collider[] doorLock;
    public bool first = false;
    // player enters room, locks, enemies are activated. 
    // Dit script op de floor, enemies die in de floor prefab zitten, in de array gooien..... 
    // hoe doe je de deuren dicht als je vanaf dit script niet weet hoeveel deuren deze kamer heeft? 

    private void Update()
    {
     

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && !first)
        {
            first = true;
            foreach(Collider coll in doorLock)
            {
                StartCoroutine(Enemies());
                coll.enabled = true;
            }
        }

        foreach(GameObject enemy in enemiesInRoom)
        {
            if(enemy == null)
            {

                foreach (Collider coll in doorLock)
                {
                    coll.enabled = false;
                }
            }
        }
       
      

    }

    IEnumerator Enemies()
    {
        yield return new WaitForSeconds(3f);
    }
}
