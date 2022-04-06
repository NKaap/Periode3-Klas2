using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{

    public GameObject enemyOne, enemyTwo, enemyThree;
    public GameObject barOne, barTwo, barThree, barFour;
  
    public float radius = 2;

    public bool allEnemiesDead;
    public bool playerEnteredRoom = false;
    public bool playerCol = false;
    public float timeLeft = 2;

    // player enters room, locks, enemies are activated. 

    private void Start()
    {
        barOne.SetActive(false);
        barThree.SetActive(false);
        barTwo.SetActive(false);
        barFour.SetActive(false);
        enemyOne.SetActive(false);
        enemyThree.SetActive(false);
        enemyTwo.SetActive(false);
    }


    private void Update()
    {
        if (playerEnteredRoom & !playerCol)
        {
            IronBarsRoomLock();
            enemyOne.SetActive(true);
            enemyThree.SetActive(true);
            enemyTwo.SetActive(true);
            playerCol = true;
        }
      
        if(enemyOne == null && enemyTwo == null && enemyThree == null)
        {
            allEnemiesDead = true;
           
        }

        if (allEnemiesDead)
        {
            Debug.Log("EnemiesGone");
            Destroy(barOne);
            Destroy(barTwo);
            Destroy(barThree);
            Destroy(barFour);
          
        }
    }

    public void IronBarsRoomLock()
    {
        Collider[] barOneColliders = Physics.OverlapSphere(barOne.transform.position, radius);
        foreach (Collider collider in barOneColliders)
        {
            if (collider.transform.CompareTag("Door"))
            {
                barOne.SetActive(true);
            }
        }

        Collider[] barTwoColliders = Physics.OverlapSphere(barTwo.transform.position, radius);
        foreach (Collider collider in barTwoColliders)
        {
            if (collider.transform.CompareTag("Door"))
            {
                barTwo.SetActive(true);
            }
        }

        Collider[] barThreeColliders = Physics.OverlapSphere(barThree.transform.position, radius);
        foreach (Collider collider in barThreeColliders)
        {
            if (collider.transform.CompareTag("Door"))
            {
                barThree.SetActive(true);
            }
        }

        Collider[] barFourColliders = Physics.OverlapSphere(barFour.transform.position, radius);
        foreach (Collider collider in barFourColliders)
        {
            if (collider.transform.CompareTag("Door"))
            {
                barFour.SetActive(true);
            }
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // nog een timer aan toevoegen
            
            playerEnteredRoom = true;

            
        }

    }


}
