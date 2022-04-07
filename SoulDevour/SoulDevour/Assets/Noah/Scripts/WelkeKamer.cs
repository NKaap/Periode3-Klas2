using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelkeKamer : MonoBehaviour
{
    public GameObject TutorialController;

    public bool firstClass;
    public bool fightClass;
    public bool itemRoom;
    public bool shopRoom;
    public bool secondClass;
    public bool bossRoom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (firstClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().firstClass = true;
            }
            if (fightClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().fightClass = true;
            }
            if (itemRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().itemRoom = true;
            }
            if (shopRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().shopRoom = true;
            }
            if (secondClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().secondClass = true;
            }
            if (bossRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().bossRoom = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (firstClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().firstClass = false;
            }
            if (fightClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().fightClass = false;
            }
            if (itemRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().itemRoom = false;
            }
            if (shopRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().shopRoom = false;
            }
            if (secondClass == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().secondClass = false;
            }
            if (bossRoom == true)
            {
                TutorialController.GetComponent<TutorialRoomScript>().bossRoom = false;
            }
        }
    }
}
