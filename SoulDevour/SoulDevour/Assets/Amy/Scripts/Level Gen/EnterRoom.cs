using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public GameObject[] enemiesInRoom;

    // player enters room, locks, enemies are activated. 
    // Dit script op de floor, enemies die in de floor prefab zitten, in de array gooien..... 
    // hoe doe je de deuren dicht als je vanaf dit script niet weet hoeveel deuren deze kamer heeft? 


    private void Start()
    {
        foreach (GameObject enemy in enemiesInRoom)
        {     
            enemy.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            foreach(GameObject enemy in enemiesInRoom)
            {
               
                StartCoroutine(Enemies());
                enemy.SetActive(true);
            }
        }
    }

    IEnumerator Enemies()
    {
        yield return new WaitForSeconds(1f);
    }
}
