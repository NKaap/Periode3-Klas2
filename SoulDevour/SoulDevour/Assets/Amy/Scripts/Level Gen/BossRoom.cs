using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    public GameObject[] teachers;
    public GameObject barOne, barTwo, barThree, barFour;
    public Transform spawnpoint;
    public float radius = 2;

    public bool teacherDead = false;
    public bool playerEnteredRoom = false;
    public float timeLeft = 2;
    public bool spawned = false;

    public GameObject doorToNextLevel;
    // player enters room, locks, enemies are activated. 

    private void Start()
    {
        barOne.SetActive(false);
        barThree.SetActive(false);
        barTwo.SetActive(false);
        barFour.SetActive(false);
        doorToNextLevel.SetActive(false);

    }


    private void Update()
    {
        if (playerEnteredRoom)
        {
            IronBarsRoomLock();        
        }


        if (teacherDead)
        {
            barOne.SetActive(false);
            barThree.SetActive(false);
            barTwo.SetActive(false);
            barFour.SetActive(false);
            doorToNextLevel.SetActive(true);
        }
    }

    public void SpawnTeacher()
    {
        GameObject spawnedTeacher = Instantiate(teachers[Random.Range(0, teachers.Length)], spawnpoint.position, spawnpoint.rotation);
        spawned = true;

        if(spawnedTeacher == null)
        {
            Debug.Log("Teacher Down!");
            teacherDead = true;
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
            if (!spawned)
            {
                SpawnTeacher();
            }

            playerEnteredRoom = true;

        }

    }

}
